using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BankingCreditSystem.Application.Features.CreditApplications.Queries.GetList
{
    public class GetListCreditApplicationQuery : IRequest<Paginate<CreditApplicationResponse>>, ISecuredRequest
    {
        public string[] Roles => new[] { "Admin", "BankStaff" };
    }
}
