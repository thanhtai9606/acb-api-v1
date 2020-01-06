using acb_app.Models;
using BecamexIDC.Pattern.EF.Repositories;
using BecamexIDC.Pattern.Services;

public interface ISaleHeaderService : IService<SaleHeader> { }
    public class SaleHeaderService : Service<SaleHeader>, ISaleHeaderService
    {
        public SaleHeaderService(IRepositoryAsync<SaleHeader> repository) : base(repository)
        {
        }
    }