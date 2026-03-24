using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTCG_Server.Server
{
    public class IdGenerator
    {
        private int _nextId;

        public int GenerateNewId()
        {
            return _nextId++;
        }
    }
}
