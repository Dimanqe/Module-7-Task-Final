using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
class Program
{
    static void Main()
    {
        var homeDelivery = new HomeDelivery
        {
            Address = "Russia, Cheboksary, Prokovyeva ave. 11, apt.123",
            CourierName = "Alex Popov",
            DeliveryDate = DateTime.Now.AddDays(2)
        };

        var product1 = new Product
        {
            Name = "Keyboard",
            Price = 999
        };

        var product2 = new Product
        {
            Name = "Mouse",
            Price = 599
        };

        var order = new Order<Delivery, object>
        {
            Delivery = homeDelivery,
            Number = 5567,
            Description = "Electronic devices"
        };

        order.AddProduct(product1);
        order.AddProduct(product2);

        decimal totalPrice = order.CalculateTotalPrice();

        Console.WriteLine("Order Number: " + order.Number);
        Console.WriteLine("Delivery Address: " + order.Delivery.Address);
        Console.WriteLine("Delivery Date: " + homeDelivery.DeliveryDate);
        Console.WriteLine("Products:");

        foreach (var product in order.Products)
        {
            Console.WriteLine($"- {product.Name}: {product.Price} RUB");
        }

        Console.WriteLine($"Total Price: " + totalPrice + " RUB");

        var deliveryProvider = new MyDeliveryProvider
        {
            CompanyName = "Alibaba",
            ContactInfo = "Phone: 123-456-7890, Email: alibaba@example.com"
        };

        deliveryProvider.PerformDelivery(order);
    }
}
abstract class Delivery
{
    public string Address { get; set; }
}
class HomeDelivery : Delivery
{
    public string CourierName { get; set; }
    public DateTime DeliveryDate { get; set; }
}
class PickPointDelivery : Delivery
{
    public string PickPointLocation { get; set; }
    public string PickPointCode { get; set; }
}
class ShopDelivery : Delivery
{
    public string ShopName { get; set; }
    public DateTime PickupDate { get; set; }
}
class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
abstract class DeliveryProvider
{
    public string CompanyName { get; set; }
    public string ContactInfo { get; set; }

    public abstract void PerformDelivery(Order<Delivery, object> order);
}
class Order<TDelivery, TStruct> where TDelivery : Delivery
{
    public TDelivery Delivery { get; set; }
    public int Number { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }

    public Order()
    {
        Products = new List<Product>();
    }
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
    }
    public decimal CalculateTotalPrice()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.Price;
        }
        return total;
    }
    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.Address);
    }
}

class MyDeliveryProvider : DeliveryProvider
{
    public override void PerformDelivery(Order<Delivery, object> order)
    {
        Console.WriteLine($"Delivering to: {order.Delivery.Address}");
        Console.WriteLine($"Parcel description: {order.Description}");
        Console.WriteLine("Delivery completed!");
    }
}
