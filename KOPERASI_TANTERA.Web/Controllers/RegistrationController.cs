using KOPERASI_TANTERA.Web.Models.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KOPERASI_TANTERA.Web.Controllers
{
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        [Route("take-a-few-minutes")]
        public IActionResult Index()
        {
            return View();
        }

        #region step-one

        [HttpGet]
        [Route("step-one")]
        public IActionResult one()
        {
            StepOneModel steponemodel = new StepOneModel();
            return View(steponemodel);
        }
        [HttpPost]
        [Route("step-one")]
        public IActionResult one(StepOneModel model)
        {
            if (!ModelState.IsValid)
            {
            return View(model);
            } 

            HttpContext.Session.SetString("stepone_data", JsonConvert.SerializeObject(model));
            return RedirectToAction("step-two");
        }
        #endregion

        #region step-two

        [HttpGet]
        [Route("step-two")]
        public IActionResult two()
        {
            string jsonData = HttpContext.Session.GetString("stepone_data") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(jsonData))
                return RedirectToAction("step-one");

            var stepTwo = new StepTwoModel()
            {
                IsAccept = false
            };
            
            return View(stepTwo);
        }

        [HttpPost]
        [Route("step-two")]
        public IActionResult two(StepTwoModel model)
        {
            if(ModelState.IsValid)
            {
                string jsonData = HttpContext.Session.GetString("stepone_data") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(jsonData))
                    return RedirectToAction("step-one");


            }
            

            return RedirectToAction("three");
            //return View(model);
        }

        #endregion

        #region step-three
        [HttpGet("step-three")]
        public IActionResult three()
        {
            return View();
        }

        [HttpPost("step-three")]
        public IActionResult three(string[] pin, string[] confirm)
        {
            //if(!ModelState.IsValid)

            //member details: 

            return RedirectToAction("step-four");
        }


        #endregion

        #region step-four
        [Route("step-four")]
        public IActionResult four()
        {
            return View();
        }

        #endregion
    }
}
