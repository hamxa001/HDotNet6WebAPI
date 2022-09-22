using HWebAPI.Models;
using Newtonsoft.Json;
using System.Net;

namespace UnitTest_MsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mockEmps = new UserLogin() { username= "asdf", password="asdf23Dab"};

            var employeeRepositoryMock = TestInitializer.MockLoginRepository;
            employeeRepositoryMock.Setup
                  (x => x.Login(mockEmps)).Returns(Task.FromResult(new ServiceResponse<string>() { Data= "awer", Message = "asdfwer", Success = true}));

            var response = TestInitializer.TestHttpClient.GetAsync("api/login").Result;

            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.AreEqual(mockEmps[0].EmpId, responseData[0].EmpId);
        }
    }
}