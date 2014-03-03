using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SteerRent.DAL.Helper
{
    class Helper
    {
        static public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SteerRentConnection"].ToString();
        }

        static public string GetConnectionString_aspnetDB()
        {
            return ConfigurationManager.ConnectionStrings["MyUserConnection"].ToString();
        }
    }
}
