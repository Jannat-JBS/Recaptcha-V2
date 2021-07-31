using ApplicationServices.Business;
using ApplicationServices.Business.IAppServices;
using Domain.Contracts;
using Domain.Contracts.V1;
using FalconITScrapperWebAPI.ActionFilters;
using LoggerService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconITScrapperWebAPI.Controllers.V1.SIV
{
    [ApiController]
    public class HomeController : Controller
    {
        public ISearchNumberPlate _ISearchNumberPlate { get; set; }
        //public ISearchNumberPlate _ISearchProxy { get; set; }
        private readonly ILoggerManager _logger;
        public HomeController(ISearchNumberPlate ISearchNumberPlate, ILoggerManager logger)
        {
            _ISearchNumberPlate = ISearchNumberPlate;
            _logger = logger;
        }
        //[ServiceFilter(typeof(ValidationFilterModelAttribute))]
        [HttpPost(ApiRoutes.SIV.GetNumberPlates)]
        public IActionResult Index([FromBody] NumberPlateContract numberPlateContract)
        {
            //throw new ArgumentNullException();
            ViewData.Model = _ISearchNumberPlate.testSearchNumberPlate(numberPlateContract.NumberPlate,numberPlateContract.Proxy);
            return Ok(ViewData.Model);


        }
    }
}
