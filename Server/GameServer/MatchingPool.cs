using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameServer
{
    public class MatchingPool
    {
        public event Action<User, User>? OnMatch;

        private List<User> _users = new();

        public bool AddUser(User user)
        {
            if (_users.Contains(user)) return false;

            if (!MatchCheck(user))
            {
                _users.Add(user);
            }
            return true;
        }

        public bool RemoveUser(User user)
        {
            return _users.Remove(user);
        }

        public bool Contains(User user)
        {
            return _users.Contains(user);
        }

        private bool MatchCheck(User newUser)
        {
            if (_users.Count > 0)
            {
                User foundUser = _users[0];
                _users.Remove(foundUser);
                OnMatch?.Invoke(foundUser, newUser);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
