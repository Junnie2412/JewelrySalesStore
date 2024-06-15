using JewelrySalesStoreData.Models;
using JewelrySalesStoreData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData
{
    public class UnitOfWork
    {
        private Net1702_221_4_JewelrySalesStoreContext _unitOfWorkContext;
        private CategoryRepository _category;
        private ProductRepository _product;
        private CompanyRepository _company;
        private OrderDetailRepository _detail;
        private PromotionRepository _promotion;
        private CustomerRepository _customer;
        private OrderRepository _order;

        public UnitOfWork() { 
            _unitOfWorkContext ??= new Net1702_221_4_JewelrySalesStoreContext();
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                //return _category ??= new Repository.CategoryRepository();
                return _category ??= new Repository.CategoryRepository(_unitOfWorkContext);
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                return _product ??= new Repository.ProductRepository(_unitOfWorkContext);
            }
        }
        public CompanyRepository CompanyRepository
        {
            get
            {
                return _company ??= new Repository.CompanyRepository(_unitOfWorkContext);
            }
        }

        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _detail ??= new Repository.OrderDetailRepository(_unitOfWorkContext);
            }
        }

        public PromotionRepository PromotionRepository
        {
            get
            {
                return _promotion ??= new Repository.PromotionRepository();
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                //return _customer ??= new Repository.CustomerRepository();
                return _customer ??= new Repository.CustomerRepository();
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                return _order ??= new Repository.OrderRepository(_unitOfWorkContext);
            }
        }
    }
}
