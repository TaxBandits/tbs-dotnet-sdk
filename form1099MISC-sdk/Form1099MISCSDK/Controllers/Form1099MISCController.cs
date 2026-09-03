using Form1099MISCSDK.Models.Business;
using Form1099MISCSDK.Models.Form1099MISCCreate;
using Form1099MISCSDK.Models.Form1099MISCDelete;
using Form1099MISCSDK.Models.Form1099MISCGet;
using Form1099MISCSDK.Models.Form1099MISCList;
using Form1099MISCSDK.Models.Form1099MISCRequestDraftPdfUrl;
using Form1099MISCSDK.Models.Form1099MISCRequestPdfUrls;
using Form1099MISCSDK.Models.Form1099MISCStatus;
using Form1099MISCSDK.Models.Form1099MISCTransmit;
using Form1099MISCSDK.Models.Form1099MISCUpdate;
using Form1099MISCSDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Form1099MISCSDK.Controllers
{
    public class Form1099MISCController : Controller
    {
        #region CreateMISC View
        public ActionResult CreateMISCView(Guid businessId, string businessName, string tin)
        {
            if (businessId != Guid.Empty && !string.IsNullOrWhiteSpace(businessName) && !string.IsNullOrWhiteSpace(tin))
            {
                Form1099MiscCreateRequest createRequest = new Form1099MiscCreateRequest { ReturnHeader = new APIReturnHeader { Business = new Business { BusinessId = businessId, BusinessNm = businessName, EINorSSN = tin } } };
                return View(createRequest);
            }
            return View();
        }
        #endregion

        #region Form 1099MISC Create 
        [HttpPost]
        public ActionResult CreateMISC(Form1099MiscCreateRequest createRequest)
        {
            var miscCreateResponseJson = string.Empty;
            var miscCreateResponse = new Form1099MiscCreateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for MISC Create
                    string requestUri = Constants.CREATE_MISC;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099MiscCreateResponse>().Result;
                        if (createResponse != null)
                        {
                            miscCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscCreateResponse object
                            miscCreateResponse = JsonConvert.DeserializeObject<Form1099MiscCreateResponse>(miscCreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099MiscCreateResponse>().Result;
                        miscCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscCreateResponse object
                        miscCreateResponse = JsonConvert.DeserializeObject<Form1099MiscCreateResponse>(miscCreateResponseJson);
                    }
                }
            }

            return PartialView("_MISCCreateResponse", miscCreateResponse);
        }
        #endregion

        #region Form 1099MISC List
        [HttpGet]
        public ActionResult GetMISCList(Guid businessId, string businessName, string tin)
        {
            var miscListRepsone = new Form1099MiscResponse();
            var miscListResponseJSON = string.Empty;
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
                    //API URL to Get MISC List Return
                    string requestUri = Constants.List_MISC_URL + "?BusinessId=" + businessId + "&Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099MiscResponse>().Result;
                        if (listResponse != null)
                        {
                            miscListResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            miscListRepsone = JsonConvert.DeserializeObject<Form1099MiscResponse>(miscListResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099MiscResponse>().Result;
                        miscListResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        miscListRepsone = JsonConvert.DeserializeObject<Form1099MiscResponse>(miscListResponseJSON);
                        var businessDetails = new Form1099MiscListResponse();
                        miscListRepsone.Form1099Records = new List<Form1099MiscListResponse>();
                        businessDetails.BusinessId = businessId;
                        businessDetails.BusinessNm = businessName;
                        businessDetails.EINorSSN = tin;
                        miscListRepsone.Form1099Records.Add(businessDetails);
                    }
                }
            }

            return View(miscListRepsone.Form1099Records);
        }
        #endregion

        #region Get MISCDetail By Submissionid and recordid
        [HttpGet]
        public IActionResult GetMISCDetail(Guid submissionId, Guid recordId)
        {
            var miscGetResponse = new Form1099MiscRecordResponse();
            var states = new Form1099StateDetail();
            var miscGetResponseJSON = string.Empty;
            var miscDetails = new Form1099MiscCreateRequest();
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
                    //API URL to Get MISC Return using SubmissionId And RecordId
                    string requestUri = Constants.GET_MISC_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099MiscRecordResponse>().Result;
                        if (getResponse != null)
                        {
                            miscGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                            miscGetResponse = JsonConvert.DeserializeObject<Form1099MiscRecordResponse>(miscGetResponseJSON);
                            if (miscDetails != null)
                            {
                                miscDetails.ReturnHeader = miscGetResponse.Form1099Records.ReturnHeader;
                                miscDetails.SubmissionManiFest = miscGetResponse.Form1099Records.SubmissionManifest;
                                miscDetails.ReturnData = miscGetResponse.Form1099Records.ReturnData;
                                if (miscDetails.ReturnData.Any(x => x.MISCFormData.States != null && x.MISCFormData.States.Count == 1))
                                {
                                    miscDetails.ReturnData.ForEach(x => x.MISCFormData.States.Add(states));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099MiscRecordResponse>().Result;
                        miscGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                        miscGetResponse = JsonConvert.DeserializeObject<Form1099MiscRecordResponse>(miscGetResponseJSON);
                    }
                }
            }

            return View("CreateMISCView", miscDetails);
        }
        #endregion

        #region MISC Update 
        [HttpPost]
        public ActionResult UpdateMISC(Form1099MiscUpdateRequest updateMISC)
        {
            var miscUpdateResponseJson = string.Empty;
            var miscUpdateResponse = new Form1099MiscUpdateResponse();
            string apiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For MISC Update
                    string requestUri = Constants.UPDATE_MISC_URL;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Put Response from API
                    var apiResponse = apiClient.PutAsJsonAsync(requestUri, updateMISC).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var updateResponse = apiResponse.Content.ReadAsAsync<Form1099MiscUpdateResponse>().Result;
                        if (updateResponse != null)
                        {
                            miscUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscUpdateResponse object
                            miscUpdateResponse = JsonConvert.DeserializeObject<Form1099MiscUpdateResponse>(miscUpdateResponseJson);
                        }
                    }
                    else
                    {
                        var updateResponse = apiResponse.Content.ReadAsAsync<Form1099MiscUpdateResponse>().Result;
                        miscUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscUpdateResponse object
                        miscUpdateResponse = JsonConvert.DeserializeObject<Form1099MiscUpdateResponse>(miscUpdateResponseJson);
                    }
                }
            }

            return PartialView("_MISCUpdateResponse", miscUpdateResponse);
        }
        #endregion

        #region Get MISC Status 
        [HttpGet]
        public IActionResult GetMISCStatus(string submissionId, string recordId)
        {
            var miscStatusResponse = new Form1099MISCStatusResponse();
            var miscStatusResponseJSON = string.Empty;
            //Get  token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var jwt = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Status using SubmissionId and RecordId 
                    string requestUrl = Constants.STATUS_MISC_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    //Get URLs from App.Config
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, jwt);
                    //Get Status Response
                    var apiResponse = apiClient.GetAsync(requestUrl).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Form1099MISCStatusResponse>().Result;
                        miscStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to miscStatusResponse object
                        miscStatusResponse = JsonConvert.DeserializeObject<Form1099MISCStatusResponse>(miscStatusResponseJSON);
                    }
                    else
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        miscStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to miscStatusResponse object
                        miscStatusResponse = JsonConvert.DeserializeObject<Form1099MISCStatusResponse>(miscStatusResponseJSON);
                    }
                }
            }

            return PartialView("_MISCStatusResponse", miscStatusResponse);
        }
        #endregion

        #region Form 1099MISC ValidateForm 
        [HttpPost]
        public ActionResult ValidateForm(Form1099MiscCreateRequest createRequest)
        {
            var miscValidateFormResponseJson = string.Empty;
            var miscValidateFormResponse = new ValidateFormResponse1099MISC();
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
                    string requestUri = Constants.VALIDATEFORM_MISC_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiValidateFormResponse = apiResponse.Content.ReadAsAsync<ValidateFormResponse1099MISC>().Result;
                        if (apiValidateFormResponse != null)
                        {
                            miscValidateFormResponseJson = JsonConvert.SerializeObject(apiValidateFormResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscValidateFormResponse object
                            miscValidateFormResponse = JsonConvert.DeserializeObject<ValidateFormResponse1099MISC>(miscValidateFormResponseJson);
                        }
                    }
                    else
                    {
                        var apiValidateFormResponse = apiResponse.Content.ReadAsAsync<ValidateFormResponse1099MISC>().Result;
                        miscValidateFormResponseJson = JsonConvert.SerializeObject(apiValidateFormResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscValidateFormResponse object
                        miscValidateFormResponse = JsonConvert.DeserializeObject<ValidateFormResponse1099MISC>(miscValidateFormResponseJson);
                    }
                }
            }

            return PartialView("_MISCValidateFormResponse", miscValidateFormResponse);
        }
        #endregion

        #region Form 1099MISC Delete 
        [HttpGet]
        public ActionResult Delete(string submissionId, string recordId)
        {
            var miscDeleteResponseJson = string.Empty;
            var miscDeleteResponse = new Form1099MiscDeleteResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for MISC Delete
                    string requestUri = Constants.DELETE_MISC_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.DeleteAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099MiscDeleteResponse>().Result;
                        miscDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to miscDeleteResponse object
                        miscDeleteResponse = JsonConvert.DeserializeObject<Form1099MiscDeleteResponse>(miscDeleteResponseJson);
                    }
                    else
                    {
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099MiscDeleteResponse>().Result;
                        miscDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscDeleteResponse object
                        miscDeleteResponse = JsonConvert.DeserializeObject<Form1099MiscDeleteResponse>
                        (miscDeleteResponseJson);
                    }
                }
            }

            return PartialView("_MISCDeleteResponse", miscDeleteResponse);
        }
        #endregion

        #region Form 1099MISC Transmit 
        [HttpGet]
        public ActionResult Transmit(Guid submissionId, Guid recordId)
        {
            var miscTransmitResponseJson = string.Empty;
            var transmitRequest = new TransmitForm1099MiscRequest();
            var miscTransmitResponse = new TransmitForm1099Response();
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
                    //API URL to MISC Transmit
                    string requestUri = Constants.TRANSMIT_MISC_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, transmitRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<TransmitForm1099Response>().Result;
                        if (transmitResponse != null)
                        {
                            miscTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscTransmitResponse object
                            miscTransmitResponse = JsonConvert.DeserializeObject<TransmitForm1099Response>(miscTransmitResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<TransmitForm1099Response>().Result;
                        miscTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscTransmitResponse object
                        miscTransmitResponse = JsonConvert.DeserializeObject<TransmitForm1099Response>(miscTransmitResponseJson);
                    }
                }
            }

            return PartialView("_MISCTransmitResponse", miscTransmitResponse);
        }
        #endregion

        #region Form 1099Misc RequestDraftPdfUrl 
        [HttpGet]
        public ActionResult RequestDraftPdfUrl(Guid recordId)
        {
            var miscDraftPdfResponseJson = string.Empty;
            var draftPdfUrlRequest = new RequestDraftPdfUrlRequest();
            var miscDraftPdfUrlResponse = new RequestDraftPdfUrlResponse();
            draftPdfUrlRequest.RecordId = recordId;
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
                    string requestUri = Constants.MISC_DRAFT_URL;
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
                            miscDraftPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscDraftPdfUrlResponse object
                            miscDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(miscDraftPdfResponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestDraftPdfUrlResponse>().Result;
                        miscDraftPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscDraftPdfUrlResponse object
                        miscDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(miscDraftPdfResponseJson);
                    }
                }
            }

            return PartialView("_MISCDraftPdfResponse", miscDraftPdfUrlResponse);
        }
        #endregion

        #region Form10999MISCRequestDraftPdfUrl
        [HttpGet]
        public async Task<ActionResult> Form1099MiscRequestDraftPdfUrl(string DraftPdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(DraftPdfUrl))
            {
                string fileExtension = DraftPdfUrl.Substring(DraftPdfUrl.LastIndexOf('.') + 1, DraftPdfUrl.Length - (DraftPdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099MiscPdfS3ByFileName(DraftPdfUrl);
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
        public ActionResult RequestPdfUrl(Guid SubmissionId, Guid recordId)
        {
            var miscRequestPdfResponseJson = string.Empty;
            var pdfUrlRequest = new RequestPdfURLRequest();
            var miscPdfUrlResponse = new Form1099MISCRequestPdfUrlsResponse();
            var recordIds = new RequestRecordIds();
            pdfUrlRequest.RecordIds = new List<RequestRecordIds>();
            recordIds.RecordId = recordId;
            pdfUrlRequest.RecordIds.Add(recordIds);
            pdfUrlRequest.SubmissionId = SubmissionId;

            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For Misc RequestPdfUrl
                    string requestUri = Constants.MISC_RequestPdf_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, pdfUrlRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<Form1099MISCRequestPdfUrlsResponse>().Result;
                        if (draftPdfUrlResponse != null)
                        {
                            miscRequestPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to miscPdfUrlResponse object
                            miscPdfUrlResponse = JsonConvert.DeserializeObject<Form1099MISCRequestPdfUrlsResponse>(miscRequestPdfResponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<Form1099MISCRequestPdfUrlsResponse>().Result;
                        miscRequestPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to miscPdfUrlResponse object
                        miscPdfUrlResponse = JsonConvert.DeserializeObject<Form1099MISCRequestPdfUrlsResponse>(miscRequestPdfResponseJson);
                    }
                }
            }

            return PartialView("_MISCRequestPdfResponse", miscPdfUrlResponse);
        }
        #endregion

        #region RequestPdfUrl View
        public ActionResult _ViewRequestPdfUrl(string PdfUrl)
        {
            TempData["PdfUrl"] = PdfUrl;
            return PartialView();
        }
        #endregion

        #region Form10999Misc Request PdfUrl
        public async Task<ActionResult> RequestPdfUrls(string PdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(PdfUrl))
            {
                string fileExtension = PdfUrl.Substring(PdfUrl.LastIndexOf('.') + 1, PdfUrl.Length - (PdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099MiscPdfS3ByFileName(PdfUrl);
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