using KOPERASI_TANTERA.Web.Models.Auth;
using KOPERASI_TANTERA.Web.Models.Entities;
using KOPERASI_TANTERA.Web.Models.Repository.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace KOPERASI_TANTERA.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly SignInManager<Member> _signInManager;
        public AuthController(IMemberRepository memberRepository,
            SignInManager<Member> signInManager)
        {
            _memberRepository = memberRepository;
            _signInManager = signInManager;
        }

        [Route("register/take-a-few-minutes")]
        public IActionResult Index()
        {
            return View();
        }

        #region step-one

        [HttpGet]
        [Route("register/step-one")]
        public IActionResult one()
        {
            StepOneModel steponemodel = new StepOneModel();
            return View(steponemodel);
        }
        [HttpPost]
        [Route("register/step-one")]
        public IActionResult one(StepOneModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpContext.Session.SetString("stepone_data", JsonConvert.SerializeObject(model));
            return RedirectToAction("two", "Auth");
        }
        #endregion

        #region step-two

        [HttpGet]
        [Route("register/step-two")]
        public IActionResult two()
        {
            string jsonData = HttpContext.Session.GetString("stepone_data") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(jsonData))
                return RedirectToAction("one", "Auth");

            var stepTwo = new StepTwoModel()
            {
                IsAccept = false
            };

            return View(stepTwo);
        }

        [HttpPost]
        [Route("register/step-two")]
        public IActionResult two(StepTwoModel model)
        {
            if (ModelState.IsValid)
            {
                string jsonData = HttpContext.Session.GetString("stepone_data") ?? string.Empty;
                if (string.IsNullOrWhiteSpace(jsonData))
                    return RedirectToAction("one", "Auth");
            }
            return RedirectToAction("three", "Auth");
        }

        #endregion

        #region step-three
        [HttpGet]
        [Route("register/step-three")]
        public IActionResult three()
        {
            return View();
        }

        [HttpPost]
        [Route("register/step-three")]
        public async Task<IActionResult> threeAsync(string[] pin, string[] confirm)
        {
            var IsConfirm = pin.SequenceEqual(confirm);
            if (!IsConfirm)
            {
                ModelState.AddModelError("StepThree", "Confirm PIN Invalid");
                return View();
            }
            var PIN = string.Concat(pin);

            string jsonData = HttpContext.Session.GetString("stepone_data") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(jsonData))
                return RedirectToAction("one", "Auth");

            StepOneModel steponeModel = JsonConvert.DeserializeObject<StepOneModel>(jsonData);
            if (steponeModel is not null)
            {
                steponeModel.PasswordPIN = PIN;
                //register new member
                var resultModel = await _memberRepository.RegisterMember(steponeModel);
                if (resultModel.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,steponeModel.EmailAddress),
                        new Claim(ClaimTypes.Email,steponeModel.EmailAddress),
                        new Claim("User","true")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var claimsPrinciple = new ClaimsPrincipal(claimIdentity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrinciple);

                    // Redirect to Dashboard
                    return RedirectToAction("index", "Dashboard", new { area = "member" });
                }
                else
                {
                    foreach (var error in resultModel.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                    //add into logs
                    return View();
                }
            }
            return RedirectToAction("one", "Auth");
        }

        #endregion

        [HttpGet]
        [Route("login")]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(CredentialViewModel credentialViewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Member/Dashboard");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    credentialViewModel.Email,
                    credentialViewModel.Password,
                    credentialViewModel.RememberMe,
                    false);
                if (result.Succeeded)
                {
                    // Assign Claims here for Authorization
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,credentialViewModel.Email),
                        new Claim(ClaimTypes.Email,credentialViewModel.Email),
                        new Claim("User","true")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                    var claimsPrinciple = new ClaimsPrincipal(claimIdentity);

                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrinciple);

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("login", "Fail to login");
                }
            }
            return View(credentialViewModel);
        }

        [HttpGet]
        [Route("access-denied")]
        public IActionResult AccessDenied()
        {

            return View();
        }
    }
}
