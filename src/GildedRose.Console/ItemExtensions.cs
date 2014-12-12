using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GildedRose.Console
{
    public static class ItemExtensions
    {
        public const int MAX_QUALITY = 50;

        private static void UpdateQualityForLegendaryItem(this Item item)
        {
            return;
        }

        private static void UpdateQualityForEventTicket(this Item item)
        {
            if (item.Quality < MAX_QUALITY)
            {
                item.Quality++;

                if (item.SellIn < 11)
                {
                    if (item.Quality < MAX_QUALITY)
                    {
                        item.Quality++;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < MAX_QUALITY)
                    {
                        item.Quality++;
                    }
                }
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private static void UpdateQualityForVintageItem(this Item item)
        {
            item.Quality = Math.Min(MAX_QUALITY, item.Quality + 1);

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = Math.Min(MAX_QUALITY, item.Quality + 1);
            }
        }

        private static void UpdateQualityForConjuredItem(this Item item)
        {
            item.Quality = Math.Max(item.Quality - 2, 0);

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = Math.Max(item.Quality - 2, 0);
            }            
        }

        private static void UpdateQualityForNormalItem(this Item item)
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

        public static void UpdateQuality(this Item item)
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
}
