
using System.Linq;
using System.Threading.Tasks;
using acb_app.Models;
using BecamexIDC.Common;
using BecamexIDC.Pattern.EF.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acb_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "Bearer")] // waring have to use this
    //[Authorize] This is not working
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _SaleService;
        private readonly ICustomerService _CustomerService;
        private readonly IProductService _ProductService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private OperationResult operationResult = new OperationResult();
        public SaleController(ISaleService SaleService, 
                                ICustomerService customerService,
                                IProductService productService,
                              IUnitOfWorkAsync unitOfWork)
        {
            _SaleService = SaleService;
            _unitOfWork = unitOfWork;
            _CustomerService = customerService;
            _ProductService = productService;
          
        }
        [HttpPost, Route("AddSale")]
        public async Task<IActionResult> AddSale(Sale Sale)
        {
            try
            {
               // var product = _unitOfWork.
                _SaleService.Add(Sale);
                int res = await _unitOfWork.SaveChangesAsync();
                if (res > 0)
                {
                    operationResult.Success = true;
                    operationResult.Message = "Added new record";
                    operationResult.Caption = "Add complete";
                }

            }
            catch (System.Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = ex.ToString();
                operationResult.Caption = "Add failed!";
            }
            return Ok(operationResult);
        }
        [HttpPut, Route("UpdateSale")]
        public async Task<IActionResult> UpdateSale(Sale Sale)
        {
            try
            {
                _SaleService.Update(Sale);
                int res =  await _unitOfWork.SaveChangesAsync();
                if (res > 0)
                {
                    operationResult.Success = true;
                    operationResult.Message = "Update success";
                    operationResult.Caption = "Update complete";
                }

            }
            catch (System.Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = ex.ToString();
                operationResult.Caption = "Update failed!";
            }
            return Ok(operationResult);
        }
        
        [HttpDelete, Route("DeleteSale")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                _SaleService.Delete(id);
               int res =  await _unitOfWork.SaveChangesAsync();
                if (res > 0)
                {
                    operationResult.Success = true;
                    operationResult.Message = "Delete success";
                    operationResult.Caption = "Delete complete";
                }

            }
            catch (System.Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = ex.ToString();
                operationResult.Caption = "Delete failed!";
            }
            return Ok(operationResult);
        }
        [HttpGet, Route("GetSale")]
        public IActionResult GetSale()
        {
            var sale = _SaleService.Queryable().ToList();
            var customer = _CustomerService.Queryable().ToList();
            var product = _ProductService.Queryable().ToList();

            var result = from s in sale
                         join c in customer on s.CustomerId equals c.CustomerId
                         join p in product on s.ProductId  equals p.ProductId
                         select new
                         {
                             s.SoId,
                             c.CustomerName,
                             c.Phone,
                             p.ProductName,
                             p.Model,
                             p.Warranty,
                             s.Quantity,
                             s.WarrantyStart,
                             s.WarrantyEnd,
                             s.ModifiedDate
                         };
            return Ok(result);
        }

        [HttpGet, Route("GetProducts")]
        public IActionResult GetProducts()
        {            
            var product = _ProductService.Queryable().ToList();
            var result = from p in product                        
                         select new
                         {
                             id = p.ProductId.ToString(),
                             text = p.ProductName                            
                         };

            
            return Ok(result);
        }
        [HttpGet, Route("GetCustomers")]
        public IActionResult GetCustomers()
        {            
            var customer = _CustomerService.Queryable().ToList();
            var result = from c in customer                        
                         select new
                         {
                             id = c.CustomerId.ToString(),
                             text = c.CustomerName                            
                         };

            
            return Ok(result);
        }

    }
}
