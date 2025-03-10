public interface ICustomerRepository<T> : IAsyncRepository<T, Guid> where T : Customer
{
} 