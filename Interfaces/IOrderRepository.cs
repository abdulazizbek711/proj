using MarketApi.Models;

namespace MarketApi.Interfaces;

public interface IOrderRepository
{
    ICollection<Order> GetOrders();
    Order GetOrder(int Order_number);
    
    bool OrderExists(int Order_number);
    
    bool CreateOrder(Order order);
    bool UpdateOrder(Order order);
    bool DeleteOrder(Order order);
}