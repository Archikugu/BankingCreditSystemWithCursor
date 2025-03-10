using Microsoft.EntityFrameworkCore;

namespace BankingCreditSystem.Persistence.Repositories
{
    public class IndividualCustomerRepository : CustomerRepository<IndividualCustomer>, IIndividualCustomerRepository
    {
        public IndividualCustomerRepository(BaseDbContext context) : base(context)
        {
        }
    }
}