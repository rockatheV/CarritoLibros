using ApiBook.Data.Repository.IRepository;
using ApiBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiBook.Data.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly ContextModel _context;

        public BookRepository(ContextModel context)
        {
            _context = context;
        }

        public async Task<List<BooksModel>> GetAllBooks(Expression<Func<BooksModel, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _context.Books.ToListAsync();
            }
            return await _context.Books.Where(predicate).ToListAsync();
        }

        public async Task<BooksModel> GetByIdBook(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> CreateBook(BooksModel book)
        {
            await _context.Books.AddAsync(book);
            return await Save();
        }

        public async Task<bool> EditBook(BooksModel book)
        {
            _context.Books.Update(book);
            return await Save();
        }

        public async Task<bool> DeleteBook(BooksModel book)
        {
            _context.Books.Remove(book);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
