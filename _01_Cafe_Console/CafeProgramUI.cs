using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Cafe_Console
{
    public class CafeProgramUI
    {
        //MenuRepository _repo = new MenuRepository();
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
            Console.WriteLine("Enter menu item number: ");
            string inputString = Console.ReadLine();
            while (!inputString.All(char.IsDigit))
            {
                Console.WriteLine("Menu item numbers must be a positive whole number.");
                Console.WriteLine("Enter menu item number: ");
                inputString = Console.ReadLine();
            }
            int inputInt = int.Parse(inputString);
        }

        public bool CheckNumAvailability(int number)
        {
            foreach ()
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
                bool isYesOrNo = false;
                while (!isYesOrNo)
                {
                    Console.WriteLine("Enter y for yes or n for no: ");
                    string inputYesOrNo = Console.ReadLine().ToLower();
                    if (inputYesOrNo == "y")
                    {
                        isYesOrNo = true;
                        confirmed = true;
                    }
                    else if (inputYesOrNo == "n")
                    {
                        isYesOrNo = true;
                    }
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


        }
    }
}
