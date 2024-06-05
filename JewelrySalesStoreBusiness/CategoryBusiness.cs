using JewelrySalesStore.Common;
using JewelrySalesStoreBusiness.Base;
using JewelrySalesStoreData;
using JewelrySalesStoreData.DAO;
using JewelrySalesStoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreBusiness
{
    public interface ICategoryBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> Save(Category category);
        Task<IBusinessResult> Update(Category category);
        Task<IBusinessResult> DeleteById(Guid code);
    }
    public class CategoryBusiness : ICategoryBusiness
    {
        //private readonly CategoryDAO _DAO;        
        //private readonly CategoryRepository _CategoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryBusiness()
        {
            //_CategoryRepository ??= new CategoryRepository();            
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
                var currencies = await _unitOfWork.CategoryRepository.GetAllAsync();


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

        public async Task<IBusinessResult> GetById(Guid code)
        {
            try
            {
                #region Business rule
                #endregion

                //var  Category = await _ CategoryRepository.GetByIdAsync(code);
                var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(code);

                if (Category == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Category);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Category Category)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync( Category);
                int result = await _unitOfWork.CategoryRepository.CreateAsync(Category);
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

        public async Task<IBusinessResult> Update(Category Category)
        {
            try
            {
                //int result = await _currencyRepository.UpdateAsync( Category);
                int result = await _unitOfWork.CategoryRepository.UpdateAsync(Category);

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

        public async Task<IBusinessResult> DeleteById(Guid code)
        {
            try
            {
                //var  Category = await _currencyRepository.GetByIdAsync(code);
                var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(code);
                if (Category != null)
                {
                    //var result = await _currencyRepository.RemoveAsync( Category);
                    var result = await _unitOfWork.CategoryRepository.RemoveAsync(Category);
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


