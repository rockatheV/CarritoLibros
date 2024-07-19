using ApiBook.Data.Repository.IRepository;
using ApiBook.Data.Services.IServices;
using ApiBook.Models;
using ApiBook.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiBook.Data.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailsRepository _orderDetailRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookRepository _bookRepo;
        public OrderDetailService(IBookRepository bookRepo, IUserRepository userRepo, IOrderDetailsRepository orderDetailsRepo, IOrderRepository orderRepo)
        {

            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _orderDetailRepo = orderDetailsRepo;
            _orderRepo = orderRepo;

        }

        public async Task<Response> GetAllDetailsOrdersbyOrder(int idOrder)
        {
            Response res = new Response();
            try
            {

                List<OrderDetailsModel> orderDetails = await _orderDetailRepo.GetAllOrderDetails(x => x.IdOrder == idOrder);
                List<OrderDetailsDto> ordersDetailsDto = new List<OrderDetailsDto>();
                foreach (OrderDetailsModel order in orderDetails)
                {
                    OrderDetailsDto orderDetailsDto = new OrderDetailsDto()
                    {
                        Id = order.Id,
                        Amount = order.Amount,
                        IdBook = order.IdBook,
                        Book = order.Book.Title,
                        BookAuthor = order.Book.Author,
                        BookPrice = order.Book.Price,
                        IdOrder = order.IdOrder,
                        Order = order.Order.FechaPedido.ToString(),
                        Price = order.Price
                    };
                    ordersDetailsDto.Add(orderDetailsDto);
                }


                res.Data = ordersDetailsDto;
                res.Succes = true;
                res.Mesage = "Ok";

                return res;

            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }
        public async Task<Response> CreateOrUpdateOrderDetail(OrderDetailsDto orderDetailDto)
        {
            Response res = new Response();

            try
            {
                // Validar que el pedido existe
                List<OrdersModel> order = await _orderRepo.GetAllOrders(p => p.Id == orderDetailDto.IdOrder);
                if (order.Count == 0)
                {
                    throw new InvalidOperationException("El pedido con el ID proporcionado no existe.");
                }

                // Validar que el libro existe
                List<BooksModel> book = await _bookRepo.GetAllBooks(l => l.Id == orderDetailDto.IdBook);
                if (book.Count == 0)
                {
                    throw new InvalidOperationException("El libro con el ID proporcionado no existe.");
                }

                // Validar que la cantidad sea mayor que cero
                if (orderDetailDto.Amount <= 0)
                {
                    throw new InvalidOperationException("La cantidad debe ser mayor que cero.");
                }

                // Verificar si ya existe un detalle de pedido para este libro en este pedido
                List<OrderDetailsModel> ListExistingDetail = await _orderDetailRepo.GetAllOrderDetails(od => od.IdOrder == orderDetailDto.IdOrder && od.IdBook == orderDetailDto.IdBook);
                OrderDetailsModel existingDetail = ListExistingDetail.FirstOrDefault();

                if (existingDetail != null)
                {
                    // Si ya existe, actualizar la cantidad y el precio
                    existingDetail.Amount += orderDetailDto.Amount;
                    existingDetail.Price += orderDetailDto.Amount * (book.FirstOrDefault().Price);

                    await _orderDetailRepo.EditOrderDetail(existingDetail);
                }
                else
                {

                    // Si no existe, insertar un nuevo detalle de pedido
                    OrderDetailsModel newDetail = new OrderDetailsModel
                    {
                        IdOrder = orderDetailDto.IdOrder,
                        IdBook = orderDetailDto.IdBook,
                        Amount = orderDetailDto.Amount,
                        Price = orderDetailDto.Amount * (book.FirstOrDefault().Price)

                    };
                    await _orderDetailRepo.CreateOrderDetail(newDetail);
                }
                res.Succes = true;
                res.Mesage = "Detalle de pedido creado o actualizado exitosamente.";
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

        public async Task<Response> UpdateAmountDetailOrder(int Id, int Amount)
        {
            Response res = new Response();
            try
            {
                OrderDetailsModel orderDetailsModel = await _orderDetailRepo.GetByIdOrderDetail(Id);
                if (orderDetailsModel == null)
                {
                    throw new InvalidOperationException("No existe un detalle con el id " + Id);
                }
                orderDetailsModel.Amount = Amount;
                orderDetailsModel.Price= orderDetailsModel.Amount * (orderDetailsModel.Book.Price);
                bool update = await _orderDetailRepo.EditOrderDetail(orderDetailsModel);
                if (update)
                {
                    res.Succes = true;
                    res.Mesage = "Se actualizo Correctamente";
                    return res;
                }
                throw new InvalidOperationException("Error al intentyar actualizar el detalle");
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

        public async Task<Response> GetOrderDetailsById(int id)
        {
            try
            {
                Response res = new Response();
                OrderDetailsModel orderDetailsModel= await _orderDetailRepo.GetByIdOrderDetail(id);
                if (orderDetailsModel == null)
                {
                    throw new InvalidOperationException("No existe Un detatalle con el id " + id);
                }
                OrderDetailsDto orderDetailsDto = new OrderDetailsDto()
                {
                    Amount = orderDetailsModel.Amount,
                    Book = orderDetailsModel.Book.Title,
                    BookAuthor = orderDetailsModel.Book.Author,
                    BookPrice = orderDetailsModel.Book.Price,
                    Id = orderDetailsModel.Id,
                    IdBook = orderDetailsModel.IdBook,
                    IdOrder = orderDetailsModel.IdOrder,
                    Order = orderDetailsModel.Order.FechaPedido.ToString(),
                    Price = orderDetailsModel.Price,
                };
                res.Data = orderDetailsDto;
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
    }
}
