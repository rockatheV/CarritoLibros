using ApiBook.Data.Repository.IRepository;
using ApiBook.Data.Services.IServices;
using ApiBook.Models;
using ApiBook.Models.DTOs;

namespace ApiBook.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<Response> GetAllUsers()
        {
            Response res=new Response();
            try{

                List<UserModel> users = await _userRepo.GetAllUsers();
                List<UserDto> usersDto = new List<UserDto>();
                foreach (UserModel user in users) 
                {
                    UserDto userDto = new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                    };
                    usersDto.Add(userDto);
                }
               

                res.Data = usersDto;
                res.Succes = true;
                res.Mesage="Ok";

                return res;

            }
            catch (Exception)
            {
                throw new Exception("Error en el servidor, Contactese con los administradores de el aplicativo");
            }
        }
        public async Task<Response> GetByIdUser(int id)
        {
            Response res = new Response();
            try
            {

                UserModel user = await _userRepo.GetByIdUser(id);
                if(user==null)
                {
                    throw new InvalidOperationException("No existe un usuario con el id " + id);
                }
                UserDto userDto = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };
                    
                res.Data = userDto;
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

        public async Task<Response>CreateUser(UserDto userDto)
        {
            Response res = new Response();
            try
            {
                if (userDto.Name=="")
                {
                    throw new InvalidOperationException("El nombre es obligatoio");
                }
                if (userDto.Email == "")
                {
                    throw new InvalidOperationException("El Email es obligatoio");
                }
                List<UserModel> ExisteUser=await _userRepo.GetAllUsers(x=>x.Email==userDto.Email);
                if (ExisteUser.Count>0)
                {
                    throw new InvalidOperationException("Ya existe un usuario con este email");
                }
                UserModel userCreate = new UserModel
                {
                    Name = userDto.Name,
                    Email = userDto.Email
                };
                bool save= await _userRepo.CreateUser(userCreate);
                if (!save)
                {
                    throw new InvalidOperationException("Error al intentar crear el usuario");
                }
                else
                {
                    res.Succes = true;
                    res.Mesage = "Ususario creado correctamente";
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

        public async Task<Response> EditUser(int id,UserDto userDto)
        {
            Response res = new Response();
            try
            {
                if (userDto.Id!=id)
                {
                    throw new InvalidOperationException("El id enviado por la url y el de el cuerpo de la peticion no coinciden");
                }
                if ( userDto.Id==0)
                {
                    throw new InvalidOperationException("El id es obligatorio");
                }
                if (string.IsNullOrEmpty(userDto.Name))
                {
                    throw new InvalidOperationException("El nombre es obligatorio");
                }
                if (string.IsNullOrEmpty(userDto.Email))
                {
                    throw new InvalidOperationException("El Email es obligatorio");
                }

                UserModel userToUpdate = await _userRepo.GetByIdUser((int)userDto.Id);
                if (userToUpdate == null)
                {
                    throw new InvalidOperationException("Usuario no encontrado");
                }

                List<UserModel> existingUsersWithEmail = await _userRepo.GetAllUsers(x => x.Email == userDto.Email && x.Id != userDto.Id);
                if (existingUsersWithEmail.Count > 0)
                {
                    throw new InvalidOperationException("Ya existe un usuario con este email");
                }

                userToUpdate.Name = userDto.Name;
                userToUpdate.Email = userDto.Email;

                bool save = await _userRepo.EditUser(userToUpdate);
                if (!save)
                {
                    throw new InvalidOperationException("Error al intentar editar el usuario");
                }
                else
                {
                    res.Succes = true;
                    res.Mesage = "Usuario editado correctamente";
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
