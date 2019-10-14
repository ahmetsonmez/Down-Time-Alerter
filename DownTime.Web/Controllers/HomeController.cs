using DownTime.CommonModel.Models;
using DownTime.Core.Helper;
using DownTime.Services.Interface;
using DownTime.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace DownTime.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWebSiteService _webSiteService;
        private readonly IWebSiteRequestService _webSiteRequestService;
        public HomeController(IWebSiteService webSiteService, IWebSiteRequestService webSiteRequestService)
        {
            _webSiteService = webSiteService;
            _webSiteRequestService = webSiteRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DefineWebSite(int? id)
        {
      
            ViewBag.StatusList = GetStatusList.GetList();
            if (!id.HasValue) return View(new WebSiteDto());

            var model = _webSiteService.Get(Convert.ToInt32(id));
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (model?.UserId == Convert.ToInt32(userId))
                return View(model);
            
            return RedirectToAction("HandleErrorCode", "Error",new {statusCode="404"});
        }

        [HttpPost]
        public IActionResult DefineWebSite(WebSiteDto model)
        {
            ViewBag.StatusList = GetStatusList.GetList();
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            model.UserId = Convert.ToInt32(userId);            
            _webSiteService.CreateOrUpdate(model);
           
            return RedirectToAction("WebSiteList", "Home");
        }

        public IActionResult WebSiteList()
        {
            var model = _webSiteService.GetWebSites().ToList();
            return View(model);
        }

        public IActionResult WebSiteRequestList(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;          
            var model = _webSiteRequestService.GetWebSiteRequests(id).ToList();
            if (model.Any() && model.FirstOrDefault()?.WebSite.UserId == Convert.ToInt32(userId))
                return View(model);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
