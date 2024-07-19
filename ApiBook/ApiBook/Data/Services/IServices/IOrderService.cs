using ApiBook.Models.DTOs;

namespace ApiBook.Data.Services.IServices
{
    public interface IOrderService
    {
        Task<Response> GetAllOrdersByUser(int idUser);
        Task<Response> CreateOrder(int idUser);
    }
}
