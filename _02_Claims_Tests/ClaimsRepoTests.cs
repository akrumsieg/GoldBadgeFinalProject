using _02_Claims_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _02_Claims_Tests
{
    [TestClass]
    public class ClaimsRepoTests
    {
        private ClaimsRepository _repo;
        private Claim _claim1;
        private Claim _claim2 = new Claim(
                2,
                TypeOfClaim.Home,
                "Flooded basement",
                2187637316.00,
                new DateTime(2021, 02, 01),
                new DateTime(2021, 03, 01));

        [TestInitialize]
        public void Arrange()
        {
            _repo = new ClaimsRepository();
            _claim1 = new Claim(
                1,
                TypeOfClaim.Car,
                "Crashed into tree at Main St. and 1st St.",
                8021.23,
                new DateTime(2021, 01, 01),
                new DateTime(2021, 02, 01));
            _repo.AddClaimToQueue(_claim1);
        }

        [TestMethod]
        public void AddClaimToQueue_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.AddClaimToQueue(_claim2));
        }

        [TestMethod]
        public void ReturnQueue_ShouldReturnCorrectTypeOfQueue()
        {
            Assert.IsInstanceOfType(_repo.ReturnQueue(), typeof(Queue<Claim>));
        }

        [TestMethod]
        public void UpdateClaimByNumber_ShouldReturnUpdatedValue()
        {
            _repo.UpdateClaimByNumber(1, _claim2);
            Assert.AreEqual(2, _claim2.ClaimID);
        }

        [TestMethod]
        public void DequeueNextClaim_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.DequeueNextClaim());
        }

        [TestMethod]
        public void FindClaimByNumber_ShouldReturnCorrectClaim()
        {
            Assert.AreEqual(_repo.FindClaimByNumber(1), _claim1);
        }

        [TestMethod]
        public void ReturnQueueCount_ShouldReturnCorrectInt()
        {
            Assert.AreEqual(_repo.ReturnQueueCount(), 1);
        }
    }
}
