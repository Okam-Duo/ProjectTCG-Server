using Shared.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AccountServer
    {
        private Listener<ClientSession> _listener;
        private AccountServerPacketHandler _packetHandler;
        private DBConnection _db;

        public AccountServer()
        {
            //DNS (Domain Name System)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);


            _packetHandler = new AccountServerPacketHandler(this);
            _listener = new(
                endPoint,
                sessionFactory: () => { return new ClientSession(_packetHandler); },
                onAcceptAsync: OnAcceptAsync
                );
            _db = new DBConnection();


            _listener.StartListen();

            Console.WriteLine($"{nameof(AccountServer)} is constructed!");
        }

        public async Task<bool> CheckIdAvailable(string loginId)
        {
            bool available = false;

            await _db.RunSql(
                @"SELECT count(*) as idCount
FROM Accounts
WHERE loginId = @ID;",
                (reader) =>
                {
                    reader.Read();
                    int idCount = (int)reader["idCount"];
                    if (idCount == 0)
                    {
                        available = true;
                    }
                },
                new() { { "@ID", loginId } }
                );

            return available;
        }

        public async Task<bool> TrySignIn(string loginId, string passwordHash, string nickName)
        {
            bool isSuccess;
            Exception? e;

            (isSuccess, e) = await _db.RunSql(
                @"INSERT INTO Accounts(loginId,loginPasswordHash,nickName)
VALUES(@id,@password,@nickname);",
                callBack: null,
                new() { { "@id", loginId }, { "@password", passwordHash }, { "@nickname", nickName } }
                );

            return isSuccess;
        }

        //반환 : uid, -1이라면 로그인 실패
        public async Task<int> TryGetUID(string loginId, string passwordHash)
        {
            int userId = -1;

            await _db.RunSql(
                @"BEGIN TRAN;

	UPDATE Accounts
	SET lastLoginDateTime = CURRENT_TIMESTAMP
	WHERE loginId=@ID;

	SELECT *
	FROM Accounts
	WHERE loginId=@ID;

COMMIT;",
                (reader) =>
                {
                    reader.Read();
                    if (passwordHash == (string)reader["loginPasswordHash"])
                    {
                        userId = (int)reader["userId"];
                    }
                },
                new() { { "@ID", loginId } }
                );

            return userId;
        }

        private async void OnAcceptAsync(ClientSession session)
        {

        }
    }
}
