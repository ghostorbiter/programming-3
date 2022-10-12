using System;
using System.Text;

namespace Lab5BEn
{
    public class PostBox
    {
        private string Location;
        private int ArraySize;
        private Item[] Array;
        public PostBox(string location, Item[] items)
        {
            Location = location;
            if (items != null)
                ArraySize = items.GetLength(0);
            Array = new Item[ArraySize];
            Array = items;
        }

        public int GetItemsCount()
        {
            return ArraySize;
        }

        public Item GetItem(int i)
        {
            if (i >= ArraySize)
                return null;
            return Array[i];
        }

        public void Print()
        {
            Console.WriteLine($"Postbox {Location}");
            Console.WriteLine("Items: ");
            for (int i = 0; i < ArraySize; ++i)
            {
                Console.WriteLine($"item {i}: " + Array[i].ToString());
            }
        }
    }
}