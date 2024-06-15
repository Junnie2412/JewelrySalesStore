using JewelrySalesStoreData.DAO;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData
{
    public class UnitOfWork : GenericRepository<Promotion>
    {   
        private Net1702_221_4_JewelrySalesStoreContext _unitOfWorkContext;
        private PromotionRepository _Promotion;
        public UnitOfWork()
        {
            _unitOfWorkContext = new Net1702_221_4_JewelrySalesStoreContext();
        }
        public PromotionRepository PromotionRepository
        {
            get
            {
                // return _Promotion ??= new Repository.PromotionRepository();
                return _Promotion ??= new Repository.PromotionRepository(_unitOfWorkContext);

            }
        }
    }
}
