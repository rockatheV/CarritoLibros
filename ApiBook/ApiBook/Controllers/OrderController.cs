using ApiBook.Data.Services.IServices;
using ApiBook.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        public OrderController(IOrderService orderService,IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        [HttpGet("GetAllOrdersByUser/{idUser}")]
        public async Task<IActionResult> GetAllOrdersByUser(int idUser)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderService.GetAllOrdersByUser(idUser));
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
        [HttpGet("GetAllDetailsOrdersbyOrder/{idOrder}")]
        public async Task<IActionResult> GetAllDetailsOrdersbyOrder(int idOrder)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderDetailService.GetAllDetailsOrdersbyOrder(idOrder));
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

        [HttpPost("CreateOrder/{iduser}")]
        public async Task<IActionResult> CreateOrder(int iduser)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderService.CreateOrder(iduser));
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

        [HttpPatch("CreateOrUpdateOrderDetail")]
        public async Task<IActionResult> CreateOrUpdateOrderDetail(OrderDetailsDto orderDetailsDto)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderDetailService.CreateOrUpdateOrderDetail(orderDetailsDto));
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

        [HttpPatch("UpdateAmountDetailOrder/{id}/{amount}")]
        public async Task<IActionResult> UpdateAmountDetailOrder(int id,int amount)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderDetailService.UpdateAmountDetailOrder(id,amount));
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

        [HttpGet("GetOrderDetailsById/{id}")]
        public async Task<IActionResult> GetOrderDetailsById(int id)
        {
            Response res = new Response();
            try
            {
                return Ok(await _orderDetailService.GetOrderDetailsById(id));
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
