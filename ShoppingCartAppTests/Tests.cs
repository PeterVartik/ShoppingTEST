using ShoppingCartApp;

namespace ShoppingCartAppTests
{
    [TestClass]
    public class CartItemTests
    {
        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var item = new CartItem("Apple", 1.50, 3);
            Assert.AreEqual("Apple", item.Name);
            Assert.AreEqual(1.50, item.UnitPrice);
            Assert.AreEqual(3, item.Quantity);
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
            var item = new CartItem("Banana", 0.75, 4);
            Assert.AreEqual(3.00, item.GetLineTotal());
        }

        [TestMethod]
        public void UpdateQuantity_ValidValue()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(5);
            Assert.AreEqual(5, item.Quantity);
        }
        [TestMethod]
        public void UpdateQuantity_InvalidValue()
        {
            var item = new CartItem("Milk", 1.20, 1);
            Assert.ThrowsException<ArgumentException>(() => item.UpdateQuantity(0));
            Assert.ThrowsException<ArgumentException>(() => item.UpdateQuantity(-5));

        }

        [TestClass]
        public class ShoppingCartTests
        {
            private ShoppingCart CreateCartWithItems()
            {
                var cart = new ShoppingCart();
                cart.AddItem("Apple", 1.00, 3);
                cart.AddItem("Bread", 2.50, 1);
                return cart;
            }

            [TestMethod]
            public void AddItem_NewItem()
            {
                var cart = new ShoppingCart();
                cart.AddItem("Apple", 1.00, 2);
                Assert.AreEqual(2, cart.GetItemCount());
            }
            [TestMethod]
            public void AddItem_ExistingItem()
            {
                var cart = CreateCartWithItems();
                cart.AddItem("Apple", 1.00, 2);
                Assert.AreEqual(6, cart.GetItemCount());
            }
            [TestMethod]
            public void AddItem_InvalidArguments()
            {
                Assert.ThrowsException<ArgumentException>(() => new ShoppingCart().AddItem("", 1.00, 2));
            }


            [TestMethod]
            public void RemoveItem_ExistingItem()
            {
                var cart = CreateCartWithItems();
                bool result = cart.RemoveItem("Apple");
                Assert.IsTrue(result);
                Assert.AreEqual(1, cart.GetItemCount());
            }
            [TestMethod]
            public void removeItem_NonExistingItem()
            {
                var cart = CreateCartWithItems();
                bool result = cart.RemoveItem("NonExisting");
                Assert.IsFalse(result);
                Assert.AreEqual(4, cart.GetItemCount());
            }
            [TestMethod]
            public void RemoveItem_CaseSensitivity()
            {
                var cart = CreateCartWithItems();
                bool result = cart.RemoveItem("apple");
                Assert.IsTrue(result);
                Assert.AreEqual(1, cart.GetItemCount());
            }


            [TestMethod]
            public void GetTotal_MultipleItems()
            {
                var cart = new ShoppingCart();
                cart.AddItem("Apple", 1.00, 3);  // 3.00
                cart.AddItem("Bread", 2.50, 2);  // 5.00
                Assert.AreEqual(8.00m, cart.GetTotal());
            }
            [TestMethod]
            public void GetTotal_EmptyCart()
            {
                var cart = new ShoppingCart();
                Assert.AreEqual(0m, cart.GetTotal());
            }
            [TestMethod]
            public void GetTotal_AfterRemovingItem()
            {
                var cart = CreateCartWithItems();
                cart.RemoveItem("Apple");
                Assert.AreEqual(2.50m, cart.GetTotal());
            }



            [TestMethod]
            public void Clear_CartWithItems()
            {
                var cart = CreateCartWithItems();
                cart.Clear();
                Assert.AreEqual(0, cart.GetItemCount());
                Assert.AreEqual(0m, cart.GetTotal());
            }

            [TestMethod]
            public void Clear_EmptyCart()
            {
                var cart = new ShoppingCart();
                cart.Clear();
                Assert.AreEqual(0, cart.GetItemCount());
                Assert.AreEqual(0m, cart.GetTotal());
            }

        }

        [TestClass]
        public class DiscountTests
        {
            [TestMethod]
            public void ApplyPercentage_TenPercent()
            {
                var discount = new Discount();
                Assert.AreEqual(180, discount.ApplyPercentage(200, 10));
            }
            [TestMethod]
            public void ApplyPercentage_ZeroPercent()
            {
                var discount = new Discount();
                Assert.AreEqual(200, discount.ApplyPercentage(200, 0));
            }

            [TestMethod]
            public void ApplyPercentage_HundredPercent()
            {
                var discount = new Discount();
                Assert.AreEqual(0, discount.ApplyPercentage(200, 100));
            }

            [TestMethod]
            public void ApplyPercentage_FiftyPercent()
            {
                var discount = new Discount();
                Assert.AreEqual(100, discount.ApplyPercentage(200, 50));
            }

            [TestMethod]
            public void ApplyPercentage_OverHundredPercent()
            {
                var discount = new Discount();
                Assert.ThrowsException<ArgumentException>(() => discount.ApplyPercentage(200, 105));
            }


            [TestMethod]
            public void ApplyFixed_AmountLessThanTotal()
            {
                var discount = new Discount();
                Assert.AreEqual(75, discount.ApplyFixed(100, 25));
            }
            [TestMethod]
            public void ApplyFixed_AmountEqualToTotal()
            {
                var discount = new Discount();
                Assert.AreEqual(0, discount.ApplyFixed(100, 100));
            }

            [TestMethod]
            public void ApplyFixed_AmountGreaterThanTotal()
            {
                var discount = new Discount();
                Assert.AreEqual(0, discount.ApplyFixed(100, 150));
            }

            [TestMethod]
            public void ApplyFixed_NegativeAmount()
            {
                var discount = new Discount();
                Assert.ThrowsException<ArgumentException>(() => discount.ApplyFixed(100, -10));
            }


            [TestMethod]
            public void IsValid_PositiveValue()
            {
                var discount = new Discount();
                Assert.IsTrue(discount.IsValid(15));
            }
            [TestMethod]
            public void IsValid_ZeroValue()
            {
                var discount = new Discount();
                Assert.IsFalse(discount.IsValid(0));
            }

            [TestMethod]

            public void IsValid_NegativeValue()
            {
                var discount = new Discount();
                Assert.IsFalse(discount.IsValid(-5));

            }
        }
    }
}
