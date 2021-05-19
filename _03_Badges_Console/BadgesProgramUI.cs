using _03_Badges_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Badges_Console
{
    public class BadgesProgramUI
    {
        private readonly BadgesRepository _repo = new BadgesRepository();
        private readonly _00_Helper_Methods.HelperMethods helperMethods = new _00_Helper_Methods.HelperMethods();

        public void Run()
        {
            SeedBadgesDictionary();
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.WriteLine("What would you like to do?\n" +
                    "1. Create a new badge\n" +
                    "2. Update badge access\n" +
                    "3. Display all badges\n" +
                    "4. Remove all access from a badge\n" +
                    "9. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateNewBadge();
                        break;
                    case "2":
                        UpdateBadgeAccess();
                        break;
                    case "3":
                        DisplayAllBadges();
                        break;
                    case "4":
                        RemoveAllAccessFromBadge();
                        break;
                    case "9":
                        continueRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. \n" +
                            "Please enter the number corresponding to the action you would like to do.\n" +
                            "Ex: Type 1 and press the ENTER key to create a new badge.\n");
                        break;
                }
            }
        }

        //MENU OPTION 1 - Create new badge
        public void CreateNewBadge()
        {
            Console.Clear();
            Badge newBadge = new Badge(CollectAndReturnID(), CollectAndReturnDoorAccessList());
            _repo.AddBadge(newBadge);
            helperMethods.ReturnToMenu();
        }

        //MENU OPTION 2 - Update badge access
        public void UpdateBadgeAccess()
        {
            DisplayAllBadges();
            Console.Write("Enter the badge number you would like to update: ");
            int badgeToUpdate = helperMethods.CollectAndReturnPosWholeNum();
            while (!_repo.ReturnBadgeDictionary().ContainsKey(badgeToUpdate))
            {
                Console.Write("That ID does not currently exist.\n" +
                    "Please enter an existing ID number: ");
                badgeToUpdate = helperMethods.CollectAndReturnPosWholeNum();
            }
            bool finishedUpdating = false;
            while (!finishedUpdating)
            {
                DisplayDoorAccess(badgeToUpdate);
                Console.WriteLine("What would you like to do?\n" +
                    "1. Remove door access\n" +
                    "2. Add door access\n" +
                    "3. Return to main menu\n");
                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayDoorAccess(badgeToUpdate);
                        Console.Write("\nWhich door would you like to remove? ");
                        string doorToRemove = Console.ReadLine();
                        if (!_repo.ReturnDoorAccessList(badgeToUpdate).Contains(doorToRemove))
                        {
                            Console.Write($"\nBadge #{badgeToUpdate} does not currently have access to door \'{doorToRemove}\'.\n" +
                                $"Door access remains unchanged.\n" +
                                $"Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            List<string> updatedList = _repo.ReturnDoorAccessList(badgeToUpdate);
                            updatedList.Remove(doorToRemove);
                            _repo.UpdateBadgeAccess(badgeToUpdate, updatedList);
                            Console.WriteLine($"\nAccess to door \'{doorToRemove}\' was removed.\n" +
                                $"Would you like to make further updates to badge #{badgeToUpdate}?");
                            if (helperMethods.CollectAndReturnYOrN() == "n") finishedUpdating = true;
                        }
                        break;
                    case "2":
                        DisplayDoorAccess(badgeToUpdate);
                        Console.Write("\nEnter door name to add access: ");
                        string doorToAdd = Console.ReadLine();
                        if (_repo.ReturnDoorAccessList(badgeToUpdate).Contains(doorToAdd))
                        {
                            Console.Write($"\nBadge #{badgeToUpdate} already has access to door \'{doorToAdd}\'.\n" +
                                $"Door access remains unchanged.\n" +
                                $"Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            List<string> updatedList = _repo.ReturnDoorAccessList(badgeToUpdate);
                            updatedList.Add(doorToAdd);
                            _repo.UpdateBadgeAccess(badgeToUpdate, updatedList);
                            Console.WriteLine($"\nAccess to door \'{doorToAdd}\' was added.\n" +
                                $"Would you like to make further updates to badge #{badgeToUpdate}?");
                            if (helperMethods.CollectAndReturnYOrN() == "n") finishedUpdating = true;
                        }
                        break;
                    case "3":
                        finishedUpdating = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input.\n" +
                            "Press any key to continue.\n");
                        Console.ReadKey();
                        break;
                }
            }
            helperMethods.ReturnToMenu();
        }


        //MENU OPTION 3 - Display all badges
        public void DisplayAllBadges()
        {
            Console.Clear();
            Console.WriteLine("All existing badges:\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "Badge#\tAccessible Doors");
            foreach (KeyValuePair<int, List<string>> badge in _repo.ReturnBadgeDictionary())
            {
                DisplayOneBadge(badge.Key);
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        //MENU OPTION 4 - remove all access from badge
        public void RemoveAllAccessFromBadge()
        {
            DisplayAllBadges();
            Console.Write("\nEnter badge ID for which you would like to remove ALL door access: ");
            int idToStrip = helperMethods.CollectAndReturnPosWholeNum();
            while (!_repo.ReturnBadgeDictionary().ContainsKey(idToStrip))
            {
                Console.Write("That ID does not currently exist.\n" +
                    "Please enter an existing ID number: ");
                idToStrip = helperMethods.CollectAndReturnPosWholeNum();
            }
            Console.Clear();
            Console.WriteLine($"ARE YOU SURE YOU WANT TO REMOVE ALL DOOR ACCESS FROM BADGE #{idToStrip}?");
            if (helperMethods.CollectAndReturnYOrN() == "y")
            {
                _repo.RemoveAllAccess(idToStrip);
                Console.Clear();
                Console.WriteLine($"Badge #{idToStrip} no longer has access to any doors.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Badge #{idToStrip} door access remains unchanged.");
            }
            helperMethods.ReturnToMenu();
        }


        //helper methods
        public int CollectAndReturnID()
        {
            Console.Write("Enter the ID number of the new badge: ");
            int newID = helperMethods.CollectAndReturnPosWholeNum();
            while (_repo.ReturnBadgeDictionary().ContainsKey(newID))
            {
                Console.Write("That ID is already assigned.\n" +
                    "Please enter an available ID number: ");
                newID = helperMethods.CollectAndReturnPosWholeNum();
            }
            return newID;
        }

        public List<string> CollectAndReturnDoorAccessList()
        {
            bool confirmed = false;
            List<string> doors = new List<string>();
            while (!confirmed)
            {
                Console.Write("\nEnter the doors this badge should access.\n" +
                    "Separate each door with a comma. (Door1, Door2, etc.): ");
                doors = Console.ReadLine().Split(',').ToList<string>();
                for (int i = 0; i < doors.Count(); i++)
                {
                    doors[i] = doors[i].Trim();
                }
                Console.WriteLine("\nIs this door access list correct?");
                helperMethods.DisplayStringsList(doors);
                if (helperMethods.CollectAndReturnYOrN() == "y") confirmed = true;
            }
            return doors;
        }

        public void DisplayDoorAccess(int id)
        {
            Console.Clear();
            Console.WriteLine($"Badge #{id} currently has access to these doors:");
            helperMethods.DisplayStringsList(_repo.ReturnBadgeDictionary()[id]);
            Console.WriteLine("");
        }

        public void DisplayOneBadge(int id)
        {
            Console.WriteLine($"{id}\t{_repo.ReturnDoorAccessListAsString(id)}");
        }

        //seed _repo with badges
        public void SeedBadgesDictionary()
        {
            _repo.AddBadge(new Badge(001, new List<string> { "A1", "A2", "B1" }));
            _repo.AddBadge(new Badge(002, new List<string> { }));
            _repo.AddBadge(new Badge(003, new List<string> { "A1", "B1", "C1", "D1" }));
        }
    }
}
