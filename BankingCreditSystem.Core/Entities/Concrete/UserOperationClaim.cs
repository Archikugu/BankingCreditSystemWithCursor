using BankingCreditSystem.Core.Repositories;

public class UserOperationClaim : Entity<int>
{
    public Guid UserId { get; set; }
    public string OperationClaimId { get; set; }

    public virtual User User { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }
} 