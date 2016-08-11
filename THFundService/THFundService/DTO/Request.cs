using System.Runtime.Serialization;

namespace WeerutTestWCFService.DTO
{
    [DataContract(Name = "request")]
    public class Request
    {

        [DataMember(Name = "fund", IsRequired = true)]
        public Fund Fund;

        public override string ToString()
        {
            return Fund.ToString();
        }

    }
}
