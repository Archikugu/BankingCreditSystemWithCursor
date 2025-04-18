using System;
using BankingCreditSystem.Core.Repositories;

public class User : Entity<Guid>    
{
    public string Email { get; set; }
   
   public byte[] PasswordSalt { get; set; }
   public byte[] PasswordHash { get; set; }

   public bool Status { get; set; }
}
