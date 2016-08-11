using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WeerutTestWCFService.DTO;

namespace WeerutTestWCFService.DAO
{

    /// <summary>
    /// THFundService.DAO.THLTFFundDAO is Database Access Object (DAO) concrete class
    /// that contains implementations for interacting with LocalDB\THFundDB.sqlite
    /// </summary>
    public class THLTFFundDAO : IFundDAO
    {

        #region GetAllFunds
        public virtual List<Fund> GetAllFunds()
        {
            using (var context = new THFundDbContext())
            {
                var query = from fund in context.TH_LTF
                            orderby fund.ID
                            select fund;
                return query.ToList();
            }
        }
        #endregion

        #region GetFundByID
        public virtual Fund GetFundByID(string fundID)
        {
            using (var context = new THFundDbContext())
            {
                return GetFundByID(fundID, context);
            }
        }

        public virtual Fund GetFundByID(string fundID, THFundDbContext context)
        {
            var query = from fund in context.TH_LTF
                        where fund.ID == fundID
                        select fund;
            return query.SingleOrDefault();
        }
        #endregion

        #region AddFund
        public virtual void AddFund(Fund fundToBeAdded)
        {
            using (var context = new THFundDbContext())
            {
                var entry = context.Entry(fundToBeAdded);
                entry.State = EntityState.Added;
                context.SaveChanges();
            }
        }
        #endregion

        #region UpdateFund
        public virtual bool UpdateFund(string fundID, Fund newFundInfo)
        {
            var success = false;
            using (var context = new THFundDbContext())
            {
                var originalInfo = GetFundByID(fundID, context);
                if (originalInfo != null)
                {
                    var entry = context.Entry(originalInfo);
                    entry.CurrentValues.SetValues(newFundInfo);
                    entry.State = EntityState.Modified;
                    success = context.SaveChanges() > 0;
                }
            }
            return success;
        }
        #endregion

        #region DeleteFund
        public virtual bool DeleteFund(string fundID)
        {
            var success = false;
            using (var context = new THFundDbContext())
            {
                var fundToBeDeleted = GetFundByID(fundID, context);
                if (fundToBeDeleted != null)
                {
                    var entry = context.Entry(fundToBeDeleted);
                    entry.State = EntityState.Deleted;
                    success = context.SaveChanges() > 0;
                }
            }
            return success;
        }
        #endregion

    }
}
