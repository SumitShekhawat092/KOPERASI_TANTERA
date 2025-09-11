using KOPERASI_TANTERA.Web.Data;
using KOPERASI_TANTERA.Web.Models.Entities;
using KOPERASI_TANTERA.Web.Models.Repository.Contract;

namespace KOPERASI_TANTERA.Web.Models.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Member> RegisterMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
