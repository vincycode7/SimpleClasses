using System;
using System.Collections.Generic;

namespace SimpleClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Items in the cart
           List<CartItem> items = new List<CartItem>() 
                                    { 
                                        new CartItem() {Name = "Book", Type = "Alienware", Price = 3800.0m, Discount = 1.59m},
                                        new CartItem() {Name = "laptop", Type = "Alienware", Price = 3800.0m, Discount = 1.59m},
                                    };


            // Display the attributes
            Console.WriteLine("Item Name :- {0} \nItem Type :- {1} \nItem Price :- {2}", items[0].Name, items[0].Type, items[0].Price);
        }                
    }

    public class CartItem 
    {
        public string Name {get; set; }
        public string Type {get; set; }
        public decimal Price {get; set;}
        public decimal Discount {get; set;}

    }
        
    public class CartSystem 
    {
        private List<CartItem> _cartItems = new List<CartItem>();
        private Dictionary<string, decimal> _discountsByType = new Dictionary<string, decimal>();
        
        public CartSystem(List<CartItem> cartItems) 
        {
            _cartItems = cartItems;
        }

        public decimal CalculateTotalPrice() 
        {
            decimal total = 0;
            foreach (var cartItem in _cartItems) 
            {
                switch (cartItem.Type) 
                {

                    case "Cloth":
                        total += cartItem.Price * 0.95m;
                        break;

                    case "Gadget":
                        total += cartItem.Price * 0.60m;
                        break;

                    default:

                        if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Tuesday) {
                            total += cartItem.Price * 0.8m;
                        } 
                        
                        else {
                            total += cartItem.Price;
                        }
                        break;
                }
            }
            return total;
        }
    }


    public interface IProduct
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; }
        public int Quantity { get; set; }
    }

    public class ProductSettings
    {
        public List<DayOfWeek> DiscountDays { get; set; }
        public decimal ThresholdPrice { get; set; } = 1500;
        public decimal ThresholdDiscount { get; set; } = .8m;

        public decimal ApplyDiscountToProduct(decimal amount)
        {
            if (amount >= this.ThresholdPrice)
                amount *= ThresholdDiscount;

            return amount;
        }

        public ProductSettings()
        {
            this.DiscountDays = new List<DayOfWeek>() { DayOfWeek.Tuesday };
        }
    }
    public class Computer : IProduct
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; } = 250;
        public decimal Discount { get; } = 1;
        public int Quantity { get; set; } = 2;
    }

    public class Printer : IProduct
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; } = 350;
        public decimal Discount { get; } = .9m;
        public int Quantity { get; set; } = 3;
    }

    class RefactorCode
    {
        readonly ProductSettings settings = new ProductSettings();
        public decimal CalculateTotalPrice(List<IProduct> cartItems)
        {
            DayOfWeek today = DayOfWeek.Friday;
            decimal total = 0;
            foreach (var cartItem in cartItems)
            {
                decimal totalPrice = cartItem.Price * cartItem.Quantity;

                if (settings.DiscountDays.Any(x => x == today))
                    totalPrice *= cartItem.Discount;

                total += totalPrice;
            }

            total = settings.ApplyDiscountToProduct(total);

            return total;
        }
    }
}