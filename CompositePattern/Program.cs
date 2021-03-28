using System;
using System.Collections;

// Iterator : headFirst
namespace CompositePattern
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PancakeHouseMenu pancakeHouseMenu = new();
            DinerMenu dinerMenu = new();

            Waitress waitress = new(pancakeHouseMenu, dinerMenu);

            waitress.PrintMenu();
        }
    }

    public class MenuItem
    {
        private string name;
        private string description;
        private bool vegetarian;
        private double price;

        public MenuItem(string name, string description, bool vegetarian, double price)
        {
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool Vegetarian { get => vegetarian; set => vegetarian = value; }
        public double Price { get => price; set => price = value; }
    }

    public class PancakeHouseMenu
    {
        private ArrayList menuItems;

        public PancakeHouseMenu()
        {
            menuItems = new ArrayList();
            addItem("Pancake set1", "desc1....", true, 2.99);
            addItem("Pancake set2", "desc2....", false, 2.99);
            addItem("Pancake set3", "desc3....", true, 3.99);
            addItem("Pancake set4", "desc4....", true, 3.59);
        }

        public void addItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }

        //public ArrayList MenuItems { get => menuItems; }

        public Iterator createIterator()
        {
            return new PancakeHouseIterator(menuItems);
        }
    }

    public class DinerMenu
    {
        private static int MAX_ITEMS = 6;
        private int numberOfItems = 0;
        private MenuItem[] menuItems;

        public DinerMenu()
        {
            menuItems = new MenuItem[MAX_ITEMS];

            addItem("DinerMenu set1", "desc1....", true, 2.99);
            addItem("DinerMenu set2", "desc2....", false, 2.99);
            addItem("DinerMenu set3", "desc3....", false, 3.99);
            addItem("DinerMenu set4", "desc4....", false, 3.05);
        }

        private void addItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new(name, description, vegetarian, price);

            if (numberOfItems >= MAX_ITEMS)
            {
                Console.WriteLine("Fulled, No more addition");
            }
            else
            {
                menuItems[numberOfItems] = menuItem;
                numberOfItems++;
            }
        }

        //public MenuItem[] MenuItems { get => menuItems; }

        public Iterator createIterator()
        {
            return new DinerMenuIterator(menuItems);
        }
    }

    public interface Iterator
    {
        bool HasNext();

        Object next();
    }

    public class DinerMenuIterator : Iterator
    {
        private MenuItem[] items;
        private int position = 0;

        public DinerMenuIterator(MenuItem[] items)
        {
            this.items = items;
        }

        public bool HasNext()
        {
            if (position >= items.Length || items[position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object next()
        {
            MenuItem menuItem = items[position];
            position++;
            return menuItem;
        }
    }

    public class PancakeHouseIterator : Iterator
    {
        private ArrayList items;
        private int position = 0;

        public PancakeHouseIterator(ArrayList items)
        {
            this.items = items;
        }

        public bool HasNext()
        {
            if (position >= items.Count || items[position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object next()
        {
            MenuItem menuItem = (MenuItem)items[position++];
            return menuItem;
        }

        public Iterator createIterator()
        {
            return new PancakeHouseIterator(items);
        }
    }

    public class Waitress
    {
        private PancakeHouseMenu pancakeHouseMenu;
        private DinerMenu dinerMenu;

        public Waitress(PancakeHouseMenu pancakeHouseMenu, DinerMenu dinerMenu)
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
        }

        public void PrintMenu()
        {
            Iterator pancakeIterator = pancakeHouseMenu.createIterator();
            Iterator dinerIterator = dinerMenu.createIterator();

            Console.WriteLine("Morning menu : ");
            PrintMenu(pancakeIterator);
            Console.WriteLine("Afternoon menu : ");
            PrintMenu(dinerIterator);
        }

        private void PrintMenu(Iterator iterator)
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.next();
                Console.Write(menuItem.Name + ", ");
                Console.Write(menuItem.Price + ", ");
                Console.WriteLine(menuItem.Description);
            }
        }
    }
}