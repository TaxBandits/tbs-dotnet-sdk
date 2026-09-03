using Form1099KSDK.Models.Business;
using Form1099KSDK.Models.Form1099KCreate;
using Form1099KSDK.Models.Form1099KDelete;
using Form1099KSDK.Models.Form1099KDraftPdfUrl;
using Form1099KSDK.Models.Form1099KGet;
using Form1099KSDK.Models.Form1099KList;
using Form1099KSDK.Models.Form1099KRequestPdfURL;
using Form1099KSDK.Models.Form1099KStatus;
using Form1099KSDK.Models.Form1099KTransmit;
using Form1099KSDK.Models.Form1099KUpdate;
using Form1099KSDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Form1099KSDK.Controllers
{
    public class Form1099KController : Controller
    {
        #region CreateForm 1099K View
        public ActionResult CreateForm1099K(Guid businessId, string businessName, string tin)
        {
            if (businessId != Guid.Empty && !string.IsNullOrWhiteSpace(businessName) && !string.IsNullOrWhiteSpace(tin))
            {
                Form1099KCreateRequest createRequest = new Form1099KCreateRequest { ReturnHeader = new APIReturnHeader { Business = new Business { BusinessId = businessId, BusinessNm = businessName, EINorSSN = tin } } };
                return View(createRequest);
            }
            return View();
        }
        #endregion

        #region Form 1099K Create 
        [HttpPost]
        public ActionResult CreateForm1099K(Form1099KCreateRequest createRequest)
        {
            var form1099KCreateResponseJson = string.Empty;
            var form1099KCreateResponse = new Form1099KCreateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for K Create
                    string requestUri = Constants.CREATE_1099K_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099KCreateResponse>().Result;
                        if (createResponse != null)
                        {
                            form1099KCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KCreateResponse object
                            form1099KCreateResponse = JsonConvert.DeserializeObject<Form1099KCreateResponse>(form1099KCreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099KCreateResponse>().Result;
                        form1099KCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KCreateResponse object
                        form1099KCreateResponse = JsonConvert.DeserializeObject<Form1099KCreateResponse>(form1099KCreateResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KCreateResponse", form1099KCreateResponse);
        }
        #endregion

        #region Form 1099K List
        [HttpGet]
        public ActionResult GetForm1099KList(Guid businessId, string businessName, string tin)
        {
            var form1099KListRepsone = new Form1099KListResponse();
            var form1099KListResponseJSON = string.Empty;
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
                    //API URL to Get K List Return
                    string requestUri = Constants.List_1099K_URL + "?BusinessId=" + businessId + "&Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099KListResponse>().Result;
                        if (listResponse != null)
                        {
                            form1099KListResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            form1099KListRepsone = JsonConvert.DeserializeObject<Form1099KListResponse>(form1099KListResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099KListResponse>().Result;
                        form1099KListResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        form1099KListRepsone = JsonConvert.DeserializeObject<Form1099KListResponse>(form1099KListResponseJSON);
                        var businessDetails = new Form1099KRecordsList();
                        form1099KListRepsone.Form1099Records = new List<Form1099KRecordsList>();
                        businessDetails.BusinessId = businessId;
                        businessDetails.BusinessNm = businessName;
                        businessDetails.EINorSSN = tin;
                        form1099KListRepsone.Form1099Records.Add(businessDetails);
                    }
                }
            }

            return View(form1099KListRepsone);
        }
        #endregion

        #region Get KDetail By Submissionid and recordid
        [HttpGet]
        public IActionResult GetForm1099KDetail(Guid submissionId, Guid recordId)
        {
            var form1099KGetResponse = new Form1099KGetResponse();
            var states = new Form1099KStateDetail();
            var form1099KGetResponseJSON = string.Empty;
            var form1099KDetails = new Form1099KCreateRequest();
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
                    //API URL to Get K Return using SubmissionId And RecordId
                    string requestUri = Constants.GET_1099K_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099KGetResponse>().Result;
                        if (getResponse != null)
                        {
                            form1099KGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                            form1099KGetResponse = JsonConvert.DeserializeObject<Form1099KGetResponse>(form1099KGetResponseJSON);
                            if (form1099KDetails != null)
                            {
                                form1099KDetails.ReturnHeader = form1099KGetResponse.Form1099Records.ReturnHeader;
                                form1099KDetails.SubmissionManiFest = form1099KGetResponse.Form1099Records.SubmissionManifest;
                                form1099KDetails.ReturnData = form1099KGetResponse.Form1099Records.ReturnData;
                                if (form1099KDetails.ReturnData.Any(x => x.KFormData.States != null && x.KFormData.States.Count == 1))
                                {
                                    form1099KDetails.ReturnData.ForEach(x => x.KFormData.States.Add(states));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099KGetResponse>().Result;
                        form1099KGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                        form1099KGetResponse = JsonConvert.DeserializeObject<Form1099KGetResponse>(form1099KGetResponseJSON);
                    }
                }
            }

            return View("CreateForm1099K", form1099KDetails);
        }
        #endregion

        #region Form 1099 K Update 
        [HttpPost]
        public ActionResult UpdateForm1099K(Form1099KUpdateRequest updateForm1099K)
        {
            var form1099KUpdateResponseJson = string.Empty;
            var form1099KUpdateResponse = new Form1099KUpdateResponse();
            string apiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For K Update
                    string requestUri = Constants.UPDATE_1099K_URL;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Put Response from API
                    var apiResponse = apiClient.PutAsJsonAsync(requestUri, updateForm1099K).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var updateResponse = apiResponse.Content.ReadAsAsync<Form1099KUpdateResponse>().Result;
                        if (updateResponse != null)
                        {
                            form1099KUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KUpdateResponse object
                            form1099KUpdateResponse = JsonConvert.DeserializeObject<Form1099KUpdateResponse>(form1099KUpdateResponseJson);
                        }
                    }
                    else
                    {
                        var updateResponse = apiResponse.Content.ReadAsAsync<Form1099KUpdateResponse>().Result;
                        form1099KUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KUpdateResponse object
                        form1099KUpdateResponse = JsonConvert.DeserializeObject<Form1099KUpdateResponse>(form1099KUpdateResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KUpdateResponse", form1099KUpdateResponse);
        }
        #endregion

        #region Get Form1099 K Status 
        [HttpGet]
        public IActionResult GetForm1099KStatus(string submissionId, string recordId)
        {
            var form1099KStatusResponse = new Form1099KStatusResponse();
            var form1099KStatusResponseJSON = string.Empty;
            //Get  token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var jwt = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Status using SubmissionId and RecordId 
                    string requestUrl = Constants.STATUS_1099K_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    //Get URLs from App.Config
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, jwt);
                    //Get Status Response
                    var apiResponse = apiClient.GetAsync(requestUrl).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Form1099KStatusResponse>().Result;
                        form1099KStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to form1099KStatusResponse object
                        form1099KStatusResponse = JsonConvert.DeserializeObject<Form1099KStatusResponse>(form1099KStatusResponseJSON);
                    }
                    else
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        form1099KStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to form1099KStatusResponse object
                        form1099KStatusResponse = JsonConvert.DeserializeObject<Form1099KStatusResponse>(form1099KStatusResponseJSON);
                    }
                }
            }

            return PartialView("_Form1099KStatusResponse", form1099KStatusResponse);
        }
        #endregion

        #region Form 1099K ValidateForm 
        [HttpPost]
        public ActionResult ValidateForm(Form1099KCreateRequest createRequest)
        {
            var form1099KValidateFormResponseJson = string.Empty;
            var form1099KValidateFormResponse = new Form1099KValidateFormResponse();
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
                    string requestUri = Constants.VALIDATEFORM_1099K_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiValidateFormResponse = apiResponse.Content.ReadAsAsync<Form1099KValidateFormResponse>().Result;
                        if (apiValidateFormResponse != null)
                        {
                            form1099KValidateFormResponseJson = JsonConvert.SerializeObject(apiValidateFormResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KValidateFormResponse object
                            form1099KValidateFormResponse = JsonConvert.DeserializeObject<Form1099KValidateFormResponse>(form1099KValidateFormResponseJson);
                        }
                    }
                    else
                    {
                        var apiValidateFormResponse = apiResponse.Content.ReadAsAsync<Form1099KValidateFormResponse>().Result;
                        form1099KValidateFormResponseJson = JsonConvert.SerializeObject(apiValidateFormResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KValidateFormResponse object
                        form1099KValidateFormResponse = JsonConvert.DeserializeObject<Form1099KValidateFormResponse>(form1099KValidateFormResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KValidateFormResponse", form1099KValidateFormResponse);
        }
        #endregion

        #region Form 1099K Delete 
        [HttpGet]
        public ActionResult Delete(string submissionId, string recordId)
        {
            var form1099KDeleteResponseJson = string.Empty;
            var form1099KDeleteResponse = new Form1099KDeleteResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for Form1099K Delete
                    string requestUri = Constants.DELETE_1099K_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.DeleteAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099KDeleteResponse>().Result;
                        form1099KDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to form1099KDeleteResponse object
                        form1099KDeleteResponse = JsonConvert.DeserializeObject<Form1099KDeleteResponse>(form1099KDeleteResponseJson);
                    }
                    else
                    {
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099KDeleteResponse>().Result;
                        form1099KDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KDeleteResponse object
                        form1099KDeleteResponse = JsonConvert.DeserializeObject<Form1099KDeleteResponse>(form1099KDeleteResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KDeleteResponse", form1099KDeleteResponse);
        }
        #endregion

        #region Form 1099K Transmit 
        [HttpGet]
        public ActionResult Transmit(Guid submissionId, Guid recordId)
        {
            var form1099KTransmitResponseJson = string.Empty;
            var transmitRequest = new Form1099KTransmitRequest();
            var form1099KTransmitResponse = new Form1099KTransmitResponse();
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
                    //API URL to Form1099K Transmit
                    string requestUri = Constants.TRANSMIT_1099K_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, transmitRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form1099KTransmitResponse>().Result;
                        if (transmitResponse != null)
                        {
                            form1099KTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KTransmitResponse object
                            form1099KTransmitResponse = JsonConvert.DeserializeObject<Form1099KTransmitResponse>(form1099KTransmitResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form1099KTransmitResponse>().Result;
                        form1099KTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KTransmitResponse object
                        form1099KTransmitResponse = JsonConvert.DeserializeObject<Form1099KTransmitResponse>(form1099KTransmitResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KTransmitResponse", form1099KTransmitResponse);
        }
        #endregion

        #region Form 1099K RequestDraftPdfUrl 
        [HttpGet]
        public ActionResult RequestDraftPdfUrl(Guid recordId)
        {
            var form1099KDraftPdfResponseJson = string.Empty;
            var draftPdfUrlRequest = new RequestDraftPdfUrlRequest();
            var form1099KDraftPdfUrlResponse = new RequestDraftPdfUrlResponse();
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
                    string requestUri = Constants.DRAFTPDFURL_1099K_URL;
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
                            form1099KDraftPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KDraftPdfUrlResponse object
                            form1099KDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(form1099KDraftPdfResponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestDraftPdfUrlResponse>().Result;
                        form1099KDraftPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KDraftPdfUrlResponse object
                        form1099KDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(form1099KDraftPdfResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KDraftPdfResponse", form1099KDraftPdfUrlResponse);
        }
        #endregion

        #region Form1099K RequestDraftPdfUrl
        [HttpGet]
        public async Task<ActionResult> Form1099KRequestDraftPdfUrl(string DraftPdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(DraftPdfUrl))
            {
                string fileExtension = DraftPdfUrl.Substring(DraftPdfUrl.LastIndexOf('.') + 1, DraftPdfUrl.Length - (DraftPdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099KPdfS3ByFileName(DraftPdfUrl);
                string fileName = Path.GetFileName(DraftPdfUrl);
                if (file != null && file.Length > 0)
                {
                    Response.Clear();
                    //Response.Buffer = false;
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
            var form1099KRequestPdfResponseJson = string.Empty;
            var pdfUrlRequest = new RequestPdfURLRequest();
            var form1099KPdfUrlResponse = new Form1099KRequestPdfUrlsResponse();
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
                    //API URL For Form1099K RequestPdfUrl
                    string requestUri = Constants.RequestPdf_1099K_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, pdfUrlRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<Form1099KRequestPdfUrlsResponse>().Result;
                        if (draftPdfUrlResponse != null)
                        {
                            form1099KRequestPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form1099KPdfUrlResponse object
                            form1099KPdfUrlResponse = JsonConvert.DeserializeObject <Form1099KRequestPdfUrlsResponse>(form1099KRequestPdfResponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<Form1099KRequestPdfUrlsResponse>().Result;
                        form1099KRequestPdfResponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form1099KPdfUrlResponse object
                        form1099KPdfUrlResponse = JsonConvert.DeserializeObject<Form1099KRequestPdfUrlsResponse>(form1099KRequestPdfResponseJson);
                    }
                }
            }

            return PartialView("_Form1099KRequestPdfResponse", form1099KPdfUrlResponse);
        }
        #endregion

        #region RequestPdfUrl View
        public ActionResult _ViewRequestPdfUrl(string PdfUrl)
        {
            TempData["PdfUrl"] = PdfUrl;
            return PartialView();
        }
        #endregion

        #region Form1099K Request PdfUrl
        public async Task<ActionResult> RequestPdfUrls(string PdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(PdfUrl))
            {
                string fileExtension = PdfUrl.Substring(PdfUrl.LastIndexOf('.') + 1, PdfUrl.Length - (PdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099KPdfS3ByFileName(PdfUrl);
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