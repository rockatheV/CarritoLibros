using ApiBook.Models.DTOs;

namespace ApiBook.Data.Services.IServices
{
    public interface IBookService
    {
        Task<Response> GetAllBooks();
        Task<Response> GetByIdBook(int id);
        Task<Response> CreateBook(BooksDto bookDto);
        Task<Response> EditBook(int id, BooksDto bookDto);
    }
}
