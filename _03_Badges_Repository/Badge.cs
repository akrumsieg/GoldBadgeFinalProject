using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Badges_Repository
{
    public class Badge
    {
        //constructors
        public Badge (int badgeID, List<string> doorAccessList)
        {
            BadgeID = badgeID;
            DoorAccessList = doorAccessList;
        }

        //properties
        public int BadgeID { get; set; }
        public List<string> DoorAccessList { get; set; }

        //helper methods

    }
}
