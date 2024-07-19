using ApiBook.Models;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers(Expression<Func<UserModel, bool>> predicate = null);
        Task<UserModel> GetByIdUser(int id);
        Task<bool> CreateUser(UserModel user);
        Task<bool> EditUser(UserModel user);
        Task<bool> DeleteUser(UserModel user);
        Task<bool> Save();
    }
}
