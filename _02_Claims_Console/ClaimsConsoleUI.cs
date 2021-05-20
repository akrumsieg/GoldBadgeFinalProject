using _02_Claims_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Claims_Console
{
    public class ClaimsConsoleUI
    {
        private readonly ClaimsRepository _repo = new ClaimsRepository();
        private readonly _00_Helper_Methods.HelperMethods helperMethods = new _00_Helper_Methods.HelperMethods();

        public void Run()
        {
            SeedClaimsQueue();
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.WriteLine("What would you like to do?\n" +
                    "1. See all claims in queue\n" +
                    "2. Take care of next claim in queue\n" +
                    "3. Submit a new claim to queue\n" +
                    "4. Update a claim in the queue\n" +
                    "9. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        SeeAllClaims();
                        break;
                    case "2":
                        CompleteClaim();
                        break;
                    case "3":
                        SubmitNewClaim();
                        break;
                    case "4":
                        UpdateClaim();
                        break;
                    case "9":
                        continueRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. \n" +
                            "Please enter the number corresponding to the action you would like to do.\n" +
                            "Ex: Type 1 and press the ENTER key to see all claims in the queue.\n");
                        break;
                }
            }
        }

        //MENU OPTION 1: See all claims in queue
        public void SeeAllClaims()
        {
            Console.Clear();
            if (QueueIsEmpty()) return;
            Console.WriteLine("These are the claims currently in the queue:");
            foreach (Claim claim in _repo.ReturnQueue())
            {
                DisplayOneClaim(claim);
            }
            Console.WriteLine("");
        }

        //MENU OPTION 2: Take care of next claim in queue
        public void CompleteClaim()
        {
            Console.Clear();
            if (QueueIsEmpty()) return;
            Console.WriteLine("This is the next claim in the queue:");
            DisplayOneClaim(_repo.ReturnQueue().Peek());
            Console.WriteLine("\nHas this claim been COMPLETELY RESOLVED? (y/n): ");
            if (helperMethods.CollectAndReturnYOrN() == "y")
            {
                Console.Clear();
                _repo.DequeueNextClaim();
                Console.WriteLine("The claim has been removed from the queue.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The claim will remain in the queue.");
            }
            helperMethods.ReturnToMenu();
        }

        //MENU OPTION 3: Submit a new claim to queue
        public void SubmitNewClaim()
        {
            Console.Clear();
            Claim newClaim = new Claim();
            newClaim.ClaimID = CollectAndReturnID();
            newClaim.ClaimType = CollectAndReturnClaimType();
            newClaim.Description = CollectAndReturnDescription();
            newClaim.ClaimAmount = CollectAndReturnClaimAmount();
            newClaim.DateOfIncident = CollectAndReturnDateOfIncident();
            newClaim.DateOfClaim = CollectAndReturnDateOfClaim();

            if (_repo.AddClaimToQueue(newClaim)) Console.WriteLine("\nClaim was added to the queue.");
            helperMethods.ReturnToMenu();
        }


        //MENU OPTION 4: Update a claim in the queue
        public void UpdateClaim()
        {
            Console.Clear();
            if (QueueIsEmpty()) return;
            SeeAllClaims();
            Console.Write("Enter the ID of the claim you would like to update: ");
            int originalID = helperMethods.CollectAndReturnPosWholeNum();
            bool isNull = true;
            while (isNull)
            {
                if (_repo.FindClaimByNumber(originalID) == null)
                {
                    Console.Write("\nThat claim ID does not currently exist.\n" +
                        "Please enter the ID of an existing claim to update: ");
                    originalID = helperMethods.CollectAndReturnPosWholeNum();
                }
                else isNull = false;
            }
            Claim updatedClaim = new Claim(
                CollectAndReturnID(),
                CollectAndReturnClaimType(),
                CollectAndReturnDescription(),
                CollectAndReturnClaimAmount(),
                CollectAndReturnDateOfIncident(),
                CollectAndReturnDateOfClaim());
            _repo.UpdateClaimByNumber(originalID, updatedClaim);
            Console.Clear();
            Console.WriteLine("Claim was updated successfully.");
            helperMethods.ReturnToMenu();
        }

        //helper methods
        public void DisplayOneClaim(Claim claim)
        {
            Console.WriteLine($"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                $"\tID: {claim.ClaimID}\n" +
                $"\tType: {claim.ClaimType}\n" +
                $"\tDescription: {claim.Description}\n" +
                $"\tAmount: ${claim.ClaimAmount}\n" +
                $"\tDate of Incident: {claim.DateOfIncident}\n" +
                $"\tDate of Claim: {claim.DateOfClaim}\n" +
                $"\tClaim Submitted on Time: {claim.IsValid}\n" +
                $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public int CollectAndReturnID()
        {
            Console.Write("\nEnter claim ID: ");
        AssignNumber:
            int inputInt = helperMethods.CollectAndReturnPosWholeNum();
            if (_repo.FindClaimByNumber(inputInt) != null)
            {
                Console.Write("\nThat item number is already assigned. Please enter an available number: ");
                goto AssignNumber;
            }
            else
            {
                return inputInt;
            }
        }

        public TypeOfClaim CollectAndReturnClaimType()
        {
            Console.WriteLine("\nEnter claim type: \n" +
                "1. Car\n" +
                "2. Home\n" +
                "3. Theft");
        CollectInput:
            int intInput = helperMethods.CollectAndReturnPosWholeNum();
            while (intInput == 0 || intInput > 3)
            {
                Console.Write("\nPlease enter 1, 2, or 3: ");
                goto CollectInput;
            }
            return (TypeOfClaim)intInput;
        }

        public string CollectAndReturnDescription()
        {
            Console.Write("\nDescribe the claim: ");
            return Console.ReadLine();
        }

        public double CollectAndReturnClaimAmount()
        {
            Console.Write("\nEnter claim amount: ");
            return helperMethods.CollectAndReturnDollarAmount();
        }

        public DateTime CollectAndReturnDateOfIncident()
        {
            Console.Write("\nEnter date of INCIDENT (yyyy/mm/dd): ");
            return helperMethods.CollectAndReturnDate();
        }

        public DateTime CollectAndReturnDateOfClaim()
        {
            Console.Write("\nEnter date of CLAIM (yyyy/mm/dd): ");
            return helperMethods.CollectAndReturnDate();
        }

        public bool QueueIsEmpty()
        {
            if (_repo.ReturnQueueCount() == 0)
            {
                Console.Write("There are currently no claims in the queue.");
                helperMethods.ReturnToMenu();
                return true;
            }
            else return false;
        }

        public void SeedClaimsQueue()
        {
            _repo.AddClaimToQueue(new Claim(
                1,
                TypeOfClaim.Car,
                "Crashed into tree at Main St. and 1st St.",
                8021.12,
                new DateTime(2021, 01, 01),
                new DateTime(2021, 02, 01)));
            _repo.AddClaimToQueue(new Claim(
                2,
                TypeOfClaim.Home,
                "Flooded basement",
                7316.00,
                new DateTime(2021, 02, 01),
                new DateTime(2021, 03, 01)));
            _repo.AddClaimToQueue(new Claim(
                415,
                TypeOfClaim.Home,
                "Tree crashed through roof",
                43000.00,
                new DateTime(2021, 05, 01),
                new DateTime(2021, 05, 02)));
        }
    }
}
