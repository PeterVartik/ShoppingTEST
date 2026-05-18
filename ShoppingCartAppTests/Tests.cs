using ShoppingCartApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ShoppingCartAppTests
{
    [TestClass]
    public class CartItemTests
    {
        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var cartItem = new CartItem("Apple", 1.50, 3);
            Assert.AreEqual("Apple", cartItem.Name);
            Assert.AreEqual(1.50, cartItem.UnitPrice);
            Assert.AreEqual(3, cartItem.Quantity);
        }

        [TestMethod]
        public void Constructor_InvalidName()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem("", 1.50, 3));
        }

        [TestMethod]
        public void Constructor_InvalidUnitPrice()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem("InvalidUnitPrice", -1, 3));
        }

        [TestMethod]
        public void Constructor_InvalidQuantity()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem("InvalidQuantity", 1.50, 0));
        }

        [TestMethod]
        public void GetTotal_MultipleQuantity()
        {
            var cartItem = new CartItem("Banana", 0.75, 4);
            Assert.AreEqual(3.00, cartItem.GetLineTotal());
        }

        [TestMethod]
        public void UpdateQuantity_ValidValue()
        {
            var cartItem = new CartItem("Milk", 1.20, 1);
            cartItem.UpdateQuantity(5);
            Assert.AreEqual(5, cartItem.Quantity);
        }

        [TestMethod]
        public void UpdateQuantity_InvalidValue()
        {
            var cartItem = new CartItem("Milk", 1.20, 1);
            Assert.ThrowsException<ArgumentException>(() => cartItem.UpdateQuantity(0));
            Assert.ThrowsException<ArgumentException>(() => cartItem.UpdateQuantity(-5));
        }
    }

    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCart CreatePopulatedCart()
        {
            var myCart = new ShoppingCart();
            myCart.AddItem("Apple", 1.00, 3);
            myCart.AddItem("Bread", 2.50, 1);
            return myCart;
        }

        [TestMethod]
        public void AddItem_NewItem()
        {
            var myCart = new ShoppingCart();
            myCart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(2, myCart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_ExistingItem()
        {
            var myCart = CreatePopulatedCart();
            myCart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(6, myCart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_InvalidArguments()
        {
            Assert.ThrowsException<ArgumentException>(() => new ShoppingCart().AddItem("", 1.00, 2));
        }

        [TestMethod]
        public void RemoveItem_ExistingItem()
        {
            var myCart = CreatePopulatedCart();
            bool isRemoved = myCart.RemoveItem("Apple");
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(1, myCart.GetItemCount());
        }

        [TestMethod]
        public void removeItem_NonExistingItem()
        {
            var myCart = CreatePopulatedCart();
            bool isRemoved = myCart.RemoveItem("NonExisting");
            Assert.IsFalse(isRemoved);
            Assert.AreEqual(4, myCart.GetItemCount());
        }

        [TestMethod]
        public void RemoveItem_CaseSensitivity()
        {
            var myCart = CreatePopulatedCart();
            bool isRemoved = myCart.RemoveItem("apple");
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(1, myCart.GetItemCount());
        }

        [TestMethod]
        public void GetTotal_MultipleItems()
        {
            var myCart = new ShoppingCart();
            myCart.AddItem("Apple", 1.00, 3);
            myCart.AddItem("Bread", 2.50, 2);
            Assert.AreEqual(8.00m, myCart.GetTotal());
        }

        [TestMethod]
        public void GetTotal_EmptyCart()
        {
            var myCart = new ShoppingCart();
            Assert.AreEqual(0m, myCart.GetTotal());
        }

        [TestMethod]
        public void GetTotal_AfterRemovingItem()
        {
            var myCart = CreatePopulatedCart();
            myCart.RemoveItem("Apple");
            Assert.AreEqual(2.50m, myCart.GetTotal());
        }

        [TestMethod]
        public void Clear_CartWithItems()
        {
            var myCart = CreatePopulatedCart();
            myCart.Clear();
            Assert.AreEqual(0, myCart.GetItemCount());
            Assert.AreEqual(0m, myCart.GetTotal());
        }

        [TestMethod]
        public void Clear_EmptyCart()
        {
            var myCart = new ShoppingCart();
            myCart.Clear();
            Assert.AreEqual(0, myCart.GetItemCount());
            Assert.AreEqual(0m, myCart.GetTotal());
        }
    }

    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void ApplyPercentage_TenPercent()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(180, testDiscount.ApplyPercentage(200, 10));
        }

        [TestMethod]
        public void ApplyPercentage_ZeroPercent()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(200, testDiscount.ApplyPercentage(200, 0));
        }

        [TestMethod]
        public void ApplyPercentage_HundredPercent()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(0, testDiscount.ApplyPercentage(200, 100));
        }

        [TestMethod]
        public void ApplyPercentage_FiftyPercent()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(100, testDiscount.ApplyPercentage(200, 50));
        }

        [TestMethod]
        public void ApplyPercentage_OverHundredPercent()
        {
            var testDiscount = new Discount();
            Assert.ThrowsException<ArgumentException>(() => testDiscount.ApplyPercentage(200, 105));
        }

        [TestMethod]
        public void ApplyFixed_AmountLessThanTotal()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(75, testDiscount.ApplyFixed(100, 25));
        }

        [TestMethod]
        public void ApplyFixed_AmountEqualToTotal()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(0, testDiscount.ApplyFixed(100, 100));
        }

        [TestMethod]
        public void ApplyFixed_AmountGreaterThanTotal()
        {
            var testDiscount = new Discount();
            Assert.AreEqual(0, testDiscount.ApplyFixed(100, 150));
        }

        [TestMethod]
        public void ApplyFixed_NegativeAmount()
        {
            var testDiscount = new Discount();
            Assert.ThrowsException<ArgumentException>(() => testDiscount.ApplyFixed(100, -10));
        }

        [TestMethod]
        public void IsValid_PositiveValue()
        {
            var testDiscount = new Discount();
            Assert.IsTrue(testDiscount.IsValid(15));
        }

        [TestMethod]
        public void IsValid_ZeroValue()
        {
            var testDiscount = new Discount();
            Assert.IsFalse(testDiscount.IsValid(0));
        }

        [TestMethod]
        public void IsValid_NegativeValue()
        {
            var testDiscount = new Discount();
            Assert.IsFalse(testDiscount.IsValid(-5));
        }
    }
}
