using Microsoft.EntityFrameworkCore;

namespace BankingCreditSystem.Persistence.Repositories
{
    public class CorporateCustomerRepository : CustomerRepository<CorporateCustomer>, ICorporateCustomerRepository
    {
        public CorporateCustomerRepository(BaseDbContext context) : base(context)
        {
        }
    }
}