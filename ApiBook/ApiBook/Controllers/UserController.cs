using ApiBook.Data.Services.IServices;
using ApiBook.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUsers")]
        public async Task< IActionResult >GetAllUsers()
        {
            Response res = new Response();
            try
            {
                return Ok(await _userService.GetAllUsers());
            }
            catch(InvalidOperationException ex) 
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

        [HttpGet("GetByIdUser/{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            Response res = new Response();
            try
            {
                return Ok(await _userService.GetByIdUser(id));
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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            Response res = new Response();
            try
            {
                return Ok(await _userService.CreateUser(userDto));

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

        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id,UserDto userDto)
        {
            Response res = new Response();
            try
            {
                return Ok(await _userService.EditUser(id,userDto));

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
