using ApiBook.Data.Repository.IRepository;
using ApiBook.Data.Services.IServices;
using ApiBook.Models.DTOs;
using ApiBook.Models;

namespace ApiBook.Data.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        private IOrderDetailsRepository _orderDetailsRepo;
        public OrderService(IOrderRepository orderRepo, IUserRepository userRepo,IOrderDetailsRepository orderDetailsRepo)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _orderDetailsRepo = orderDetailsRepo;

        }

        public async Task<Response> GetAllOrdersByUser(int idUser)
        {
            Response res = new Response();
            try
            {
                UserModel userurl=await _userRepo.GetByIdUser(idUser);
                if (userurl == null)
                {
                    throw new InvalidOperationException("El usuario con el id "+ idUser +" no existe");
                }

                List<OrdersModel> orders = await _orderRepo.GetAllOrders(x=>x.IdUser==idUser);
                List<OrdersDto> ordersDto = new List<OrdersDto>();
                foreach (OrdersModel order in orders)
                {
                    List<OrderDetailsModel> details = await _orderDetailsRepo.GetAllOrderDetails(x => x.IdOrder == order.Id);
                    OrdersDto orderDto = new OrdersDto();
                    orderDto.Id = order.Id;
                    orderDto.IdUser = order.IdUser;
                    orderDto.FechaPedido= order.FechaPedido;
                    orderDto.Quantity= details.Count();
                    orderDto.PriceTotal = details.Sum(s => s.Amount);
                    ordersDto.Add(orderDto);
                }


                res.Data = ordersDto;
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

        public async Task<Response> CreateOrder(int idUser)
        {
            try{
                Response res = new Response();
                UserModel user = await _userRepo.GetByIdUser(idUser);
                if (user == null)
                {
                    throw new InvalidOperationException("No exte un usuario con el id "+ idUser);
                }
                OrdersModel orders = new OrdersModel()
                {
                    IdUser = idUser,
                    FechaPedido = DateTime.Now,
                };
                bool save= await _orderRepo.CreateOrder(orders);
                if (save)
                {
                    res.Succes= true;
                    res.Mesage = "Se creo la orden correctamente";
                    return res;
                }
                throw new InvalidOperationException("Error cuando se estaba creando la orden ");

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
