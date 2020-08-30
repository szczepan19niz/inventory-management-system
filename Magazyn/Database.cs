using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn
{
    class Database
    {
        public string GetConnection()
        {
            string cn = "server=localhost;user id=root;password=;database=magazyn_itemservice";
            return cn;
        }
    }
}
