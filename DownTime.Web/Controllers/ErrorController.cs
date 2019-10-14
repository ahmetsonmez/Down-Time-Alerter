using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DownTime.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the page you requested could not be found";
                    
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry something went wrong on the server";                    
                    break;
                default:
                    ViewBag.ErrorMessage = "Sorry ";
                    break;
            }

            return View();
        }
    }
}