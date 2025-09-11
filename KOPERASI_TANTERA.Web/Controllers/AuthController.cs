using Microsoft.AspNetCore.Mvc;

namespace KOPERASI_TANTERA.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
