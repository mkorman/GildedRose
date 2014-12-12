using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        private Program target;

        [SetUp]
        public void SetUp()
        {
            target = new Program()
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
        }

        [Test]
        public void TestVestDegradation()
        {
            var vest = target.Items.First(i => i.Name.Contains("Dexterity"));

            Assert.That(vest.SellIn, Is.EqualTo(10));
            Assert.That(vest.Quality, Is.EqualTo(20));

            // Aging before expiry date
            for (int i = 1; i <= 10; i++)
            {
                target.UpdateQuality();
                Assert.That(vest.SellIn, Is.EqualTo(10 - i));
                Assert.That(vest.Quality, Is.EqualTo(20 - i));
            }

            // Aging after expiry date
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(vest.SellIn, Is.EqualTo(0 - i));
                Assert.That(vest.Quality, Is.EqualTo(10 - 2*i));
            }

            // Aging after zero quality
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(vest.SellIn, Is.EqualTo(-5 - i));
                Assert.That(vest.Quality, Is.EqualTo(0));
            }
        }

        [Test]
        public void TestElixirDegradation()
        {
            var elixir = target.Items.First(i => i.Name.Contains("Elixir"));

            Assert.That(elixir.SellIn, Is.EqualTo(5));
            Assert.That(elixir.Quality, Is.EqualTo(7));


            // Aging before expiry date
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(elixir.SellIn, Is.EqualTo(5 - i));
                Assert.That(elixir.Quality, Is.EqualTo(7 - i));
            }

            // Aging after expiry date
            for (int i = 1; i <= 1; i++)
            {
                target.UpdateQuality();
                Assert.That(elixir.SellIn, Is.EqualTo(0 - i));
                Assert.That(elixir.Quality, Is.EqualTo(2 - 2*i));
            }

            // Aging after zero quality
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(elixir.SellIn, Is.EqualTo(-1 - i));
                Assert.That(elixir.Quality, Is.EqualTo(0));
            }
        }

        [Test]
        public void TestLegendaryDegradation()
        {
            var vest = target.Items.First(i => i.Name.Contains("Sulfuras"));

            Assert.That(vest.SellIn, Is.EqualTo(0));
            Assert.That(vest.Quality, Is.EqualTo(80));

            // Aging before expiry date
            for (int i = 1; i <= 100; i++)
            {
                target.UpdateQuality();
                Assert.That(vest.SellIn, Is.EqualTo(0));
                Assert.That(vest.Quality, Is.EqualTo(80));
            }
        }

        [Test]
        public void TestAgedBrieDegradation()
        {
            var brie = target.Items.First(i => i.Name.Contains("Aged Brie"));

            Assert.That(brie.SellIn, Is.EqualTo(2));
            Assert.That(brie.Quality, Is.EqualTo(0));


            // Aging before expiry date
            for (int i = 1; i <= 2; i++)
            {
                target.UpdateQuality();
                Assert.That(brie.SellIn, Is.EqualTo(2 - i));
                Assert.That(brie.Quality, Is.EqualTo(0 + i));
            }

            // Aging after expiry date
            for (int i = 1; i <= 100; i++)
            {
                target.UpdateQuality();
                Assert.That(brie.SellIn, Is.EqualTo(0 - i));
                Assert.That(brie.Quality, Is.EqualTo(Math.Min (50, 2 + 2 * i)));
            }
        }

        [Test]
        public void TestConcertPassDegradation()
        {
            var pass = target.Items.First(i => i.Name.Contains("concert"));

            Assert.That(pass.SellIn, Is.EqualTo(15));
            Assert.That(pass.Quality, Is.EqualTo(20));

            // Aging up to 10 days before concert
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(pass.SellIn, Is.EqualTo(15 - i));
                Assert.That(pass.Quality, Is.EqualTo(20 + i));
            }

            // Aging up to 5 days before concert
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(pass.SellIn, Is.EqualTo(10 - i));
                Assert.That(pass.Quality, Is.EqualTo(25 + 2*i));
            }

            // Aging up to concert day
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(pass.SellIn, Is.EqualTo(5 - i));
                Assert.That(pass.Quality, Is.EqualTo(35 + 3 * i));
            }

            // Aging after concert
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(pass.SellIn, Is.EqualTo(0 - i));
                Assert.That(pass.Quality, Is.EqualTo(0));
            }
        }

        [Test]
        public void TestConjuredItemDegradation()
        {
            var conjuredItem = target.Items.First(i => i.Name.Contains("Conjured"));

            Assert.That(conjuredItem.SellIn, Is.EqualTo(3));
            Assert.That(conjuredItem.Quality, Is.EqualTo(6));

            // Aging before expiry date
            for (int i = 1; i <= 3; i++)
            {
                target.UpdateQuality();
                Assert.That(conjuredItem.SellIn, Is.EqualTo(3 - i));
                Assert.That(conjuredItem.Quality, Is.EqualTo(6 - 2*i));
            }

            // Aging after expiry date & quality drop
            for (int i = 1; i <= 5; i++)
            {
                target.UpdateQuality();
                Assert.That(conjuredItem.SellIn, Is.EqualTo(0 - i));
                Assert.That(conjuredItem.Quality, Is.EqualTo(0));
            }
        }
    }
}