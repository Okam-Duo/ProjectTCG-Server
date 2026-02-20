using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Packets;

namespace Server.GameServer
{
    public class GameServer
    {
        public event Action<ClientSession>? OnLogout;
        
        public DBConnection db;

        private Dictionary<int, User> _users = new();
        private object _lock = new();

        public GameServer()
        {
            db = new DBConnection();
        }

        public bool TryAddUserSession(AccountInfo accountInfo, ClientSession session)
        {
            bool isContained;
            User user = null;

            lock (_lock)
            {
                isContained = FindUser(accountInfo.userId) != null;
                if (!isContained)//이미 로그인 되있는 계정이 아니라면
                {
                    user = new User(session, accountInfo, this);
                    _users.Add(accountInfo.userId, user);//로그인된 계정 목록에 추가
                }
            }

            if (isContained)
            {
                session.Send(new S_LoginRes(false, accountInfo.nickName, accountInfo.userId).Write());
                return false;
            }
            else
            {
                session.Send(new S_LoginRes(true, accountInfo.nickName, accountInfo.userId).Write());
                OnAddUser(user);

                return true;
            }
        }

        public bool TryRemoveUserSession(User user)
        {
            bool isContained;
            lock (_lock)
            {
                isContained = _users.ContainsKey(user.accountInfo.userId);
                if (isContained)
                {
                    _users.Remove(user.accountInfo.userId);
                }
            }

            if (isContained)
            {
                Console.WriteLine($"유저 로그아웃 : {user.accountInfo.nickName}");
                OnLogout?.Invoke(user.Logout());
                return true;
            }
            else
            {
                return false;
            }
        }

        private User? FindUser(int userId)
        {
            _users.TryGetValue(userId, out User? user);
            return user;
        }

        private void OnAddUser(User user)
        {
            Console.WriteLine($"유저 로그인 : {user.accountInfo.nickName}");
        }
    }
}
