using KOPERASI_TANTERA.Web.Models.Entities;

namespace KOPERASI_TANTERA.Web.Models.Repository.Contract
{
    public interface IMemberRepository
    {
        Task<Member> RegisterMember(Member member);
    }
}
