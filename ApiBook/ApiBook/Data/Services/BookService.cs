using ApiBook.Data.Repository.IRepository;
using ApiBook.Data.Services.IServices;
using ApiBook.Models.DTOs;
using ApiBook.Models;

namespace ApiBook.Data.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepo;
        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public async Task<Response> GetAllBooks()
        {
            Response res = new Response();
            try
            {

                List<BooksModel> books = await _bookRepo.GetAllBooks();
                List<BooksDto> booksDto = new List<BooksDto>();
                foreach (BooksModel book in books)
                {
                    BooksDto bookDto = new BooksDto()
                    {
                       Id = book.Id,
                       Author = book.Author,
                       Price = book.Price,
                       PublicationDate = book.PublicationDate,
                       Title = book.Title
                    };
                    booksDto.Add(bookDto);
                }


                res.Data = booksDto;
                res.Succes = true;
                res.Mesage = "Ok";

                return res;

            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }
        public async Task<Response> GetByIdBook(int id)
        {
            Response res = new Response();
            try
            {

                BooksModel book = await _bookRepo.GetByIdBook(id);
                if (book == null)
                {
                    throw new InvalidOperationException("No existe un libro con el id " + id);
                }
                BooksDto bookDto = new BooksDto()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Price = book.Price,
                    PublicationDate = book.PublicationDate,
                    Title = book.Title
                };

                res.Data = bookDto;
                res.Succes = true;
                res.Mesage = "Ok";

                return res;

            }
            catch (InvalidOperationException ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }

        public async Task<Response> CreateBook(BooksDto bookDto)
        {
            Response res = new Response();
            try
            {
                if (bookDto.Title == "")
                {
                    throw new InvalidOperationException("El Title es obligatoio");
                }
                if (bookDto.Author == "")
                {
                    throw new InvalidOperationException("El Author es obligatoio");
                }
                List<BooksModel> ExisteUser = await _bookRepo.GetAllBooks(x => x.Title.ToLower() == bookDto.Title.ToLower() && x.Author.ToLower()==bookDto.Author.ToLower());
                if (ExisteUser.Count > 0)
                {
                    throw new InvalidOperationException("Ya existe un libro con este titulo y este autor");
                }
                BooksModel booksCreate = new BooksModel
                {
                    Author = bookDto.Author,
                    Price = (decimal)bookDto.Price,
                    PublicationDate = (DateTime)bookDto.PublicationDate,
                    Title = bookDto.Title
                };
                bool save = await _bookRepo.CreateBook(booksCreate);
                if (!save)
                {
                    throw new InvalidOperationException("Error al intentar crear el libro");
                }
                else
                {
                    res.Succes = true;
                    res.Mesage = "Libro creado correctamente";
                    return res;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }

        public async Task<Response> EditBook(int id, BooksDto bookDto)
        {
            Response res = new Response();
            try
            {
                if (bookDto.Id != id)
                {
                    throw new InvalidOperationException("El id enviado por la url y el de el cuerpo de la peticion no coinciden");
                }
                if (bookDto.Id == 0)
                {
                    throw new InvalidOperationException("El id es obligatorio");
                }
                if (string.IsNullOrEmpty(bookDto.Title))
                {
                    throw new InvalidOperationException("El Title es obligatorio");
                }
                if (string.IsNullOrEmpty(bookDto.Author))
                {
                    throw new InvalidOperationException("El Author es obligatorio");
                }

                BooksModel bookToUpdate = await _bookRepo.GetByIdBook((int)bookDto.Id);
                if (bookToUpdate == null)
                {
                    throw new InvalidOperationException("libro no encontrado");
                }

                List<BooksModel> existingBookWithEmail = await _bookRepo.GetAllBooks(x => x.Title.ToLower() == bookDto.Title.ToLower() && x.Author.ToLower() == bookDto.Author.ToLower() && x.Id != bookDto.Id);
                if (existingBookWithEmail.Count > 0)
                {
                    throw new InvalidOperationException("Ya existe un libro con este Titulo y ese autor");
                }

                bookToUpdate.Title = bookDto.Title;
                bookToUpdate.Author = bookDto.Author;
                bookToUpdate.Price = bookDto.Price;
                bookToUpdate.PublicationDate = bookDto.PublicationDate;

                bool save = await _bookRepo.EditBook(bookToUpdate);
                if (!save)
                {
                    throw new InvalidOperationException("Error al intentar editar el libro");
                }
                else
                {
                    res.Succes = true;
                    res.Mesage = "libro editado correctamente";
                    return res;
                }
            }
            catch (InvalidOperationException ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }

    }
}
