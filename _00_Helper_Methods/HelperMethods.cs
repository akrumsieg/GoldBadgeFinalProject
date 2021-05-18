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
    }
}
