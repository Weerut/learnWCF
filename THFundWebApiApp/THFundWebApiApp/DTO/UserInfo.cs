using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;

namespace ThomsonReuters.Eikon.THFundWebApiApp.DTO
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string UUID;
        [DataMember]
        public string Title;
        [DataMember]
        public string FirstName;
        [DataMember]
        public string LastName;
        [DataMember]
        public string EmailAddress;

        /// <summary>
        /// Create UserInfo instance from UserInfo.UserDetails returned from UserInfoService
        /// </summary>
        /// <param name="userDetails">UserInfo.UserDetails returned from UserInfoService</param>
        /// <returns>UserInfo instance</returns>
        public static UserInfo CreateUserInfoFromUserDetails(IEnumerable<KeyValuePair<string, string>> userDetails)
        {
            return Mapper.Map<UserInfo>(userDetails);
        }

        #region AutoMapper
        /// <summary>
        /// AutoMapper library is used for mapping properties of Service References generated class to properties of DTO class
        /// </summary>
        static UserInfo()
        {
            Mapper.CreateMap<IEnumerable<KeyValuePair<string, string>>, DTO.UserInfo>()
                .ForMember(dest => dest.UUID,         opt => opt.MapFrom(src => src.Where(x => x.Key == "UUID").Select(x => x.Value).SingleOrDefault()))
                .ForMember(dest => dest.Title,        opt => opt.MapFrom(src => src.Where(x => x.Key == "ContactTitle").Select(x => x.Value).SingleOrDefault()))
                .ForMember(dest => dest.FirstName,    opt => opt.MapFrom(src => src.Where(x => x.Key == "FirstName").Select(x => x.Value).SingleOrDefault()))
                .ForMember(dest => dest.LastName,     opt => opt.MapFrom(src => src.Where(x => x.Key == "LastName").Select(x => x.Value).SingleOrDefault()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Where(x => x.Key == "EmailAddress").Select(x => x.Value).SingleOrDefault()));
        }
        #endregion

    }
}
