using acb_app.Models;
using BecamexIDC.Pattern.EF.Repositories;
using BecamexIDC.Pattern.Services;

public interface ICustomerService : IService<Customer> { }
    public class CustomerService : Service<Customer>, ICustomerService
    {
        public CustomerService(IRepositoryAsync<Customer> repository) : base(repository)
        {
        }
    }