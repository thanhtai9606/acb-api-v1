
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
        private readonly IUnitOfWorkAsync _unitOfWork;
        private OperationResult operationResult = new OperationResult();
        public SaleController(ISaleService SaleService, IUnitOfWorkAsync unitOfWork)
        {
            _SaleService = SaleService;
            _unitOfWork = unitOfWork;
          
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
        [HttpPost, Route("UpdateSale")]
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
            return Ok(_SaleService.Queryable());
        }


    }
}
