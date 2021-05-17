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
            return _menu.Count() > originalCount ? true : false;
        }

        //READ
        public List<MenuItem> ReturnMenuList()
        {
            return _menu;
        }

        public MenuItem FindItemByNumber(int number)
        {
            foreach (MenuItem item in _menu)
            {
                if (item.Number == number) return item;
            }
            return null;
        }

        //UPDATE
        public void UpdateByNumber(int number, MenuItem updatedItem)
        {
            MenuItem itemToUpdate = FindItemByNumber(number);
            itemToUpdate.Number = updatedItem.Number;
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Description = updatedItem.Description;
            itemToUpdate.Ingredients = updatedItem.Ingredients;
            itemToUpdate.Price = updatedItem.Price;
        }

        //DELETE
        public bool DeleteByNumber(int number)
        {
            return _menu.Remove(FindItemByNumber(number));
        }
    }
}
