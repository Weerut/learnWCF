using System;
using System.Net;
using System.ServiceModel.Web;
using WeerutTestWCFService.DAO;
using WeerutTestWCFService.DTO;
using TR.AppServer.Common.Interfaces;

namespace WeerutTestWCFService
{
    public class THFundService : ITHFundService
    {

        #region Logger and DAO instance
        private static readonly ILogger Logger = TR.AppServer.Logging.Logger.Default;

        private readonly IFundDAO dao = new THLTFFundDAO();
        #endregion

        #region GetTHLTFFundByID
        public Response GetTHLTFFundByID(string fundID)
        {
            Logger.LogInfo("Enter GetTHLTFFundByID with Fund ID: {0}", fundID);
            var fund = dao.GetFundByID(fundID);
            if (fund == null)
            {
                Logger.LogWarn("Fund ID '{0}' could not be not found!", fundID);
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return Response.CreateResponse(
                    new ArgumentException(string.Format("Fund '{0}' could not be not found!", fundID)));
            }
            return Response.CreateResponse(fund);
        }
        #endregion

        // ***** Required *****
        // Add API for retrieving all LTF funds
        // - Method name "GetAllTHLTFFunds" *** A MUST ***
        // - Return type is "THFundService.DTO.Response" which includes a list of LTF funds
        // - Support only "GET" HTTP method
        // - URI is http://localhost:8989/THFundService/THLTF
        public Response GetAllTHLTFFunds()
        {
            Logger.LogInfo("Enter GetAllTHLTFFunds");
            var funds = dao.GetAllFunds();
            if (funds == null)
            {
                Logger.LogWarn("GetAllTHLTFFunds could not be not found!");
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return Response.CreateResponse(
                    new ArgumentException(string.Format("GetAllTHLTFFunds could not be not found!")));
            }
            return Response.CreateResponse(funds);
        }

        #region AddTHLTFFund
        public Response AddTHLTFFund(Request request)
        {
            Logger.LogInfo("Enter AddTHLTFFund with info:{0}{1}", Environment.NewLine, request);
            try
            {
                dao.AddFund(request.Fund);
                return Response.CreateResponse("Success");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error processing AddTHLTFFund: {0}", ex);
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return Response.CreateResponse(ex);
            }
        }
        #endregion

        // ***** For intermediate / professional C# developer only *****
        // Add API for updating single entry LTF fund by fund ID
        // - Method name "UpdateTHLTFFund"
        // - Return type is "THFundService.DTO.Response" which includes "Success" message if succeeded;
        //   otherwise, includes error message from thrown exception
        // - Support only "PUT" HTTP method
        // - URI is http://localhost:8989/THFundService/THLTF/<fundID>
        public Response UpdateTHLTFFund(string fundID, Request request)
        {
            Logger.LogInfo("Enter AddTHLTFFund with info:{0}{1}{0}{2}", Environment.NewLine, fundID, request);
            try
            {
                var result = dao.UpdateFund(fundID, request.Fund);
                if (result)
                {
                    return Response.CreateResponse("Success");
                }
                else
                {
                    Logger.LogError("Error processing UpdateTHLTFFund");
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                    return Response.CreateResponse("Fail");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error processing AddTHLTFFund: {0}", ex);
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return Response.CreateResponse(ex);
            }
        }

        #region DeleteTHLTFFundByID
        public Response DeleteTHLTFFundByID(string fundID)
        {
            Logger.LogInfo("Enter DeleteTHLTFFundByID with Fund ID: {0}", fundID);
            var success = dao.DeleteFund(fundID);
            if (!success)
            {
                Logger.LogWarn("Fund ID '{0}' could not be not found!", fundID);
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return Response.CreateResponse(
                        new ArgumentException(string.Format("Fund '{0}' could not be not found!", fundID)));
            }
            return Response.CreateResponse("Success");
        }
        #endregion

    }
}
