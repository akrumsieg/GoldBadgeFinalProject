using _01_Cafe_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Cafe_Console
{
    public class CafeProgramUI
    {
        private readonly MenuRepository _repo = new MenuRepository();
        public void Run()
        {
            SeedMenuList();
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.WriteLine("What would you like to do?\n" +
                    "1. Add menu item\n" +
                    "2. Display all menu items\n" +
                    "3. Update a menu item\n" +
                    "4. Delete a menu item\n" +
                    "9. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        DisplayAllItems();
                        break;
                    case "3":
                        UpdateItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "9":
                        continueRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. \n" +
                            "Please enter the number corresponding to the action you would like to do.\n" +
                            "Ex: Type 1 and press the ENTER key to begin adding a menu item.\n");
                        break;
                }
            }
        }

        //MENU OPTION 1
        public void AddMenuItem()
        {
            Console.Clear();
            int number = CollectAndReturnNumber();
            string name = CollectAndReturnName();
            string description = CollectAndReturnDescription();
            List<string> ingredients = CollectAndReturnIngredientList();
            double price = CollectAndReturnPrice();

            MenuItem item = new MenuItem(number, name, description, ingredients, price);
            if (_repo.AddMenuItem(item))
            {
                Console.WriteLine("\nItem was added successfully.\n" +
                    "Press any key to return to main menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //MENU OPTION 2
        public bool DisplayAllItems()
        {
            Console.Clear();
            if (_repo.ReturnListCount() == 0)
            {
                Console.WriteLine("There are currently no items on the menu.\n" +
                    "Press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            foreach (MenuItem item in _repo.ReturnMenuList())
            {
                DisplayItem(item);
            }
            return false;
        }

        //MENU OPTION 3
        public void UpdateItem()
        {
            if (DisplayAllItems()) return;
            Console.Write("\nEnter the number of the item you would like to update: ");
            int originalItemNumber = CollectAndReturnPosWholeNum();
            bool isNull = true;
            while (isNull)
            {
                if (_repo.FindItemByNumber(originalItemNumber) == null)
                {
                    Console.Write("\nThat menu item number does not currently exist.\n" +
                        "Please enter the number of an existing menu item to update: ");
                    originalItemNumber = CollectAndReturnPosWholeNum();
                }
                else isNull = false;
            }
            MenuItem updatedItem = new MenuItem();
            updatedItem.Number = CollectAndReturnNumber();
            updatedItem.Name = CollectAndReturnName();
            updatedItem.Description = CollectAndReturnDescription();
            updatedItem.Ingredients = CollectAndReturnIngredientList();
            updatedItem.Price = CollectAndReturnPrice();
            _repo.UpdateByNumber(originalItemNumber, updatedItem);
            Console.WriteLine("\nItem was updated successfully.\n" +
                    "Press any key to return to main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        //MENU OPTION 4
        public void DeleteItem()
        {
            if (DisplayAllItems()) return;
            Console.Write("\nEnter the number of the item you would like to DELETE: ");
            int itemNumToDelete = CollectAndReturnPosWholeNum();
            bool isNull = true;
            while (isNull)
            {
                if (_repo.FindItemByNumber(itemNumToDelete) == null)
                {
                    Console.Write("\nThat menu item number does not currently exist.\n" +
                        "Please enter the number of an existing menu item to DELETE: ");
                    itemNumToDelete = CollectAndReturnPosWholeNum();
                }
                else isNull = false;
            }
            Console.Clear();
            Console.WriteLine("\nAre you sure you want to DELETE this item: ");
            DisplayItem(_repo.FindItemByNumber(itemNumToDelete));
            string yOrN = CollectAndReturnYOrN();
            if (yOrN == "y")
            {
                if (_repo.DeleteByNumber(itemNumToDelete))
                {
                    Console.WriteLine("\nItem was deleted successfully.\n" +
                    "Press any key to return to main menu.");
                    Console.ReadKey();
                }
            }
            Console.Clear();
        }
        public int CollectAndReturnNumber()
        {
        AssignNumber:
            Console.Write("\nEnter menu item number: ");
            int inputInt = CollectAndReturnPosWholeNum();
            if (_repo.FindItemByNumber(inputInt) != null)
            {
                Console.Write("\nThat item number is already assigned. Please enter an available number: ");
                goto AssignNumber;
            }
            else
            {
                return inputInt;
            }
        }

        public string CollectAndReturnName()
        {
            Console.Write("\nEnter menu item name: ");
            return Console.ReadLine().ToUpper();
        }

        public string CollectAndReturnDescription()
        {
            Console.Write("\nEnter menu item description: ");
            return Console.ReadLine();
        }

        public List<string> CollectAndReturnIngredientList()
        {
            List<string> ingredients = new List<string>();
            bool confirmed = false;
            while (!confirmed)
            {
                Console.Write("\nEnter ingredients separated by a comma (Ex: flour, water, sugar): ");
                ingredients = Console.ReadLine().Split(',').ToList<string>();
                for (int i = 0; i < ingredients.Count; i++)
                {
                    ingredients[i] = ingredients[i].Trim();
                }
                Console.WriteLine("\nAre these the correct ingredients?");
                foreach (string ingredient in ingredients)
                {
                    Console.WriteLine('\t' + ingredient);
                }
                string yOrN = CollectAndReturnYOrN();
                if (yOrN == "y")
                {
                    confirmed = true;
                }
            }
            return ingredients;
        }

        public double CollectAndReturnPrice()
        {
            Console.Write("\nEnter the price for this item: ");
            bool isNumber = double.TryParse(Console.ReadLine(), out double price);
            while (!isNumber)
            {
                Console.Write("\nPlease enter a number: ");
                isNumber = double.TryParse(Console.ReadLine(), out price);
            }
            return price;
        }


        public void DisplayItem(MenuItem item)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                $"\tNumber: {item.Number}\n" +
                $"\tName: {item.Name}\n" +
                $"\tDescription: {item.Description}\n" +
                $"\tIngredients: {item.ReturnIngredientsListAsString()}\n" +
                $"\tPrice: ${item.Price}\n" +
                $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public string CollectAndReturnYOrN()
        {
            bool isYesOrNo = false;
            while (!isYesOrNo)
            {
                Console.Write("\nEnter y for yes or n for no: ");
                string inputYesOrNo = Console.ReadLine().ToLower();
                if (inputYesOrNo == "y" || inputYesOrNo == "n")
                {
                    return inputYesOrNo;
                }
            }
            return null;
        }

        public int CollectAndReturnPosWholeNum()
        {
            string inputString = Console.ReadLine();
            //check if input is positive, whole number
            while (!inputString.All(char.IsDigit))
            {
                Console.WriteLine("\nMenu item numbers must be a positive whole number.");
                Console.Write("Enter menu item number: ");
                inputString = Console.ReadLine();
            }
            return int.Parse(inputString);
        }

        public void SeedMenuList()
        {
            _repo.AddMenuItem(new MenuItem(
                001,
                "GRILLED CHEESE SANDWICH",
                "The classic toasted cheese sandwich with a side of tomato soup",
                 new List<string> { "bread", "cheese", "butter" },
                7.99));
            _repo.AddMenuItem(new MenuItem(
                002,
                "BROCCOLI CHEDDAR SOUP",
                "A hearty bowl of soup with a mini loaf of sourdough bread",
                new List<string> { "broccoli", "cheddar cheese", "milk", "other stuff" },
                6.99));
            _repo.AddMenuItem(new MenuItem(
                003,
                "BAGEL & CREAM CHEESE",
                "Your choice of a plain, blueberry, or everything bagel",
                new List<string> { "flour", "water", "yeast", "everything else" },
                3.49));
        }
    }
}
