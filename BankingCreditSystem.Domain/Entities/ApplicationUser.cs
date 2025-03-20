using System;

namespace BankingCreditSystem.Domain.Entities{

    public class ApplicationUser : User{
        public string PhoneNumber {get;set;}= default;
        public string Address {get;set;}= default;

        public virtual Customer? Customer {get; set;}
        public Guid? CustomerId{get; set;}
    }
}