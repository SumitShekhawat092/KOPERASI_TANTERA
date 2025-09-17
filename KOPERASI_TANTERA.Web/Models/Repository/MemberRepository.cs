using KOPERASI_TANTERA.Web.Data;
using KOPERASI_TANTERA.Web.Models.Entities;
using KOPERASI_TANTERA.Web.Models.Auth;
using KOPERASI_TANTERA.Web.Models.Repository.Contract;
using Microsoft.AspNetCore.Identity;

namespace KOPERASI_TANTERA.Web.Models.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Member> _userManager;
        //private readonly SignInManager<ApplicationDbContext> _signInManager;
        public MemberRepository(ApplicationDbContext dbContext, UserManager<Member> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterMember(StepOneModel model)
        {
            //save member details here: 
            var memberModel = new Member
            {
                Name = model.CustomerName,
                ICNumber = model.ICNumber,
                PhoneNumber = model.MobileNumber,
                Email = model.EmailAddress,
                UserName = model.EmailAddress
            };
           
            var result = await _userManager.CreateAsync(memberModel, model.PasswordPIN);
            if (result.Succeeded)
            {

            }
            return result;
        }
    }
}
