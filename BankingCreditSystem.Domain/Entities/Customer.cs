using System;
using BankingCreditSystem.Core.Repositories;
using BankingCreditSystem.Domain.Entities;

public abstract class Customer : Entity<Guid>
{
  
    public bool IsActive { get; set; }

    public virtual ApplicationUser User {get;set;}= default;
    public Guid UserId {get; set;}
    
    protected Customer()
    {
        IsActive = true;
    }
} 