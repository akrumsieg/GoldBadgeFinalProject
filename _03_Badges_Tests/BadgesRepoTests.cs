using _03_Badges_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _03_Badges_Tests
{
    [TestClass]
    public class BadgesRepoTests
    {
        private BadgesRepository _repo;
        private Badge _badge1;
        private Badge _badge2 = new Badge(002, new List<string> { "A1", "A2", "B1" });

        [TestInitialize]
        public void Arrange()
        {
            _repo = new BadgesRepository();
            _badge1 = new Badge(001, new List<string> { "C1", "C2" });
            _repo.AddBadge(_badge1);
        }

        [TestMethod]
        public void AddBadge_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.AddBadge(_badge2));
        }

        [TestMethod]
        public void ReturnBadgeDictionary_ShouldReturnCorrectTypeOfDictionary()
        {
            Assert.IsInstanceOfType(_repo.ReturnBadgeDictionary(), typeof(Dictionary<int, List<string>>));
        }

        [TestMethod]
        public void UpdateBadgeAccess_ShouldReturnUpdatedAccessList()
        {
            List<string> updatedList = new List<string> { "D1", "D2" };
            _repo.UpdateBadgeAccess(1, updatedList);
            Assert.AreEqual(updatedList, _repo.ReturnBadgeDictionary()[1]);
        }

        [TestMethod]
        public void RemoveAllAccess_ShouldReturnTrue()
        {
            ;
            Assert.IsTrue(_repo.RemoveAllAccess(1));
        }
    }
}
