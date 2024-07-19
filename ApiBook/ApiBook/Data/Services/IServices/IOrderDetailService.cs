using ApiBook.Models.DTOs;

namespace ApiBook.Data.Services.IServices
{
    public interface IOrderDetailService
    {
        Task<Response> GetAllDetailsOrdersbyOrder(int idOrder);
         Task<Response> CreateOrUpdateOrderDetail(OrderDetailsDto orderDetailDto);
        Task<Response> UpdateAmountDetailOrder(int Id, int Amount);
        Task<Response> GetOrderDetailsById(int id);
    }
}
