using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Rules;
using BankingCreditSystem.Application.Features.CorporateCustomers.Constants;
using BankingCreditSystem.Application.Features.IndividualCustomers.Constants;
using BankingCreditSystem.Domain.Entities;
using BankingCreditSystem.Core.Security.Hashing;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Create
{
    public class CreateIndividualCustomerCommand : IRequest<CreatedIndividualCustomerResponse>
    {
        public CreateIndividualCustomerRequest Request { get; set; } = default!;

        public class CreateIndividualCustomerCommandHandler : IRequestHandler<CreateIndividualCustomerCommand, CreatedIndividualCustomerResponse>
        {
            private readonly IIndividualCustomerRepository _individualCustomerRepository;
            private readonly IMapper _mapper;
            private readonly IndividualCustomerBusinessRules _businessRules;

            public CreateIndividualCustomerCommandHandler(
                IIndividualCustomerRepository individualCustomerRepository,
                IMapper mapper,
                IndividualCustomerBusinessRules businessRules)
            {
                _individualCustomerRepository = individualCustomerRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedIndividualCustomerResponse> Handle(CreateIndividualCustomerCommand command, CancellationToken cancellationToken)
            {
                await _businessRules.NationalIdCannotBeDuplicatedWhenInserted(command.Request.NationalId);

                //Create ApplicationUser

                var applicationUser = new ApplicationUser{
                    Email=command.Request.Email,
                    PhoneNumber = command.Request.PhoneNumber,
                    Address = command.Request.Address,
                    Status = true
                };

                //Create password hash
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(command.Request.Password, out passwordHash,out passwordSalt);
                applicationUser.PasswordHash = passwordHash;
                applicationUser.PasswordSalt = passwordSalt;

                var individualCustomer = _mapper.Map<IndividualCustomer>(command.Request);
                individualCustomer.User = applicationUser;
                individualCustomer.IsActive = true;

                var createdCustomer = await _individualCustomerRepository.AddAsync(individualCustomer);

                var response = _mapper.Map<CreatedIndividualCustomerResponse>(createdCustomer);
                response.Message = IndividualCustomerMessages.CustomerCreated;

                return response;
            }
        }
    }
}