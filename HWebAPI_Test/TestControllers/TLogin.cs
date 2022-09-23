using Autofac.Extras.Moq;
using HWebAPI.Controllers;
using HWebAPI.IRepository;
using HWebAPI.Models;
using HWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HWebAPI_Test.TestControllers
{
    public class TLogin
    {
        private LoginController _loginController;
        private readonly Mock<ILoginRepository> _loginRepository = new Mock<ILoginRepository>();
        private readonly Mock<LoginRepository> loginRepository = new Mock<LoginRepository>();
        //private x mock;
        public TLogin()
        {
            _loginController = new LoginController(_loginRepository.Object);
        }

        [Fact]
        public async Task login_Success()
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>() 
            { Data = "Token", Message = "Login is Successfull!", Success = true };

            UserLogin userLogin = new UserLogin() { username = "login_User", password = "123456" };

            _loginRepository.Setup(x => x.Login(userLogin)).ReturnsAsync(serviceResponse);

            ActionResult<ServiceResponse<string>> result = await _loginController.Login(userLogin);

            Assert.IsType<ActionResult<ServiceResponse<string>>>(result);
        }
    }
}
