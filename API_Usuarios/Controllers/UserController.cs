using API_Usuarios.Responses;
using API_Usuarios.Models;
using API_Usuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using API_Usuarios.Validations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #region GetUsers
        [HttpGet("GetUserList")]
        public IActionResult GetUserList()
        {
            try
            {
                var userList = userService.GetUserList()?.ToList();

                if (userList == null || userList.Count == 0)
                {
                    return NotFound(new ObjectReturn<List<User>>(null, "No users found."));
                }

                return Ok(new ObjectReturn<List<User>>(userList, "Users retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectReturn<List<User>>(null, "An unexpected error occurred."));
            }
        }

        #endregion

        #region AddUser
        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {

            try
            {
                //Validacion de que el UserId no venga lleno
                if (user.Id != 0)
                {
                    return BadRequest(new ObjectReturn<User>(null, "El ID debe ser 0 al crear un nuevo usuario."));
                }

                //Validacion de que el Nombre del Usuario no venga vacio
                if (string.IsNullOrEmpty(user.Name))
                {
                    return BadRequest(new ObjectReturn<User>(null, "El Nombre del Usurio es obligatorio."));
                }

                //Validacion de que el Email del Usuario no venga vacio
                if (string.IsNullOrEmpty(user.Email))
                {
                    return BadRequest(new ObjectReturn<User>(null, "El Email del Usurio es obligatorio."));
                }

                if (!EmailValidator.IsValidEmail(user.Email))
                {
                    return BadRequest(new ObjectReturn<User>(null, "El Email del Usurio debe tener un formato correcto."));
                }

                var result = userService.AddUser(user);
                return Ok(new ObjectReturn<User>(result, "Usuario Creado Correctamente."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ObjectReturn<User>(null, e.Message));
            }
        }
        #endregion

        #region UpdateUser
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                var result = userService.UpdateUser(user);
                return Ok(new ObjectReturn<User>(result, "Usuario Editado Correctamente."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ObjectReturn<List<User>>(null, e.Message));
            }

        }
        #endregion

        #region DeleteUser
        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var result = userService.DeleteUser(userId);
                return Ok(new ObjectReturn<bool>(result, "Usuario Eliminado Correctamente."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ObjectReturn<bool>(false, e.Message));
            }

        }
        #endregion

    }
}
