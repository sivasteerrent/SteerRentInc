using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteerRent.Model
{
    public class GlobalEnum
    {
        public enum MasterPages
        { 
            Lookup,
            HLookup,
            GLookup,
            GAndLookup,
            HAndLookup,
            Location,
            Employee,
            Company,
            BU
       }

        public enum Flag
        { 
            Select = 0,
            Insert = 1,
            Update = 2,
            Delete = 3,
            StatusUpdate = 4

        }
    }
}
