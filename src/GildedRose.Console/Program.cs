using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;

        private static void Main(string[] args)
        {
            System.Console.WriteLine("Starting Gilded Rose!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }

            };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateQualityFor(item);
            }
        }

        private void UpdateQualityForLegendaryItem(Item item)
        {
            return;
        }

        private void UpdateQualityForEventTicket(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;

                if (item.SellIn < 11)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }
            item.SellIn--;
            if (item.SellIn < 0)
                item.Quality = 0;            
        }

        private void UpdateQualityForVintageItem(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Quality < 50)
                {
                    item.Quality++;
                }
            }
        }

        private void UpdateQualityForConjuredItem(Item item)
        {
            item.Quality = Math.Max(item.Quality - 2, 0);

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = Math.Max(item.Quality - 2, 0);
            }            
        }

        private void UpdateQualityForNormalItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Quality > 0)
                {
                    item.Quality--;
                }
            }
        }

        public void UpdateQualityFor(Item item)
        {
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                UpdateQualityForLegendaryItem(item);
            }
            else if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                UpdateQualityForEventTicket(item);
            }
            else if (item.Name == "Aged Brie")
            {
                UpdateQualityForVintageItem(item);
            }
            else if (item.Name == "Conjured Mana Cake")
            {
                UpdateQualityForConjuredItem(item);
            }
            else
            {
                UpdateQualityForNormalItem(item);
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
