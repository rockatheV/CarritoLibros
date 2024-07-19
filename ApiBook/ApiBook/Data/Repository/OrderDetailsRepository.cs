using ApiBook.Data.Repository.IRepository;
using ApiBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository
{
    public class OrderDetailsRepository: IOrderDetailsRepository
    {
        private readonly ContextModel _context;

        public OrderDetailsRepository(ContextModel context)
        {
            _context = context;
        }

        public async Task<List<OrderDetailsModel>> GetAllOrderDetails(Expression<Func<OrderDetailsModel, bool>> predicate)
        {
           
            return await _context.OrderDetails.Include(m=>m.Order).Include(m=>m.Book).Where(predicate).ToListAsync();
        }

        public async Task<OrderDetailsModel> GetByIdOrderDetail(int id)
        {
            return await _context.OrderDetails.Include(m => m.Order).Include(m => m.Book).FirstOrDefaultAsync (x=>x.Id==id);
        }

        public async Task<bool> CreateOrderDetail(OrderDetailsModel orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            return await Save();
        }

        public async Task<bool> EditOrderDetail(OrderDetailsModel orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            return await Save();
        }

        public async Task<bool> DeleteOrderDetail(OrderDetailsModel orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
