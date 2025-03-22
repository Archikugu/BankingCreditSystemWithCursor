using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Create;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateIndividualCustomerRequest, IndividualCustomer>();

            CreateMap<IndividualCustomer, IndividualCustomerResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address));

            CreateMap<IndividualCustomer, CreatedIndividualCustomerResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            CreateMap<IndividualCustomer, UpdatedIndividualCustomerResponse>();

            CreateMap<UpdateIndividualCustomerRequest, IndividualCustomer>();

            CreateMap<CreateIndividualCustomerCommand, IndividualCustomer>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Request.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Request.LastName))
            .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.Request.NationalId))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Request.DateOfBirth))
            .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.Request.MotherName))
            .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.Request.FatherName))
            .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.Request.PhoneNumber))
            .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Request.Email))
            .ForPath(dest => dest.User.Address, opt => opt.MapFrom(src => src.Request.Address));


            CreateMap<Paginate<IndividualCustomer>, Paginate<IndividualCustomerResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        }
    }
}