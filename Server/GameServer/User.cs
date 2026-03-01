using Server;
using Server.GameServer;
using Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameServer
{
    public class User
    {
        public readonly AccountInfo accountInfo;

        private readonly ClientSession _session;
        private GameServer _server;
        private DBConnection _db;
        private object _lock = new object();
        private bool _isClosed = false;

        public User(ClientSession session, AccountInfo accountInfo, GameServer server)
        {
            this.accountInfo = accountInfo;
            _session = session;
            _session.ChangePacketHandler(new UserPacketHandler(this, server));
            _server = server;
            _db = server.db;
        }

        public void SendPacket(ArraySegment<byte> data)
        {
            _session.Send(data);
        }

        public bool TryCloseSession()
        {
            lock (_lock)
            {
                if (_isClosed) return false;
                _session.Disconnect();
                _isClosed = true;
            }
            return true;
        }

        public ClientSession Logout()
        {
            lock (_lock)
            {
                if (_isClosed) return _session;
                _isClosed = true;
            }
            _session.ChangePacketHandler(null);
            _server.TryRemoveUserSession(this);


            return _session;
        }

        public async Task<int> GetGold()
        {
            bool canRead = false;
            int gold = -1;

            await _db.RunSql("SELECT * FROM UserInventory WHERE userId = @id AND resourceId = 1;",
                (reader) =>
                {
                    canRead = reader.Read();

                    if (canRead)
                    {
                        gold = (int)reader["resourceCount"];
                    }
                },
                new() { { "@id", accountInfo.userId } });
            if (canRead)
            {
                return gold;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Deck?> GetDeck(int index)
        {
            bool isSuccess = true;


            List<int> cardIds = new List<int>();
            (isSuccess, _) = await _db.RunSql("SELECT * FROM DeckCards WHERE deckId = (SELECT deckId FROM UserDecks WHERE userId = @id AND deckIndex = @index);",
                (reader) =>
                {
                    while (reader.Read())
                    {
                        cardIds.Add((int)reader["cardId"]);
                    }
                },
                new() { { "@id", accountInfo.userId }, { "@index", index } });
            if (!isSuccess) return null;


            List<int> heroIds = new List<int>();
            (isSuccess, _) = await _db.RunSql("SELECT * FROM DeckHeroes WHERE deckId = (SELECT deckId FROM UserDecks WHERE userId = @id AND deckIndex = @index);",
                (reader) =>
                {
                    while (reader.Read())
                    {
                        heroIds.Add((int)reader["heroId"]);
                    }
                },
                new() { { "@id", accountInfo.userId }, { "@index", index } });
            if (!isSuccess) return null;


            return new Deck(heroIds, cardIds);
        }

        public async Task<bool> SetDeck(int index, Deck deck)
        {
            const string sqlFormat =
@"BEGIN TRAN;

DECLARE @deckId AS INT;
SET @deckId = (SELECT deckId FROM UserDecks WHERE userId = @userId AND deckIndex = @deckIndex);
DELETE FROM DeckCards WHERE deckId = @deckId;
DELETE FROM DeckHeroes WHERE deckId = @deckId;

{0}

COMMIT;";

            const string cardSqlFormat = "INSERT INTO DeckCards(deckId,cardId,cardCount) VALUES(@deckId,{0},{1});\n";
            const string heroSqlFormat = "INSERT INTO HeroCards(deckId,heroId,heroCount) VALUES(@deckId,{0},1);\n";

            string insertSql = "";

            foreach (var cardId in deck.cardIds.GetItemCounts())
            {
                insertSql += string.Format(cardSqlFormat, cardId.value, cardId.count);
            }

            foreach (var heroId in deck.heroIds)
            {
                insertSql += string.Format(heroSqlFormat, heroId);
            }

            string sql = string.Format(sqlFormat, insertSql);

            (bool isSuccess, _) = await _db.RunSql(sql, callBack: null, new() { { "@userId", accountInfo.userId }, { "@deckIndex", index } });

            return isSuccess;
        }
    }
}
