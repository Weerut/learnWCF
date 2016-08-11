using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThomsonReuters.Eikon.THFundWebApiApp.THFundService;
//using ThomsonReuters.Eikon.THFundWebApiApp.UserInfoService;
using ThomsonReuters.Eikon.Toolkit.Interfaces;
using TR.AppServer.Common.Interfaces;
using Wcf.Routing;

namespace ThomsonReuters.Eikon.THFundWebApiApp
{
    [RoutePrefix("api")]
    public class THFundWebApiAppController : ApiController
    {

        private readonly ILogger _logger;
        private readonly IAppHits _appHits;

        public THFundWebApiAppController(ILogger logger, IAppHits appHits)
        {
            // Note: ILogger and IAppHits are injected by App Engine
            _logger = logger;
            _appHits = appHits;
        }

        #region GetAllTHLTFFunds
        [AcceptVerbs("GET", "POST")]
        [Route("ltf")]           // URL is http://localhost:10030/Apps/THFundWebApiApp/api/ltf
        public response GetAllTHLTFFunds()
        {
            _appHits.AppHitsFeatureHit("GetAllTHLTFFunds API");
            _logger.LogInfo("Executing GetAllTHLTFFunds for UUID: '{0}'", GetCurrentUserUUID());
            using (var thFundSvc = new THFundServiceClient(RouterBindings.Local, RouterAddresses.Local.RequestReply))
            {
                return thFundSvc.GetAllTHLTFFunds();
            }
        }
        #endregion

        #region GetTHLTFFundByID
        [AcceptVerbs("GET", "POST")]
        [Route("ltf/{*fundID}")] // URL is http://localhost:10030/Apps/THFundWebApiApp/api/ltf/<fundID>
        public response GetTHLTFFundByID(string fundID)
        {
            _appHits.AppHitsFeatureHit("GetTHLTFFundByID API");
            _logger.LogInfo("Executing GetTHLTFFundByID with param: '{0}' for UUID: '{1}'", fundID, GetCurrentUserUUID());
            using (var thFundSvc = new THFundServiceClient(RouterBindings.Local, RouterAddresses.Local.RequestReply))
            {
                var svcResponse = thFundSvc.GetTHLTFFundByID(fundID);
                if (!string.IsNullOrEmpty(svcResponse.error))
                {
                    _logger.LogError("THFundService returned error: {0}", svcResponse.error);
                    throw CreateErrorResponse(HttpStatusCode.InternalServerError, svcResponse.error);
                }
                return svcResponse;
            }
        }
        #endregion

        #region GetCurrentUserUUID
        [HttpGet]
        [Route("uuid")]          // URL is http://localhost:10030/Apps/THFundWebApiApp/api/uuid
        public string GetCurrentUserUUID()
        {
            return Request.Headers.GetValues("reutersuuid").FirstOrDefault();
        }
        #endregion

        // **** For intermediate / professional C# developer ****
        // 
        // Prerequisite steps:
        // 1) Start local App Engine
        // 2) Kill "UserInfoService.exe"
        // 3) Execute C:\EAE\bin\UserInfo\UserInfoService.exe
        //
        // Requirements:
        // Please develop a new REST web service API for retrieving user information from UserInfoService.
        // The API should receive UUID parameter from URI and pass it to UserInfoService via App Engine
        // routing functionality. Then, the API should create DTO.UserInfo instance by copy data from
        // UserInfo.UserDetails which is returned from UserInfoService. Finally, the API should responds
        // the DTO.UserInfo in JSON or XML format to the web service caller.
        // Please consider adding log messages and recording AppHits when the API is called.
        //
        // Hints:
        // 1) A valid UUID for testing is "PAXTRA78597".
        // 2) http://localhost:8989/UserInfoService/help contains a list of all available UserInfoService APIs and
        //    example request and response messages.
        // 3) The recommended UserInfoService API for this task is "GetUserInfoReq".
        // 4) UserInfo.UserDetails possible fields/attributes are:
        //    UUID*
        //    UserId
        //    ContactTitle*
        //    FirstName*
        //    LastName*
        //    FullName
        //    LocalFirstName
        //    LocalLastName
        //    PreferredLanguage
        //    EmailAddress*
        //    TelephoneNumber
        //    JobRole
        //    JobRoleCode
        //    LegalConsentFlag
        //    LegalConsentFlagSetTime
        //    GeographicalFocus
        //    GeographicalFocusCode
        //    LocationAccountId
        //    ParentAccountId
        //    LastUpdateAAA
        //    LastUpdateGCAP
        //    ReutersMessagingId
        //    LocalDACSId
        //    AccountDomain
        //    AccountName
        //    AccountType
        //    Address
        //    City
        //    Country
        //    HomeSite
        //    SubscriberAccount
        //    FtlFlag
        //    FtlCompleteDate
        //    OrganisationType
        //    Site
        //    SessionQuota
        //    PasswordSavedAllowed
        //    FailOverDataCenter
        //
        // 5) DTO.UserInfo instance can be easily created by executing DTO.UserInfo.CreateUserInfoFromUserDetails
        //    static method and passing UserInfo.UserDetails as the method parameter.
        // 6) If UUID attribute in UserInfo.UserDetails in a response is null, this means
        //    UserInfoService could not find any information for the input UUID.
        // 7) If UUID could not be found, please consider throwing HttpResponseException with
        //    HttpStatusCode.NotFound and an appropriate error message.

        //[AcceptVerbs("GET", "POST")]
        //[Route("userInfo/{*Uuid}")] // URL is http://localhost:10030/Apps/THFundWebApiApp/api/userInfo/<Uuid>
        //public DTO.UserInfo GetUserInfo(string uuid)
        //{
        //    _appHits.AppHitsFeatureHit("Get userinfo by UserID");
        //    _logger.LogInfo("Get userinfo for uuid={0}", uuid);
        //    using (var userInfoSvc = new UserInfoServiceClient(RouterBindings.Local, RouterAddresses.Local.RequestReply))
        //    {
        //        var UserInfoReq = new UserInfoReq();
        //        UserInfoReq.uuid = uuid;
        //        var getResponse= userInfoSvc.GetUserInfoReq(UserInfoReq);
        //        if (getResponse.Equals(null)) 
        //        {
        //            _logger.LogError("THFundService returned error");
        //            return null;
        //        }
        //        var userInfo = DTO.UserInfo.CreateUserInfoFromUserDetails(getResponse.UserDetails);
        //        return userInfo;
        //    }
        //}




        #region CreateErrorResponse
        private HttpResponseException CreateErrorResponse(HttpStatusCode statusCode, string message, params object[] args)
        {
            return new HttpResponseException(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(string.Format(message, args))
            });
        }
        #endregion

    }

}
