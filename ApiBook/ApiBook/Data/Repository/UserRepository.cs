using ApiBook.Data.Repository.IRepository;
using ApiBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextModel _contextModel;
        public UserRepository(ContextModel contextModel)
        {

            _contextModel = contextModel;

        }

        public async Task<bool> CreateUser(UserModel user)
        {
            await _contextModel.User.AddAsync(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(UserModel user)
        {
             _contextModel.User.Remove(user);
            return await Save();
        }

        public async Task<bool> EditUser(UserModel user)
        {
             _contextModel.User.Update(user);
            return await Save();
        }

        public async Task<List<UserModel>> GetAllUsers(Expression<Func<UserModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _contextModel.User.ToListAsync();
            }
            return await _contextModel.User.Where(predicate).ToListAsync();
        }

        public async Task<UserModel> GetByIdUser(int id)
        {
            return await _contextModel.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _contextModel.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
