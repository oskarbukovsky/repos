using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0090 // Použít new(...)

namespace MyApp
{
    internal class Program
    {
        public static string ReadLine()
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                return String.Empty;
            }
            return input;
        }
        class Item
        {
            public int Weight;
            public int Value;
            public float Ratio;
            public Item(int maxWeight = 100, int maxValue = 100)
            {
                Random rand = new Random();
                Weight = rand.Next(1, maxWeight + 1);
                Value = rand.Next(1, maxValue + 1); ;
                Ratio = (float)Value / (float)Weight;
            }
        }

        class Backpack
        {
            public int MaxWeight;
            public int Weight;
            public int Value;
            public List<Item> Items = new List<Item>();
            public Backpack(int maxWeight = 100)
            {
                MaxWeight = maxWeight;
                Weight = 0;
                Value = 0;
            }
            public void Add(Item item)
            {
                if (Weight + item.Weight <= MaxWeight)
                {
                    Items.Add(item);
                    Weight += item.Weight;
                    Value += item.Value;
                }
                else
                {
                    throw new Exception("Backpack maximum capacity exceeded!");
                }
            }
        }

        static void Main()
        {
            int M = Convert.ToInt32(ReadLine());
            int n = Convert.ToInt32(ReadLine());

            //Console.WriteLine($"{M}, {n}");

            List <Item> Items = new List<Item>();
            for (int i = 0; i < n; i++)
            {
                Items.Add(new Item(M));
            }

            Console.WriteLine("Itemy:");
            foreach(Item item in Items.OrderByDescending(e => e.Ratio).ThenBy(e => e.Weight))
            {
                Console.WriteLine("  " + item.Ratio.ToString("0.00") + "\t" + item.Value+ "\t" + item.Weight + " ");
            }

            Backpack backpack = new Backpack(M);
            try
            {
                while (true)
                {
                    Item best_ratio = Items.Where(e => e.Weight <= backpack.MaxWeight - backpack.Weight).OrderByDescending(e => e.Ratio).ThenBy(e => e.Weight).First();
                    backpack.Add(best_ratio);
                    Items.Remove(best_ratio);
                }
            }
            catch
            {
                Console.WriteLine("Backpack (" + backpack.Value + ", " + backpack.Weight + "):");
                foreach (Item item in backpack.Items.OrderByDescending(e => e.Ratio).ThenBy(e => e.Weight))
                {
                    Console.WriteLine("  " + item.Ratio.ToString("0.00") + "\t" + item.Value + "\t" + item.Weight + " ");
                }
            }

            //System.Diagnostics.Debugger.Break();
            Console.ReadKey();
        }
    }
}