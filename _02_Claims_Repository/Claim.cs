using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Claims_Repository
{
    //ClaimType enum
    public enum TypeOfClaim { Car = 1, Home, Theft}
    public class Claim
    {
        //constructors
        public Claim() { }
        public Claim(int claimID, TypeOfClaim claimType, string description, double claimAmount, DateTime dateOfIncident, DateTime dateOfClaim)
        {
            ClaimID = claimID;
            ClaimType = claimType;
            Description = description;
            ClaimAmount = claimAmount;
            DateOfIncident = dateOfIncident;
            DateOfClaim = dateOfClaim;
        }

        //properties
        public int ClaimID { get; set; }
        public TypeOfClaim ClaimType { get; set; }
        public string Description { get; set; }
        public double ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; set; }
        public bool IsValid { get
            {
                TimeSpan incidentToClaimTimeSpan = (DateOfClaim - DateOfIncident);
                if (incidentToClaimTimeSpan.TotalDays <= 30) return true;
                else return false;
            }
        }

        //methods
    }
}
