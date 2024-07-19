using ApiBook.Models;
using ApiBook.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiBook.Data.Services.IServices
{
    public interface IUserService
    {
        Task<Response> GetAllUsers();
        Task<Response> GetByIdUser(int id);
        Task<Response> CreateUser(UserDto userDto);
        Task<Response> EditUser(int id, UserDto userDto);
    }
}
