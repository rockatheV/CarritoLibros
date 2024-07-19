using ApiBook.Models;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository.IRepository
{
    public interface IBookRepository
    {
        Task<List<BooksModel>> GetAllBooks(Expression<Func<BooksModel, bool>> predicate=null);
        Task<BooksModel> GetByIdBook(int id);
        Task<bool> CreateBook(BooksModel book);
        Task<bool> EditBook(BooksModel book);
        Task<bool> DeleteBook(BooksModel book);
        Task<bool> Save();
    }
}
