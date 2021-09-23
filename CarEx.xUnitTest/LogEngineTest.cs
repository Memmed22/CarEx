using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Log.Model;
using CarEx.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using Xunit;

namespace CarEx.xUnitTest
{
    public class LogEngineTest
    {
        private  LogEngine _sud;
        //private  IWebHostEnvironment _webHost;
        public LogEngineTest()
        {
           
            _sud = new LogEngine(@"C:\Users\User\source\repos\CarEx\CarEx.Web\wwwroot");
        }

        [Fact]
        public void WriteLogCreateJsonFile()
        {
          
            //LogModel model = new LogModel
            //{
            //    Api = "test4",
            //    Code = Guid.NewGuid().ToString(),
            //    DateTime = DateTime.Now,
            //    Exception = "test",
            //    InnerException = "test",
            //    Method = "test",
            //    Name = "tst"
            //};
            //_sud.WriteLog(model);
        }
    }
}
