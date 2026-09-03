using FormW2SDK.Models.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using static FormW2SDK.Models.Base.EntityBase;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using FormW2SDK.Models.Utilities;
using Constants = FormW2SDK.Models.Utilities.Constants;

namespace FormW2SDK.Controllers
{
    public class BusinessController : Controller
    {
        #region Business View Page
        [HttpGet]
        public IActionResult CreateBusiness()
        {
            return View();
        }
        #endregion

        #region Business Create 
        [HttpPost]
        public ActionResult CreateBusiness(BusinessCreateRequest createBusiness)
        {
            var responseJson = string.Empty;
            var businessCreateResponse = new BusinessCreateResponse();
            // Generate JSON for Business
            var requestJson = JsonConvert.SerializeObject(createBusiness, Formatting.Indented);
            //Get Access token from OAuth API response 
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = accessToken.GetGeneratedAccessToken();
            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Business Create
                    string requestUri = Constants.CREATE_BUSINESS;
                    //Get APIUrl From Appsetting 
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Create Response from API
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createBusiness).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response 
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <BusinessCreateResponse>
                        ().Result;
                        if (createResponse != null)
                        {
                            responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to BusinessCreateResponse object
                            businessCreateResponse = JsonConvert.DeserializeObject
                            <BusinessCreateResponse>
                            (responseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to BusinessCreateResponse object
                        businessCreateResponse = JsonConvert.DeserializeObject
                        <BusinessCreateResponse>
                        (responseJson);
                    }
                }
            }
            return PartialView("_BusinessCreateResponse", businessCreateResponse);
        }
        #endregion

        #region Get Business List
        [HttpGet]
        public ActionResult GetBusinessList()
        {
            var businessListResponse = new BusinessListResponse();
            var business = new List
            <Business>
            ();
            var getResponseJSON = string.Empty;
            //Get URLs from App.Config
            string apiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var generatedAccessToken = accessToken.GetGeneratedAccessToken();
            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Business List Return
                    string requestUri = Constants.BUSINESS_LIST + "?Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync
                        <BusinessListResponse>
                        ().Result;
                        if (listResponse != null)
                        {
                            getResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            businessListResponse = JsonConvert.DeserializeObject
                            <BusinessListResponse>
                            (getResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        getResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        businessListResponse = JsonConvert.DeserializeObject
                        <BusinessListResponse>
                        (getResponseJSON);
                    }
                }
            }
            return View("GetBusinessList", businessListResponse);
        }
        #endregion

        #region  Business Members Type  based on Business Type
        [HttpGet]
        public ActionResult BusinessMembersType(string SelectedBusinessTypeVal)
        {
            var businessType = SelectedBusinessTypeVal;
            if (!string.IsNullOrWhiteSpace(businessType))
            {
                var businessMembersList = new List
                <SelectListItem>
                ();
                #region DropDownValues for Business MemberType
                if (businessType == BusinessType.ESTE.ToString())
                {
                    //Assign Values from EstateBusinessMembers Enum to te List
                    businessMembersList = Enum.GetValues(typeof(EstateBusinessMembers)).Cast
                    <EstateBusinessMembers>
                    ().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.PART.ToString())
                {
                    //Assign Values from PartnershipBusinessMembers Enum to te List
                    businessMembersList = Enum.GetValues(typeof(PartnershipBusinessMembers)).Cast
                    <PartnershipBusinessMembers>
                    ().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.CORP.ToString())
                {
                    //Assign Values from CorporationBusinessMembers Enum to te List
                    businessMembersList = Enum.GetValues(typeof(CorporationBusinessMembers)).Cast
                    <CorporationBusinessMembers>
                    ().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.EORG.ToString())
                {
                    //Assign Values from ExemptOrganizationBusinessMembers Enum to te List
                    businessMembersList = Enum.GetValues(typeof(ExemptOrganizationBusinessMembers)).Cast
                    <ExemptOrganizationBusinessMembers>
                    ().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                else if (businessType == BusinessType.SPRO.ToString())
                {
                    //Assign Values from SoleProprietorshipBusinessMembers Enum to te List
                    businessMembersList = Enum.GetValues(typeof(SoleProprietorshipBusinessMembers)).Cast
                    <SoleProprietorshipBusinessMembers>
                    ().Select(x => new SelectListItem { Text = Utility.GetEnumDisplayName(x), Value = (x).ToString() }).ToList();
                }
                return new JsonResult(new { data = businessMembersList });
                #endregion
            }
            return new JsonResult(false);
        }
        #endregion
    }
}
