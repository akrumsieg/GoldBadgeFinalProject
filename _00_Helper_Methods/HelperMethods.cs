using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Helper_Methods
{
    public class HelperMethods
    {
        public int CollectAndReturnPosWholeNum()
        {
            string inputString = Console.ReadLine();
            //check if input is positive, whole number
            while (!inputString.All(char.IsDigit))
            {
                Console.WriteLine("\nYou must enter a positive whole number.");
                Console.Write("Enter number: ");
                inputString = Console.ReadLine();
            }
            return int.Parse(inputString);
        }

        public double CollectAndReturnDollarAmount()
        {
        Beginning:
            string inputString = Console.ReadLine();
            if (!Double.TryParse(inputString, out double inputDouble))
            {
                Console.Write("\nPlease enter a positive number: ");
                goto Beginning;
            }
            if (inputDouble < 0)
            {
                Console.Write("\nPlease enter a positive number: ");
                goto Beginning;
            }
            string twoDecimalString = inputDouble.ToString("F");
            double twoDecimalDouble = Convert.ToDouble(twoDecimalString);
            return twoDecimalDouble;
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

        public DateTime CollectAndReturnDate()
        {
        Beginning:
            string inputString = Console.ReadLine();
            if (!DateTime.TryParse(inputString, out DateTime inputDate))
            {
                Console.Write("Please enter a valid date (yyyy/mm/dd): ");
                goto Beginning;
            }
            return inputDate;
        }

        public void ReturnToMenu()
        {
            Console.Write("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
        }

        public void DisplayStringsList(List<string> list)
        {
            foreach (string item in list)
            {
                Console.WriteLine($"\t{item}");
            }
        }
    }
}
