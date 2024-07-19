using ApiBook.Models;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<List<OrdersModel>> GetAllOrders(Expression<Func<OrdersModel, bool>> predicate);
        Task<OrdersModel> GetByIdOrder(int id);
        Task<bool> CreateOrder(OrdersModel order);
        Task<bool> EditOrder(OrdersModel order);
        Task<bool> DeleteOrder(OrdersModel order);
        Task<bool> Save();
    }
}
