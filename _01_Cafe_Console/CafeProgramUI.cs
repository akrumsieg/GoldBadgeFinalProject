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
            bool continueRunning = true;


        }

        public void RunMenu()
        {
            Console.WriteLine("What would you like to do?\n" +
                "1. Add menu item\n" +
                "2. Display all menu items\n" +
                "3. Update a menu item\n" +
                "4. Delete a menu item\n" +
                "9. Exit");

            //SWITCH CASE
        }

        public int CollectAndReturnNumber()
        {
        AssignNumber:
            Console.WriteLine("Enter menu item number: ");
            //string inputString = Console.ReadLine();
            //check if input is positive, whole number
            //while (!inputString.All(char.IsDigit))
            //{
            //    Console.WriteLine("Menu item numbers must be a positive whole number.");
            //    Console.WriteLine("Enter menu item number: ");
            //    inputString = Console.ReadLine();
            //}
            //int inputInt = int.Parse(inputString);
            //check if item number is already assigned
            int inputInt = CollectAndReturnPosWholeNum();
            if (_repo.FindItemByNumber(inputInt) != null)
            {
                Console.WriteLine("That item number is already assigned. Please enter an available number.");
                goto AssignNumber;
            }
            else
            {
                return inputInt;
            }
        }

        public string CollectAndReturnName()
        {
            Console.WriteLine("Enter menu item name: ");
            return Console.ReadLine().ToUpper();
        }

        public string CollectAndReturnDescription()
        {
            Console.WriteLine("Enter menu item description: ");
            return Console.ReadLine();
        }

        public List<string> CollectAndReturnIngredientList()
        {
            List<string> ingredients = new List<string>();
            bool confirmed = false;
            while (!confirmed)
            {
                Console.WriteLine("Enter ingredients separated by a comma (Ex: flour, water, sugar): ");
                ingredients = Console.ReadLine().Split(',').ToList<string>();
                for (int i = 0; i < ingredients.Count; i++)
                {
                    ingredients[i] = ingredients[i].Trim();
                }
                Console.WriteLine("Are these the correct ingredients?");
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
            Console.WriteLine("Enter the price for this item: ");
            bool isNumber = double.TryParse(Console.ReadLine(), out double price);
            while (!isNumber)
            {
                Console.WriteLine("Please enter a number.");
                isNumber = double.TryParse(Console.ReadLine(), out price);
            }
            return price;
        }

        public void AddMenuItem()
        {
            int number = CollectAndReturnNumber();
            string name = CollectAndReturnName();
            string description = CollectAndReturnDescription();
            List<string> ingredients = CollectAndReturnIngredientList();
            double price = CollectAndReturnPrice();

            MenuItem item = new MenuItem(number, name, description, ingredients, price);
            if (_repo.AddMenuItem(item))
            {
                Console.WriteLine("Item was added successfully.");
            }
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

        public void DisplayAllItems()
        {
            foreach (MenuItem item in _repo.ReturnMenuList())
            {
                DisplayItem(item);
            }
        }

        public void UpdateItem()
        {
            DisplayAllItems();
            Console.WriteLine("Enter the number of the item you would like to update: ");
            int originalItemNumber = CollectAndReturnPosWholeNum();
            MenuItem updatedItem = new MenuItem();
            updatedItem.Number = CollectAndReturnNumber();
            updatedItem.Name = CollectAndReturnName();
            updatedItem.Description = CollectAndReturnDescription();
            updatedItem.Ingredients = CollectAndReturnIngredientList();
            updatedItem.Price = CollectAndReturnPrice();
            _repo.UpdateByNumber(originalItemNumber, updatedItem);
        }

        public void DeleteItem()
        {
            DisplayAllItems();
            Console.WriteLine("Enter the number of the item you would like to DELETE: ");
            int itemNumToDelete = CollectAndReturnPosWholeNum();
            Console.WriteLine("Are you sure you want to DELETE this item: ");
            DisplayItem(_repo.FindItemByNumber(itemNumToDelete));
            string yOrN = CollectAndReturnYOrN();
            if (yOrN == "y")
            {
                if (_repo.DeleteByNumber(itemNumToDelete))
                {
                    Console.WriteLine("Item was deleted successfully.");
                }
            }
        }

        public string CollectAndReturnYOrN()
        {
            bool isYesOrNo = false;
            while (!isYesOrNo)
            {
                Console.WriteLine("Enter y for yes or n for no: ");
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
                Console.WriteLine("Menu item numbers must be a positive whole number.");
                Console.WriteLine("Enter menu item number: ");
                inputString = Console.ReadLine();
            }
            return int.Parse(inputString);
        }

        public void SeedMenuList()
        {
            _repo.AddMenuItem(new MenuItem(
                001,
                "Grilled Cheese Sandwich",
                "The classic toasted cheese sandwich with a side of tomato soup",
                 new List<string>{ "bread", "cheese", "butter" },
                7.99));
            _repo.AddMenuItem(new MenuItem(
                002,
                "Broccoli Cheddar Soup",
                "A hearty bowl of soup with a mini loaf of sourdough bread",
                new List<string> { "broccoli", "cheddar cheese", "milk", "other stuff" },
                6.99));
            _repo.AddMenuItem(new MenuItem(
                003,
                "Bagel & Cream Cheese",
                "Your choice of a plain, blueberry, or everything bagel",
                new List<string> { "flour", "water", "yeast", "everything else" },
                3.49));
        }
    }
}
