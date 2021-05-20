using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Cafe_Repository
{
    public class MenuRepository
    {
        private List<MenuItem> _menu = new List<MenuItem>();

        //CRUD Methods
        //CREATE
        public bool AddMenuItem(MenuItem item)
        {
            int originalCount = _menu.Count();
            _menu.Add(item);
            return _menu.Count() > originalCount;
        }

        //READ
        public List<MenuItem> ReturnMenuList()
        {
            return _menu;
        }


        //UPDATE
        public void UpdatedItem(MenuItem originalItem, MenuItem updatedItem)
        {
            originalItem.Number = updatedItem.Number;
            originalItem.Name = updatedItem.Name;
            originalItem.Description = updatedItem.Description;
            originalItem.Ingredients = updatedItem.Ingredients;
            originalItem.Price = updatedItem.Price;
        }

        //DELETE
        public bool DeleteByNumber(int number)
        {
            return _menu.Remove(FindItemByNumber(number));
        }


        //helper methods
        public int ReturnListCount()
        {
            return _menu.Count();
        }

        public MenuItem FindItemByNumber(int number)
        {
            foreach (MenuItem item in _menu)
            {
                if (item.Number == number) return item;
            }
            return null;
        }
    }
}
