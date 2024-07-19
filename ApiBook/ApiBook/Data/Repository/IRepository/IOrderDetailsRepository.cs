using ApiBook.Models;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository.IRepository
{
    public interface IOrderDetailsRepository
    {
        Task<List<OrderDetailsModel>> GetAllOrderDetails(Expression<Func<OrderDetailsModel, bool>> predicate);
        Task<OrderDetailsModel> GetByIdOrderDetail(int id);
        Task<bool> CreateOrderDetail(OrderDetailsModel orderDetail);
        Task<bool> EditOrderDetail(OrderDetailsModel orderDetail);
        Task<bool> DeleteOrderDetail(OrderDetailsModel orderDetail);
        Task<bool> Save();
    }
}
