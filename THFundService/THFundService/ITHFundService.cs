using System.ServiceModel;
using System.ServiceModel.Web;
using WeerutTestWCFService.DTO;

namespace WeerutTestWCFService
{

    /// <summary>
    /// [Using HTTP Methods for RESTful Services]
    /// 
    /// HTTP Verb       CRUD        Possible HTTP Status
    /// ==================================================
    /// GET             Read        200 (OK), 404 (Not Found), if ID not found or invalid.
    /// POST            Create      201 (Created), 409 (Conflict) if resource already exists.
    /// PUT             Update      200 (OK), 404 (Not Found), if ID not found or invalid.
    /// DELETE          Delete      200 (OK). 404 (Not Found), if ID not found or invalid.
    /// 
    /// Reference: http://www.restapitutorial.com/lessons/httpmethods.html
    /// </summary>
    [ServiceContract]
    public interface ITHFundService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "THLTF/{*fundID}")]
        Response GetTHLTFFundByID(string fundID);

        // ***** Required *****
        // Add API for retrieving all LTF funds
        // - Method name "GetAllTHLTFFunds" *** A MUST ***
        // - Return type is "THFundService.DTO.Response" which includes a list of LTF funds
        // - Support only "GET" HTTP method
        // - URI is http://localhost:8989/THFundService/THLTF
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "THLTF")]
        Response GetAllTHLTFFunds();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "THLTF")]
        Response AddTHLTFFund(Request request);

        // ***** For intermediate / professional C# developer only *****
        // Add API for updating single entry LTF fund by fund ID
        // - Method name "UpdateTHLTFFund"
        // - Return type is "THFundService.DTO.Response" which includes "Success" message if succeeded;
        //   otherwise, includes error message from thrown exception
        // - Support only "PUT" HTTP method
        // - URI is http://localhost:8989/THFundService/THLTF/<fundID>
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "THLTF/{*fundID}")]
        Response UpdateTHLTFFund(string fundID, Request request);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "THLTF/{*fundID}")]
        Response DeleteTHLTFFundByID(string fundID);

    }
}
