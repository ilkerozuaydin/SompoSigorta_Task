using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetProposal([FromBody]GetProposalRequest request)
        {
            var apiRequest = new { Object = request, Authentication = new { Source = "SOMPO", Key = "77lTCSn41w" } };
            var client = _httpClientFactory.CreateClient("Sompo");
            var bodyParamters = new StringContent(JsonConvert.SerializeObject(apiRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/sample/engine", bodyParamters);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var proposalResponse = JsonConvert.DeserializeObject<GetProposalResponse>(stringResponse);
            return Json(proposalResponse);
        }

        [HttpPost]
        public IActionResult GetProposalTable([FromBody]ProposalStatusModel model)
        {
            return PartialView("_PartialProposalStatusTable", model);
        }
    }
}
