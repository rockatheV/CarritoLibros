using ApiBook.Data.Repository.IRepository;
using ApiBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ContextModel _context;

        public OrderRepository(ContextModel context)
        {
            _context = context;
        }

        public async Task<List<OrdersModel>> GetAllOrders(Expression<Func<OrdersModel, bool>> predicate)
        {
            return await _context.Orders.Where(predicate).ToListAsync();
        }

        public async Task<OrdersModel> GetByIdOrder(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<bool> CreateOrder(OrdersModel order)
        {
            await _context.Orders.AddAsync(order);
            return await Save();
        }

        public async Task<bool> EditOrder(OrdersModel order)
        {
            _context.Orders.Update(order);
            return await Save();
        }

        public async Task<bool> DeleteOrder(OrdersModel order)
        {
            _context.Orders.Remove(order);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
