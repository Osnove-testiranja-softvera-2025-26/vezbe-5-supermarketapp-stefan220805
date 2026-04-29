using NUnit.Framework;
using OTS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS_Supermarket.Test
{
    [TestFixture]
    public class CartTest
    {
        [Test]
        public void AddMultipleToCart_AddThreeLaptops_CountersUpdated()
        {
            // ARRANGE
            Cart cart = new Cart();
            Laptop laptop = new Laptop { Price = 500 };
            int quantity = 3;

            // ACT
            cart.AddMultipleToCart(laptop, quantity);

            // ASSERT
            Assert.Multiple(() =>
            {
                Assert.That(cart.Size, Is.EqualTo(3));
                Assert.That(cart.Laptop_counter, Is.EqualTo(3));
                Assert.That(cart.Amount, Is.EqualTo(1500));
            });

        }

        [TestCase(0, 1)]  // Prazna korpa -> 1
        [TestCase(5, 6)]  // Polupuna -> 6
        [TestCase(9, 10)] // Skoro puna -> 10
        public void AddOneToCart_IncrementSize_Success(int initialSize, int expectedSize)
        {
            // ARRANGE
            Cart cart = new Cart();
            cart.Size = initialSize;
            Monitor monitor = new Monitor();

            // ACT
            cart.AddOneToCart(monitor);

            // ASSERT
            Assert.That(cart.Size, Is.EqualTo(expectedSize));
        }

        [TestCase(2, 3, 5)] // Imamo 2, dodajemo 3 -> Ukupno 5
        [TestCase(0, 10, 10)] // Imamo 0, dodajemo 10 -> Ukupno 10
        [TestCase(5, 1, 6)]  // Imamo 5, dodajemo 1 -> Ukupno 6
        public void AddMultipleToCart_UpdateCounters_Correctly(int initialSize, int quantityToAdd, int expectedTotal)
        {
            // ARRANGE
            Cart cart = new Cart();
            cart.Size = initialSize;
            cart.Keyboard_counter = initialSize; // Pretpostavimo da su prethodni artikli bili tastature
            Keyboard keyboard = new Keyboard { Price = 50 };

            // ACT
            cart.AddMultipleToCart(keyboard, quantityToAdd);

            // ASSERT
            Assert.Multiple(() => {
                Assert.That(cart.Size, Is.EqualTo(expectedTotal));
                Assert.That(cart.Keyboard_counter, Is.EqualTo(expectedTotal));
            });
        }
    }
}
