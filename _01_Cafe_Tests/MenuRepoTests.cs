using _01_Cafe_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _01_Cafe_Tests
{
    [TestClass]
    public class MenuRepoTests
    {
        //variables for arrange method
        private MenuRepository _repo;
        private MenuItem _menuItem1;
        private MenuItem _menuItem2 = new MenuItem(
                002,
                "BAGEL & CREAM CHEESE",
                "Your choice of a plain, blueberry, or everything bagel",
                new List<string> { "flour", "water", "yeast", "everything else" },
                3.49);

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepository();
            _menuItem1 = new MenuItem(
                001,
                "GRILLED CHEESE SANDWICH",
                "The classic toasted cheese sandwich with a side of tomato soup",
                new List<string> { "bread", "cheese", "butter" },
                7.99);
            _repo.AddMenuItem(_menuItem1);
        }

        [TestMethod]
        public void AddMenuItem_ShouldReturnTrue()
        {
            //act and assert
            Assert.IsTrue(_repo.AddMenuItem(_menuItem2));
        }

        [TestMethod]
        public void ReturnMenuList_ShouldReturnCorrectTypeOfList()
        {
            Assert.IsInstanceOfType(_repo.ReturnMenuList(), typeof(List<MenuItem>));
        }

        [TestMethod]
        public void UpdateByNumber_ShouldReturnUpdatedValue()
        {
            _repo.UpdateByNumber(1, new MenuItem(
                3,
                "HAM & CHEESE SANDWICH",
                "description description lorem ipsum",
                new List<string> { "ham, cheese, bread" },
                6.99));
            Assert.AreEqual(_menuItem1.Number, 3);
        }

        [TestMethod]
        public void DeleteByNumber_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.DeleteByNumber(1));
        }
    }
}
