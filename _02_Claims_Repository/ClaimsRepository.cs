using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Claims_Repository
{
    public class ClaimsRepository
    {
        private readonly Queue<Claim> _claimsQueue = new Queue<Claim>();

        //CRUD Methods
        //CREATE
        public bool AddClaimToQueue(Claim claim)
        {
            int originalCount = _claimsQueue.Count();
            _claimsQueue.Enqueue(claim);
            return _claimsQueue.Count() > originalCount;
        }

        //READ
        public Queue<Claim> ReturnQueue()
        {
            return _claimsQueue;
        }

        //UPDATE
        public void UpdateClaimByNumber(int claimNum, Claim updatedClaim)
        {
            Claim originalClaim = FindClaimByNumber(claimNum);
            originalClaim.ClaimID = updatedClaim.ClaimID;
            originalClaim.ClaimType = updatedClaim.ClaimType;
            originalClaim.Description = updatedClaim.Description;
            originalClaim.ClaimAmount = updatedClaim.ClaimAmount;
            originalClaim.DateOfIncident = updatedClaim.DateOfIncident;
            originalClaim.DateOfClaim = updatedClaim.DateOfClaim;
        }

        //DELETE
        public bool DequeueNextClaim()
        {
            int originalCount = _claimsQueue.Count();
            _claimsQueue.Dequeue();
            return _claimsQueue.Count() < originalCount;
        }

        //helper methods
        public Claim FindClaimByNumber(int claimNum)
        {
            foreach (Claim claim in _claimsQueue)
            {
                if (claim.ClaimID == claimNum) return claim;
            }
            return null;
        }

        public int ReturnQueueCount()
        {
            return _claimsQueue.Count();
        }
    }
}
