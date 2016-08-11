using System.Data.Entity;
using WeerutTestWCFService.DTO;

namespace WeerutTestWCFService.DAO
{
    /// <summary>
    /// THFundService.DAO.THFundDbContext is Entity Framework Database Context class
    /// that creates connections to a physical database by using the connection string "THFundDBConnectionString"
    /// which defined in App.config
    /// </summary>
    public class THFundDbContext : DbContext
    {

        /// <summary>
        /// Entity Framework DbSet representing TH_LTF database table
        /// </summary>
        public DbSet<Fund> TH_LTF { get; set; }

        public THFundDbContext() : base("THFundDBConnectionString")
        {
        }

    }
}
