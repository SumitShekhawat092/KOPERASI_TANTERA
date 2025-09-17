using KOPERASI_TANTERA.Web.Models.Entities;
using KOPERASI_TANTERA.Web.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace KOPERASI_TANTERA.Web.Models.Repository.Contract
{
    public interface IMemberRepository
    {
        Task<IdentityResult> RegisterMember(StepOneModel member);
    }
}
