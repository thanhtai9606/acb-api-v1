using acb_app.Models;
using BecamexIDC.Pattern.EF.Repositories;
using BecamexIDC.Pattern.Services;

public interface ISaleService : IService<Sale> { }
    public class SaleService : Service<Sale>, ISaleService
    {
        public SaleService(IRepositoryAsync<Sale> repository) : base(repository)
        {
        }
    }