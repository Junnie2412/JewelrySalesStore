using JewelrySalesStore.Common;
using JewelrySalesStoreBusiness.Base;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreBusiness
{
    public interface IProductBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Product product);
        Task<IBusinessResult> Update(Product product);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class ProductBusiness : IProductBusiness
    {
        //private readonly ProductDAO _DAO;        
        //private readonly ProductRepository _ProductRepository;
        private readonly UnitOfWork _unitOfWork;

        public ProductBusiness()
        {
            //_ProductRepository ??= new ProductRepository();            
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _currencyRepository.GetAllAsync();
                var currencies = await _unitOfWork.ProductRepository.GetAllAsync();


                if (currencies == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currencies);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(string code)
        {
            try
            {
                #region Business rule
                #endregion

                //var  Product = await _ ProductRepository.GetByIdAsync(code);
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(code);

                if (Product == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Product);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Product Product)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync( Product);
                int result = await _unitOfWork.ProductRepository.CreateAsync(Product);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Product Product)
        {
            try
            {
                //int result = await _currencyRepository.UpdateAsync( Product);
                int result = await _unitOfWork.ProductRepository.UpdateAsync(Product);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(string code)
        {
            try
            {
                //var  Product = await _currencyRepository.GetByIdAsync(code);
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(code);
                if (Product != null)
                {
                    //var result = await _currencyRepository.RemoveAsync( Product);
                    var result = await _unitOfWork.ProductRepository.RemoveAsync(Product);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
