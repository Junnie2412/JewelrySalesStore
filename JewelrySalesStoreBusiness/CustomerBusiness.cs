using JewelrySalesStore.Common;
using JewelrySalesStoreBusiness.Base;
using JewelrySalesStoreData;

using JewelrySalesStoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreBusiness
{

    public interface ICustomerBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Update(Customer customer);
        Task<IBusinessResult> DeleteById(Guid code);
    }

    public class CustomerBusiness : ICustomerBusiness
    {
        //private readonly CustomerDAO _DAO;        
        //private readonly CustomerRepository _CustomerRepository;
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness()
        {
            //_CustomerRepository ??= new CustomerRepository();            
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _CustomerRepository.GetAllAsync();
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();


                if (customers == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
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

                //var Customer = await _CustomerRepository.GetByIdAsync(code);
                var Customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);

                if (Customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Customer Customer)
        {
            try
            {
                //int result = await _CustomerRepository.CreateAsync(Customer);
                int result = await _unitOfWork.CustomerRepository.CreateAsync(Customer);
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

        public async Task<IBusinessResult> Update(Customer Customer)
        {
            try
            {
                //int result = await _CustomerRepository.UpdateAsync(Customer);
                int result = await _unitOfWork.CustomerRepository.UpdateAsync(Customer);

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
                //var Customer = await _CustomerRepository.GetByIdAsync(code);
                var Customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
                if (Customer != null)
                {
                    //var result = await _CustomerRepository.RemoveAsync(Customer);
                    var result = await _unitOfWork.CustomerRepository.RemoveAsync(Customer);
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

