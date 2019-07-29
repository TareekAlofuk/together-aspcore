using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using together_aspcore.App.Member;
using together_aspcore.App.Service;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;

namespace together_aspcore.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpGet("all")]
        public async Task<ActionResult<List<ServiceDetail>>> GetServicesDetails(
            [FromQuery] MembershipType membershipType)
        {
            var services = await _serviceService.GetServiceDetails(membershipType);
            return Ok(services);
        }


        [HttpPost]
        public async Task<ActionResult> RegisterService(
            [FromForm] RegisterServiceRequestModel registerServiceRequestModel)
        {
            var serviceUsage = registerServiceRequestModel.toServiceUsage();
            await _serviceService.RegisterService(serviceUsage);
            return Ok();
        }
    }
}