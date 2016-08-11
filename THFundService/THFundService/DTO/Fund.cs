using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace WeerutTestWCFService.DTO
{

    /// <summary>
    /// THFundService.DTO.Fund is Data Transfer Object (DTO) class that represents each LTF fund record and
    /// is transfered between service and Data Access Object (DAO) layers
    /// </summary>
    [Table("TH_LTF")]   // Database annotation
    [DataContract(Name = "fund")]   // XML.JSON Serialization annotation
    public class Fund
    {
      
        [Key]                   // Database annotation
        [Column("FUND_ID")]     // Database annotation
        [DataMember(Name = "fundID", IsRequired = true, Order = 0)]          // XML.JSON Serialization annotation
        public string ID { get; set; }

        [Column("FUND_NAME")]   // Database annotation
        [DataMember(Name = "fundName", IsRequired = true, Order = 1)]        // XML.JSON Serialization annotation
        public string Name { get; set; }

        [Column("AS_OF_DATE")]  // Database annotation
        [IgnoreDataMember]      // XML.JSON Serialization annotation
        public DateTime? AsOfDate { get; set; }

        [NotMapped]             // Database annotation
        [DataMember(Name = "asOfDate", EmitDefaultValue = false, Order = 2)] // XML.JSON Serialization annotation
        public string AsOfDateAsString
        {
            get { return AsOfDate.HasValue ? AsOfDate.Value.ToString("yyyy-MM-dd") : null; }
            set { AsOfDate = !string.IsNullOrEmpty(value) ? DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture) : default(DateTime?); }
        }

        [Column("NAV_PRICE")]   // Database annotation
        [DataMember(Name = "nav", EmitDefaultValue = false, Order = 3)]     // XML.JSON Serialization annotation
        public double? NAV { get; set; }

        [Column("OFFER_PRICE")] // Database annotation
        [DataMember(Name = "offer", EmitDefaultValue = false, Order = 4)]   // XML.JSON Serialization annotation
        public double? Offer { get; set; }

        [Column("BID_PRICE")]   // Database annotation
        [DataMember(Name = "bid", EmitDefaultValue = false, Order = 5)]     // XML.JSON Serialization annotation
        public double? Bid { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendFormat("Fund ID:    {0}", ID).AppendLine();
            sb.AppendFormat("Fund Name:  {0}", Name).AppendLine();
            sb.AppendFormat("As Of Date: {0:yyyy-MM-dd}", AsOfDate).AppendLine();
            sb.AppendFormat("NAV:        {0:#,0.####}", NAV).AppendLine();
            sb.AppendFormat("Offer:      {0:#,0.####}", Offer).AppendLine();
            sb.AppendFormat("Bid:        {0:#,0.####}", Bid).AppendLine();
            sb.AppendLine("==================================================");
            return sb.ToString();
        }

    }
}
