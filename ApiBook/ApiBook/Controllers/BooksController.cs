using ApiBook.Data.Services.IServices;
using ApiBook.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            Response res = new Response();
            try
            {
                return Ok(await _bookService.GetAllBooks());
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Mesage = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Mesage = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }

        }

        [HttpGet("GetByIdBook/{id}")]
        public async Task<IActionResult> GetByIdBook(int id)
        {
            Response res = new Response();
            try
            {
                return Ok(await _bookService.GetByIdBook(id));
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Mesage = ex.Message;
                return BadRequest(res);
            }

            catch (Exception)
            {
                res.Succes = false;
                res.Mesage = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }

        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(BooksDto BooksDto)
        {
            Response res = new Response();
            try
            {
                return Ok(await _bookService.CreateBook(BooksDto));

            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Mesage = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Mesage = "Error en el servidor, Contactese con los administradores de el aplicativo";
                return StatusCode(500, res);
            }

        }

        [HttpPut("EditBook/{id}")]
        public async Task<IActionResult> EditBook(int id, BooksDto BooksDto)
        {
            Response res = new Response();
            try
            {
                return Ok(await _bookService.EditBook(id, BooksDto));

            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Mesage = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Mesage = "Error en el servidor, Contactese con los administradores de el aplicativo";
                return StatusCode(500, res);
            }

        }
    }
}
