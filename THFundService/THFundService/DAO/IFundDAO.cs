using System.Collections.Generic;
using WeerutTestWCFService.DTO;

namespace WeerutTestWCFService.DAO
{

    /// <summary>
    /// THFundService.DAO.IFundDAO is a generic Data Access Object (DAO) interface to
    /// provide APIs for interacting with a database.
    /// </summary>
    public interface IFundDAO
    {

        List<Fund> GetAllFunds();

        Fund GetFundByID(string fundID);

        void AddFund(Fund fundToBeAdded);

        bool UpdateFund(string fundID, Fund newFundInfo);

        bool DeleteFund(string fundID);

    }
}
