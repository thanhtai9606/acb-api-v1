using acb_app.Models;
using BecamexIDC.Pattern.EF.Repositories;
using BecamexIDC.Pattern.Services;

public interface IProductService : IService<Product> { }
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IRepositoryAsync<Product> repository) : base(repository)
        {
        }
    }