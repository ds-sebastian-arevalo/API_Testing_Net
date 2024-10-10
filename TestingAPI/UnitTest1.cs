using API_Usuarios.Controllers;
using API_Usuarios.Models;
using API_Usuarios.Responses;
using API_Usuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestingAPI
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Fact]
        public void AddUser_CuandoUsuarioEsValido_RetornaOK()
        {
            //Arrange
            var newUser = new User { Id = 0, Name = "Carlitos 16", Email = "carlitos16@gmail.com" };

            _mockUserService.Setup(s => s.AddUser(It.IsAny<User>())).Returns(newUser);

            //Act
            var result = _userController.AddUser(newUser);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result); //Verificacion de Ok
            var objectReturn = Assert.IsType<ObjectReturn<User>>(okResult.Value); //Verificacion de Object Return

            Assert.Equal(newUser, objectReturn.Data); //Verificacion que el usuario dentro de objectReturn sea el mismo que el newUser
            Assert.Equal("Usuario Creado Correctamente.", objectReturn.Info); //Verficacion del string de confirmacion
        }

        [Fact]
        public void AddUser_CuandoIdUsuarioEsDistintoDeCero_RetornaBadRequest()
        {
            // Arrange
            var usuarioInvalido = new User { Id = 1, Name = "Juan", Email = "juan@example.com" };

            // Act
            var result = _userController.AddUser(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Verificamos que sea BadRequest
            var objectReturn = Assert.IsType<ObjectReturn<User>>(badRequestResult.Value); // Verificamos que devuelve el ObjectReturn<User>

            Assert.Null(objectReturn.Data); // No debe haber ningún usuario en los datos devueltos
            Assert.Equal("El ID debe ser 0 al crear un nuevo usuario.", objectReturn.Info); // Verificacion mensaje error esperado
        }

        [Fact]
        public void AddUser_CuandoNombreEsVacio_RetornaBadRequest()
        {
            // Arrange
            var usuarioInvalido = new User { Id = 0, Name = "", Email = "juan@example.com" }; // Nombre vacío

            // Act
            var result = _userController.AddUser(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Verificamos que sea BadRequest
            var objectReturn = Assert.IsType<ObjectReturn<User>>(badRequestResult.Value); // Verificamos que devuelve ObjectReturn<User>

            // Verificamos el mensaje de error esperado
            Assert.Null(objectReturn.Data); // No debe haber ningún usuario en los datos devueltos
            Assert.Equal("El Nombre del Usurio es obligatorio.", objectReturn.Info); // Mensaje de error esperado
        }

        [Fact]
        public void AddUser_CuandoEmailEsVacio_RetornaBadRequest()
        {
            // Arrange
            var usuarioInvalido = new User { Id = 0, Name = "Juan", Email = "" }; // Email vacio

            // Act
            var result = _userController.AddUser(usuarioInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Verificamos que sea BadRequest
            var objectReturn = Assert.IsType<ObjectReturn<User>>(badRequestResult.Value); // Verificamos que devuelve ObjectReturn<User>

            // Verificamos el mensaje de error esperado
            Assert.Null(objectReturn.Data); // No debe haber ningún usuario en los datos devueltos
            Assert.Equal("El Email del Usurio es obligatorio.", objectReturn.Info); // Mensaje de error esperado
        }



    }
}