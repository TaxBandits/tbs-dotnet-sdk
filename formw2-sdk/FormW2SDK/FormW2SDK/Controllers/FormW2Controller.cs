using FormW2SDK.Models.Business;
using FormW2SDK.Models.FormW2Create;
using FormW2SDK.Models.FormW2Delete;
using FormW2SDK.Models.FormW2Get;
using FormW2SDK.Models.FormW2List;
using FormW2SDK.Models.FormW2RequestDraftPdfUrl;
using FormW2SDK.Models.FormW2RequestPdfUrls;
using FormW2SDK.Models.FormW2Status;
using FormW2SDK.Models.FormW2Transmit;
using FormW2SDK.Models.FormW2Update;
using FormW2SDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FormW2SDK.Controllers
{
    public class FormW2Controller : Controller
    {
        #region CreateForm W2 View
        public ActionResult CreateFormW2(Guid businessId, string businessName, string tin)
        {
            if (businessId != Guid.Empty && !string.IsNullOrWhiteSpace(businessName) && !string.IsNullOrWhiteSpace(tin))
            {
                FormW2CreateRequest createRequest = new FormW2CreateRequest { ReturnHeader = new APIReturnHeader { Business = new Business { BusinessId = businessId, BusinessNm = businessName, EINorSSN = tin } } };
                return View(createRequest);
            }
            return View();
        }
        #endregion

        #region Form W2 Create 
        [HttpPost]
        public ActionResult CreateFormW2(FormW2CreateRequest createRequest)
        {
            var formW2CreateResponseJson = string.Empty;
            var formW2CreateResponse = new FormW2CreateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            #region StaeRecon Assiging Null value
            //If states does not contain state Alabama then initialize Al value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "AL")))
            {
                createRequest.StateReconData.AL = null;
            }

            //If states does not contain state WestVirginia then initialize WV value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "WV")))
            {
                createRequest.StateReconData.WV = null;
            }

            //If states does not contain state Arizona then initialize AZ value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "AZ")))
            {
                createRequest.StateReconData.AZ = null;
            }

            //If states does not contain state Connecticut then initialize CT value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "CT")))
            {
                createRequest.StateReconData.CT = null;
            }

            //If states does not contain state Idaho then initialize ID value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "ID")))
            {
                createRequest.StateReconData.ID = null;
            }

            //If states does not contain state Indiana then initialize IN value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "IN")))
            {
                createRequest.StateReconData.IN = null;
            }

            //If states does not contain state Kansas then initialize KS value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "KS")))
            {
                createRequest.StateReconData.KS = null;
            }
            //If states does not contain state Louisiana then initialize LA value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "LA")))
            {
                createRequest.StateReconData.LA = null;
            }

            //If states does not contain state Maryland then initialize MD value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "MD")))
            {
                createRequest.StateReconData.MD = null;
            }
            //If states does not contain state NewJersy then initialize NJ value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "NJ")))
            {
                createRequest.StateReconData.NJ = null;
            }
            //If states does not contain state Pennsylvania then initialize PA value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "PA")))
            {
                createRequest.StateReconData.PA = null;
            }
            //If states does not contain state Vermont then initialize VT value as null
            if (!createRequest.ReturnData.Any(x => x.W2FormData.States.Any(x => x.B15StateCd == "VT")))
            {
                createRequest.StateReconData.VT = null;
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for K Create
                    string requestUri = Constants.CREATE_FORMW2_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <FormW2CreateResponse>
                        ().Result;
                        if (createResponse != null)
                        {
                            formW2CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to formW2CreateResponse object
                            formW2CreateResponse = JsonConvert.DeserializeObject
                            <FormW2CreateResponse>
                            (formW2CreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <FormW2CreateResponse>
                        ().Result;
                        formW2CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to formW2CreateResponse object
                        formW2CreateResponse = JsonConvert.DeserializeObject
                        <FormW2CreateResponse>
                        (formW2CreateResponseJson);
                    }
                }
            }
            return PartialView("_FormW2CreateResponse", formW2CreateResponse);
        }
        #endregion

        #region Form W2 List
        [HttpGet]
        public ActionResult GetW2List(Guid businessId, string businessName, string tin)
        {
            var w2ListReturnResponse = new FormW2RecordsResponse();
            var w2GetReturnResponseJSON = string.Empty;
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
                    //API URL to Get w2 List Return
                    string requestUri = Constants.W2_LIST_URL + "?BusinessId=" + businessId + "&Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<FormW2RecordsResponse>().Result;
                        if (listResponse != null)
                        {
                            w2GetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            w2ListReturnResponse = JsonConvert.DeserializeObject<FormW2RecordsResponse>(w2GetReturnResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<FormW2RecordsResponse>().Result;
                        w2GetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        w2ListReturnResponse = JsonConvert.DeserializeObject<FormW2RecordsResponse>(w2GetReturnResponseJSON);
                        var businessDetails = new FormW2ListResponse();
                        w2ListReturnResponse.FormW2Records = new List<FormW2ListResponse>();
                        businessDetails.BusinessId = businessId;
                        businessDetails.BusinessNm = businessName;
                        businessDetails.EIN = tin;
                        w2ListReturnResponse.FormW2Records.Add(businessDetails);
                    }
                }
            }

            return View("GetW2List", w2ListReturnResponse.FormW2Records);
        }
        #endregion

        #region Get W2Detail By Submissionid and recordid
        [HttpGet]
        public IActionResult GetW2Detail(Guid submissionId, Guid recordId)
        {
            var w2GetResponse = new FormW2RecordResponse();
            var states = new FormW2StateDetails();
            var w2GetResponseJSON = string.Empty;
            var w2Details = new FormW2CreateRequest();
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
                    //API URL to Get w2 Return using SubmissionId And RecordId
                    string requestUri = Constants.GET_W2_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<FormW2RecordResponse>().Result;
                        if (getResponse != null)
                        {
                            w2GetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                            w2GetResponse = JsonConvert.DeserializeObject<FormW2RecordResponse>(w2GetResponseJSON);
                            if (w2GetResponse != null)
                            {
                                w2Details.ReturnHeader = w2GetResponse.FormW2Records.ReturnHeader;
                                w2Details.SubmissionManiFest = w2GetResponse.FormW2Records.SubmissionManifest;
                                w2Details.ReturnData = w2GetResponse.FormW2Records.ReturnData;
                                if (w2Details.ReturnData.Any(x => x.W2FormData.States == null))
                                {
                                    w2Details.ReturnData.ForEach(x => {
                                        x.W2FormData.States = new List<FormW2StateDetails>(); // Initialize States as a new list

                                        
                                    });
                                   

                                }
                                w2Details.StateReconData = w2GetResponse.FormW2Records.StateReconData;

                            }
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<FormW2RecordResponse>().Result;
                        w2GetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                        w2GetResponse = JsonConvert.DeserializeObject<FormW2RecordResponse>(w2GetResponseJSON);
                    }
                }
            }

            return View("CreateFormW2", w2Details);
        }
        #endregion

        #region W2 Update 
        [HttpPost]
        public ActionResult UpdateFormW2(FormW2UpdateRequest updateW2)
        {
            var w2UpdateResponseJson = string.Empty;
            var w2UpdateResponse = new FormW2UpdateResponse();
            string apiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = accessToken.GetGeneratedAccessToken();

         
            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For W2 Update
                    string requestUri = Constants.UPDATE_W2_URL;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Put Response from API
                    var apiResponse = apiClient.PutAsJsonAsync(requestUri, updateW2).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var updateResponse = apiResponse.Content.ReadAsAsync<FormW2UpdateResponse>().Result;
                        if (updateResponse != null)
                        {
                            w2UpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to w2updateResponse object
                            w2UpdateResponse = JsonConvert.DeserializeObject<FormW2UpdateResponse>(w2UpdateResponseJson);
                        }
                    }
                    else
                    {
                        var updateResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        w2UpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to w2updateResponse object
                        w2UpdateResponse = JsonConvert.DeserializeObject<FormW2UpdateResponse>(w2UpdateResponseJson);
                    }
                }
            }

            return PartialView("_W2UpdateResponse", w2UpdateResponse);
        }
        #endregion

        #region Get W2 Status 
        [HttpGet]
        public IActionResult GetStatus(string submissionId, string recordId)
        {
            var w2StatusResponse = new FormW2StatusResponse();
            var w2StatusResponseJSON = string.Empty;
            //Get  token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var jwt = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Status using SubmissionId and RecordId 
                    string requestUrl = Constants.W2_STATUS_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    //Get URLs from App.Config
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, jwt);
                    //Get Status Response
                    var apiResponse = apiClient.GetAsync(requestUrl).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<FormW2StatusResponse>().Result;
                        w2StatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to w2StatusResponse object
                        w2StatusResponse = JsonConvert.DeserializeObject<FormW2StatusResponse>(w2StatusResponseJSON);
                    }
                    else
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        w2StatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to w2StatusResponse object
                        w2StatusResponse = JsonConvert.DeserializeObject<FormW2StatusResponse>(w2StatusResponseJSON);
                    }
                }
            }

            return PartialView("_W2StatusResponse", w2StatusResponse);
        }
        #endregion

        #region Form W2 ValidateForm 
        [HttpPost]
        public ActionResult ValidateForm(FormW2CreateRequest createRequest)
        {
            var w2ValidateFormResponseJson = string.Empty;
            var w2ValidateFormResponse = new FormW2ValidateFormResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for ValidateForm Create
                    string requestUri = Constants.W2_VALIDATEFORM_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<FormW2ValidateFormResponse>().Result;
                        if (validateFormResponse != null)
                        {
                            w2ValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to w2ValidateFormResponse object
                            w2ValidateFormResponse = JsonConvert.DeserializeObject<FormW2ValidateFormResponse>(w2ValidateFormResponseJson);
                        }
                    }
                    else
                    {
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<FormW2ValidateFormResponse>().Result;
                        w2ValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to w2ValidateFormResponse object
                        w2ValidateFormResponse = JsonConvert.DeserializeObject<FormW2ValidateFormResponse>(w2ValidateFormResponseJson);
                    }
                }
            }

            return PartialView("_W2ValidateFormResponse", w2ValidateFormResponse);
        }
        #endregion

        #region Form W2 Delete 
        [HttpGet]
        public ActionResult Delete(string submissionId, string recordId)
        {
            var w2DeleteResponseJson = string.Empty;
            var W2DeleteResponse = new FormW2DeleteResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for W2 Delete
                    string requestUri = Constants.W2_DELETE_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.DeleteAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<FormW2DeleteResponse>().Result;
                        w2DeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to W2DeleteResponse object
                        W2DeleteResponse = JsonConvert.DeserializeObject<FormW2DeleteResponse>(w2DeleteResponseJson);
                    }
                    else
                    {
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<FormW2DeleteResponse>().Result;
                        w2DeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to W2DeleteResponse object
                        W2DeleteResponse = JsonConvert.DeserializeObject<FormW2DeleteResponse>(w2DeleteResponseJson);
                    }
                }
            }

            return PartialView("_W2DeleteResponse", W2DeleteResponse);
        }
        #endregion

        #region Form W2 Transmit 
        [HttpGet]
        public ActionResult Transmit(Guid? submissionId, Guid recordId)
        {
            var w2TransmitResponseJson = string.Empty;
            var transmitRequest = new FormW2TransmitRequest();
            var w2TransmitResponse = new FormW2TransmitResponse();
            transmitRequest.RecordIds = new List<Guid>();
            transmitRequest.SubmissionId = submissionId;
            transmitRequest.RecordIds.Add(recordId);
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to W2 Transmit
                    string requestUri = Constants.W2_TRANSMIT_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, transmitRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<FormW2TransmitResponse>().Result;
                        if (transmitResponse != null)
                        {
                            w2TransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to w2TransmitResponse object
                            w2TransmitResponse = JsonConvert.DeserializeObject<FormW2TransmitResponse>(w2TransmitResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<FormW2TransmitResponse>().Result;
                        w2TransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to w2TransmitResponse object
                        w2TransmitResponse = JsonConvert.DeserializeObject<FormW2TransmitResponse>(w2TransmitResponseJson);
                    }
                }
            }

            return PartialView("_W2TransmitResponse", w2TransmitResponse);
        }
        #endregion

        #region Form W2 RequestDraftPdfUrl 
        [HttpGet]
        public ActionResult RequestDraftPdfUrl(Guid? recordId)
        {
            var w2DraftPdfresponseJson = string.Empty;
            var draftPdfUrlRequest = new FromW2RequestDraftPdfUrl();
            draftPdfUrlRequest.RecordId = recordId;
            var w2DraftPdfUrlResponse = new RequestDraftPdfUrlResponse();     
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for RequestDraftPdfUrl
                    string requestUri = Constants.W2_DRAFTPDF_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, draftPdfUrlRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestDraftPdfUrlResponse>().Result;
                        if (draftPdfUrlResponse != null)
                        {
                            w2DraftPdfresponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to w2DraftPdfUrlResponse object
                            w2DraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(w2DraftPdfresponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestDraftPdfUrlResponse>().Result;
                        w2DraftPdfresponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to w2DraftPdfUrlResponse object
                        w2DraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(w2DraftPdfresponseJson);
                    }
                }
            }

            return PartialView("_W2DraftPdfResponse", w2DraftPdfUrlResponse);
        }
        #endregion

        #region FormW2RequestDraftPdfUrl
        public async Task<ActionResult> FormW2RequestDraftPdfUrl(string DraftPdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(DraftPdfUrl))
            {
                string fileExtension = DraftPdfUrl.Substring(DraftPdfUrl.LastIndexOf('.') + 1, DraftPdfUrl.Length - (DraftPdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetFormW2PdfS3ByFileName(DraftPdfUrl);
                string fileName = Path.GetFileName(DraftPdfUrl);
                if (file != null && file.Length > 0)
                {
                    Response.Clear();
                    Response.Headers.Add("Accept-Header", file.Length.ToString());
                    Response.Headers.Add("Content-Type", Utility.GetContentTypeByExtension(fileExtension.ToLower()));
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + DraftPdfUrl.Replace(" ", "").Replace(",", "") + "; size=" + file.Length.ToString());
                    Response.Headers.Add("content-length", file.Length.ToString());
                    await Response.BodyWriter.WriteAsync(file);
                    await Response.BodyWriter.FlushAsync(); // Flush the response body

                    return new EmptyResult();
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region RequestPdfUrl
        [HttpGet]
        public ActionResult RequestPdfUrl(Guid submissionId, Guid recordId)
        {
            var w2RequestPdfResponseJson = string.Empty;
            var pdfUrlRequest = new RequestPdfURLRequest();
            var w2PdfUrlResponse = new RequestPdfUrlsResponse();
            var recordIds = new RequestRecordIds();
            pdfUrlRequest.RecordIds = new List<RequestRecordIds>();

            recordIds.RecordId = recordId;
            pdfUrlRequest.RecordIds.Add(recordIds);
            pdfUrlRequest.SubmissionId = submissionId;

            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For W2 RequestPdfUrl
                    string requestUri = Constants.W2_REQUESTPDF_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, pdfUrlRequest).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var pdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestPdfUrlsResponse>().Result;
                        if (pdfUrlResponse != null)
                        {
                            w2RequestPdfResponseJson = JsonConvert.SerializeObject(pdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to w2PdfUrlResponse object
                            w2PdfUrlResponse = JsonConvert.DeserializeObject<RequestPdfUrlsResponse>(w2RequestPdfResponseJson);
                        }
                    }
                    else
                    {
                        var pdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestPdfUrlsResponse>().Result;
                        w2RequestPdfResponseJson = JsonConvert.SerializeObject(pdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to w2PdfUrlResponse object
                        w2PdfUrlResponse = JsonConvert.DeserializeObject<RequestPdfUrlsResponse>(w2RequestPdfResponseJson);
                    }
                }
            }

            return PartialView("_W2RequestPdfResponse", w2PdfUrlResponse);
        }
        #endregion

        #region RequestPdfUrl View
        public ActionResult _ViewRequestPdfUrl(string PdfUrl)
        {
            TempData["PdfUrl"] = PdfUrl;

            return PartialView();
        }
        #endregion

        #region FormW2 Request PdfUrl
        public async Task<ActionResult> RequestPdfUrls(string PdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(PdfUrl))
            {
                string fileExtension = PdfUrl.Substring(PdfUrl.LastIndexOf('.') + 1, PdfUrl.Length - (PdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetFormW2PdfS3ByFileName(PdfUrl);
                string fileName = Path.GetFileName(PdfUrl);

                if (file != null && file.Length > 0)
                {
                    Response.Clear();
                    Response.Headers.Add("Accept-Header", file.Length.ToString());
                    Response.Headers.Add("Content-Type", Utility.GetContentTypeByExtension(fileExtension.ToLower()));
                    Response.Headers.Add("Content-Disposition", "inline; filename=" + PdfUrl.Replace(" ", "").Replace(",", "") + "; size=" + file.Length.ToString());
                    Response.Headers.Add("content-length", file.Length.ToString());
                    await Response.BodyWriter.WriteAsync(file);
                    await Response.BodyWriter.FlushAsync(); // Flush the response body

                    return new EmptyResult();
                }
            }

            return new EmptyResult();
        }
        #endregion
    }
}
