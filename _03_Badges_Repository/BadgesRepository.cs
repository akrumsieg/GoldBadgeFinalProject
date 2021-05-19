using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Badges_Repository
{
    public class BadgesRepository
    {
        Dictionary<int, List<string>> _badgeDic = new Dictionary<int, List<string>>();

        //CRUD METHODS
        //CREATE
        public bool AddBadge(Badge badge)
        {
            int originalCount = _badgeDic.Count();
            _badgeDic.Add(badge.BadgeID, badge.DoorAccessList);
            return _badgeDic.Count() > originalCount;
        }

        //READ
        public Dictionary<int, List<string>> ReturnBadgeDictionary()
        {
            return _badgeDic;
        }

        //UPDATE
        public void UpdateBadgeAccess(int numToUpdate, List<string> updatedDoorAccessList)
        {
            _badgeDic[numToUpdate] = updatedDoorAccessList;
        }

        //DELETE
        public bool RemoveAllAccess(int numToRemove)
        {
            _badgeDic[numToRemove] = new List<string>();
            if (_badgeDic[numToRemove].Count() == 0) return true;
            else return false;
        }

        //helper methods
        
    }
}
