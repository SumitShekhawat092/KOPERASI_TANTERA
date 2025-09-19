using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KOPERASI_TANTERA.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
