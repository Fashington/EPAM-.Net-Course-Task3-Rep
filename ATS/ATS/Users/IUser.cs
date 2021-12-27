using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Users
{
    interface IUser
    {
        public string Name { get; }
        public string Number { get; }
    }
}
