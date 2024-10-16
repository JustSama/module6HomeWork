using System;
using System.Collections.Generic;

public class Product : ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public object Clone()
    {
        return new Product { Name = Name, Price = Price, Quantity = Quantity };
    }
}

public class Discount : ICloneable
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public object Clone()
    {
        return new Discount { Description = Description, Amount = Amount };
    }
}

public class Order : ICloneable
{
    public List<Product> Products { get; set; } = new List<Product>();
    public List<Discount> Discounts { get; set; } = new List<Discount>();
    public decimal DeliveryCost { get; set; }
    public string PaymentMethod { get; set; }

    public object Clone()
    {
        var clonedOrder = new Order
        {
            DeliveryCost = DeliveryCost,
            PaymentMethod = PaymentMethod
        };

        foreach (var product in Products)
            clonedOrder.Products.Add((Product)product.Clone());

        foreach (var discount in Discounts)
            clonedOrder.Discounts.Add((Discount)discount.Clone());

        return clonedOrder;
    }
}

class Program
{
    static void Main()
    {
        var order = new Order
        {
            DeliveryCost = 15.99m,
            PaymentMethod = "Credit Card"
        };
        order.Products.Add(new Product { Name = "Laptop", Price = 1200.50m, Quantity = 1 });
        order.Products.Add(new Product { Name = "Mouse", Price = 25.75m, Quantity = 2 });
        order.Discounts.Add(new Discount { Description = "New Year Sale", Amount = 100.00m });

        var clonedOrder = (Order)order.Clone();
        clonedOrder.PaymentMethod = "PayPal";

        Console.WriteLine("Оригинальный заказ:");
        PrintOrder(order);
        Console.WriteLine("\nКлонированный заказ:");
        PrintOrder(clonedOrder);
    }

    static void PrintOrder(Order order)
    {
        Console.WriteLine($"Доставка: {order.DeliveryCost}, Оплата: {order.PaymentMethod}");
        foreach (var product in order.Products)
            Console.WriteLine($"Товар: {product.Name}, Цена: {product.Price}, Кол-во: {product.Quantity}");
        foreach (var discount in order.Discounts)
            Console.WriteLine($"Скидка: {discount.Description}, Сумма: {discount.Amount}");
    }
}
