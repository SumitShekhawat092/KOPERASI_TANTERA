using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KOPERASI_TANTERA.Web.Areas.Member.Controllers
{
    [Authorize]
    [Area("Member")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
