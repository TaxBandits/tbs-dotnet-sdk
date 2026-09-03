using Form1099NecSDK.Models.Base;
using Form1099NecSDK.Models.Business;
using Form1099NecSDK.Models.Form1099NEC;
using Form1099NecSDK.Models.Form1099NECDelete;
using Form1099NecSDK.Models.Form1099NECDraftPdfUrl;
using Form1099NecSDK.Models.Form1099NECGet;
using Form1099NecSDK.Models.Form1099NecList;
using Form1099NecSDK.Models.Form1099NECRequestPdfUrl;
using Form1099NecSDK.Models.Form1099NECStatus;
using Form1099NecSDK.Models.Form1099NECTransmit;
using Form1099NecSDK.Models.Form1099NECUpdate;
using Form1099NecSDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Constants = Form1099NecSDK.Models.Utilities.Constants;
using Formatting = Newtonsoft.Json.Formatting;
namespace Form1099NecSDK.Controllers
{
    public class Form1099NecController : Controller
    {
        #region CreateNEC View
        public ActionResult CreateNECView(Guid businessId, string businessName, string tin)
        {
            if (businessId != Guid.Empty && !string.IsNullOrWhiteSpace(businessName) && !string.IsNullOrWhiteSpace(tin))
            {
                Form1099NecCreateRequest createRequest = new Form1099NecCreateRequest { ReturnHeader = new APIReturnHeader { Business = new Business { BusinessId = businessId, BusinessNm = businessName, EINorSSN = tin } } };
                return View(createRequest);
            }

            return View();
        }
        #endregion

        #region Form 1099Nec Create 
        [HttpPost]
        public ActionResult CreateNEC(Form1099NecCreateRequest createRequest)
        {
            var necCreateResponseJson = string.Empty;
            var necCreateResponse = new Form1099NecCreateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = AccessToken.GetGeneratedAccessToken();

            //If states does not contain state Alabama then initialize Al value as null
            if (!createRequest.ReturnData.Any(x => x.NECFormData.States.Any(x => x.StateCd == "AL")))
            {
                createRequest.StateReconData.AL = null;
            }

            //If states does not contain state WestVirginia then initialize WV value as null
            if (!createRequest.ReturnData.Any(x => x.NECFormData.States.Any(x => x.StateCd == "WV")))
            {
                createRequest.StateReconData.WV = null;
            }

            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for NEC Create
                    string requestUri = Constants.CREATE_NEC;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099NecCreateResponse>().Result;
                        if (createResponse != null)
                        {
                            necCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necCreateResponse object
                            necCreateResponse = JsonConvert.DeserializeObject<Form1099NecCreateResponse>(necCreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync<Form1099NecCreateResponse>().Result;
                        necCreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necCreateResponse object
                        necCreateResponse = JsonConvert.DeserializeObject<Form1099NecCreateResponse>(necCreateResponseJson);
                    }
                }
            }

            return PartialView("_NECCreateResponse", necCreateResponse);
        }
        #endregion

        #region Form 1099Nec List
        [HttpGet]
        public ActionResult GetNECList(Guid businessId, string businessName, string tin)
        {
            var necListReturnResponse = new Form1099NecListResponse();
            var necGetReturnResponseJSON = string.Empty;
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
                    //API URL to Get Nec List Return
                    string requestUri = Constants.NEC_LIST + "?BusinessId=" + businessId + "&Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099NecListResponse>().Result;
                        if (listResponse != null)
                        {
                            necGetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            necListReturnResponse = JsonConvert.DeserializeObject<Form1099NecListResponse>(necGetReturnResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form1099NecListResponse>().Result;
                        necGetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        necListReturnResponse = JsonConvert.DeserializeObject<Form1099NecListResponse>(necGetReturnResponseJSON);
                        var businessDetails = new Form1099NecList();
                        necListReturnResponse.Form1099Records = new List<Form1099NecList>();
                        businessDetails.BusinessId = businessId;
                        businessDetails.BusinessNm = businessName;
                        businessDetails.EINorSSN = tin;
                        necListReturnResponse.Form1099Records.Add(businessDetails);
                    }
                }
            }

            return View("GetNECList", necListReturnResponse.Form1099Records);
        }
        #endregion

        #region Get NecDetail By Submissionid and recordid
        [HttpGet]
        public IActionResult GetNECDetail(Guid submissionId, Guid recordId)
        {
            var necGetResponse = new Form1099NecGetResponse();
            var states = new Form1099StateDetail();
            var necGetResponseJSON = string.Empty;
            var necDetails = new Form1099NecCreateRequest();
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
                    //API URL to Get NEC Return using SubmissionId And RecordId
                    string requestUri = Constants.GET_NEC + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099NecGetResponse>().Result;
                        if (getResponse != null)
                        {
                            necGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                            necGetResponse = JsonConvert.DeserializeObject<Form1099NecGetResponse>(necGetResponseJSON);
                            if (necGetResponse != null)
                            {
                                necDetails.ReturnHeader = necGetResponse.Form1099Records.ReturnHeader;
                                necDetails.SubmissionManiFest = necGetResponse.Form1099Records.SubmissionManifest;
                                necDetails.ReturnData = necGetResponse.Form1099Records.ReturnData;
                                if (necDetails.ReturnData.Any(x => x.NECFormData.States != null && x.NECFormData.States.Count == 1))
                                {
                                    necDetails.ReturnData.ForEach(x => x.NECFormData.States.Add(states));
                                }
                                necDetails.StateReconData = necGetResponse.Form1099Records.StateReconData;
                            }
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form1099NecGetResponse>().Result;
                        necGetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                        necGetResponse = JsonConvert.DeserializeObject<Form1099NecGetResponse>(necGetResponseJSON);
                    }
                }
            }

            return View("CreateNECView", necDetails);
        }
        #endregion

        #region NEC Update 
        [HttpPost]
        public ActionResult UpdateNEC(Form1099NecUpdateRequest updateNEC)
        {
            var necUpdateResponseJson = string.Empty;
            var necUpdateResponse = new Form1099NecUpdateResponse();
            string apiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            string generatedAccessToken = accessToken.GetGeneratedAccessToken();

            if (!updateNEC.ReturnData.Any(x => x.NECFormData.States.Any(x => x.StateCd == "AL")))
            {
                updateNEC.StateReconData.AL = null;
            }
            if (!updateNEC.ReturnData.Any(x => x.NECFormData.States.Any(x => x.StateCd == "WV")))
            {
                updateNEC.StateReconData.WV = null;
            }
            if (!string.IsNullOrWhiteSpace(generatedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL For Nec Update
                    string requestUri = Constants.UPDATE_NEC;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Put Response from API
                    var apiResponse = apiClient.PutAsJsonAsync(requestUri, updateNEC).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var updateResponse = apiResponse.Content.ReadAsAsync<Form1099NecUpdateResponse>().Result;
                        if (updateResponse != null)
                        {
                            necUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necupdateResponse object
                            necUpdateResponse = JsonConvert.DeserializeObject<Form1099NecUpdateResponse>(necUpdateResponseJson);
                        }
                    }
                    else
                    {
                        var updateResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        necUpdateResponseJson = JsonConvert.SerializeObject(updateResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necupdateResponse object
                        necUpdateResponse = JsonConvert.DeserializeObject<Form1099NecUpdateResponse>(necUpdateResponseJson);
                    }
                }
            }

            return PartialView("_NECUpdateResponse", necUpdateResponse);
        }
        #endregion

        #region Get Nec Status 
        [HttpGet]
        public IActionResult GetStatus(string submissionId, string recordId)
        {
            var necStatusResponse = new Form1099NecStatusResponse();
            var necStatusResponseJSON = string.Empty;
            //Get  token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var jwt = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Status using SubmissionId and RecordId 
                    string requestUrl = Constants.NEC_STATUS_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    //Get URLs from App.Config
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, jwt);
                    //Get Status Response
                    var apiResponse = apiClient.GetAsync(requestUrl).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Form1099NecStatusResponse>().Result;
                        necStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to necStatusResponse object
                        necStatusResponse = JsonConvert.DeserializeObject<Form1099NecStatusResponse>(necStatusResponseJSON);
                    }
                    else
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        necStatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to necStatusResponse object
                        necStatusResponse = JsonConvert.DeserializeObject<Form1099NecStatusResponse>(necStatusResponseJSON);
                    }
                }
            }

            return PartialView("_NECStatusResponse", necStatusResponse);
        }
        #endregion

        #region Form 1099Nec ValidateForm 
        [HttpPost]
        public ActionResult ValidateForm(Form1099NecCreateRequest createRequest)
        {
            var necValidateFormResponseJson = string.Empty;
            var necValidateFormResponse = new ValidateFormNECResponse();
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
                    string requestUri = Constants.NEC_VALIDATEFORM_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<ValidateFormNECResponse>().Result;
                        if (validateFormResponse != null)
                        {
                            necValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necValidateFormResponse object
                            necValidateFormResponse = JsonConvert.DeserializeObject<ValidateFormNECResponse>(necValidateFormResponseJson);
                        }
                    }
                    else
                    {
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<ValidateFormNECResponse>().Result;
                        necValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necValidateFormResponse object
                        necValidateFormResponse = JsonConvert.DeserializeObject<ValidateFormNECResponse>(necValidateFormResponseJson);
                    }
                }
            }

            return PartialView("_NECValidateFormResponse", necValidateFormResponse);
        }
        #endregion

        #region Form 1099Nec Delete 
        [HttpGet]
        public ActionResult Delete(string submissionId, string recordId)
        {
            var necDeleteResponseJson = string.Empty;
            var necDeleteResponse = new Form1099NecDeleteResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for Nec Delete
                    string requestUri = Constants.NEC_DELETE_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.DeleteAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099NecDeleteResponse>().Result;
                        necDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to necDeleteResponse object
                        necDeleteResponse = JsonConvert.DeserializeObject<Form1099NecDeleteResponse>(necDeleteResponseJson);
                    }
                    else
                    {
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form1099NecDeleteResponse>().Result;
                        necDeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necDeleteResponse object
                        necDeleteResponse = JsonConvert.DeserializeObject<Form1099NecDeleteResponse>(necDeleteResponseJson);
                    }
                }
            }

            return PartialView("_NECDeleteResponse", necDeleteResponse);
        }
        #endregion

        #region Form 1099Nec Transmit 
        [HttpGet]
        public ActionResult Transmit(Guid? submissionId, Guid recordId)
        {
            var necTransmitResponseJson = string.Empty;
            var transmitRequest = new Form1099NecTransmitRequest();
            var necTransmitResponse = new Form1099NecTransmitResponse();
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
                    //API URL to NEC Transmit
                    string requestUri = Constants.NEC_TRANSMIT_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, transmitRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form1099NecTransmitResponse>().Result;
                        if (transmitResponse != null)
                        {
                            necTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necTransmitResponse object
                            necTransmitResponse = JsonConvert.DeserializeObject<Form1099NecTransmitResponse>(necTransmitResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form1099NecTransmitResponse>().Result;
                        necTransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necTransmitResponse object
                        necTransmitResponse = JsonConvert.DeserializeObject<Form1099NecTransmitResponse>(necTransmitResponseJson);
                    }
                }
            }

            return PartialView("_NECTransmitResponse", necTransmitResponse);
        }
        #endregion

        #region Form 1099Nec RequestDraftPdfUrl 
        [HttpGet]
        public ActionResult RequestDraftPdfUrl(Guid recordId)
        {
            var necDraftPdfresponseJson = string.Empty;
            var draftPdfUrlRequest = new RequestDraftPdfUrl();
            var necDraftPdfUrlResponse = new RequestDraftPdfUrlResponse();
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
                    string requestUri = Constants.NEC_DRAFT_URL;
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
                            necDraftPdfresponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necDraftPdfUrlResponse object
                            necDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(necDraftPdfresponseJson);
                        }
                    }
                    else
                    {
                        var draftPdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestDraftPdfUrlResponse>().Result;
                        necDraftPdfresponseJson = JsonConvert.SerializeObject(draftPdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necDraftPdfUrlResponse object
                        necDraftPdfUrlResponse = JsonConvert.DeserializeObject<RequestDraftPdfUrlResponse>(necDraftPdfresponseJson);
                    }
                }
            }

            return PartialView("_NECDraftPdfResponse", necDraftPdfUrlResponse);
        }
        #endregion

        #region Form10999NECRequestDraftPdfUrl
        public async Task<ActionResult> Form1099NecRequestDraftPdfUrl(string DraftPdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(DraftPdfUrl))
            {
                string fileExtension = DraftPdfUrl.Substring(DraftPdfUrl.LastIndexOf('.') + 1, DraftPdfUrl.Length - (DraftPdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099NecPdfS3ByFileName(DraftPdfUrl);
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
            var necRequestPdfResponseJson = string.Empty;
            var pdfUrlRequest = new RequestPdfURLRequest();
            var necPdfUrlResponse = new RequestPdfUrlsResponse();
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
                    //API URL For NEC RequestPdfUrl
                    string requestUri = Constants.NEC_REQUESTPDF_URL;
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
                            necRequestPdfResponseJson = JsonConvert.SerializeObject(pdfUrlResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to necPdfUrlResponse object
                            necPdfUrlResponse = JsonConvert.DeserializeObject<RequestPdfUrlsResponse>(necRequestPdfResponseJson);
                        }
                    }
                    else
                    {
                        var pdfUrlResponse = apiResponse.Content.ReadAsAsync<RequestPdfUrlsResponse>().Result;
                        necRequestPdfResponseJson = JsonConvert.SerializeObject(pdfUrlResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to necPdfUrlResponse object
                        necPdfUrlResponse = JsonConvert.DeserializeObject<RequestPdfUrlsResponse>(necRequestPdfResponseJson);
                    }
                }
            }

            return PartialView("_NECRequestPdfResponse", necPdfUrlResponse);
        }
        #endregion

        #region RequestPdfUrl View
        public ActionResult _ViewRequestPdfUrl(string PdfUrl)
        {
            TempData["PdfUrl"] = PdfUrl;

            return PartialView();
        }
        #endregion

        #region Form10999NEC Request PdfUrl
        public async Task<ActionResult> RequestPdfUrls(string PdfUrl)
        {
            if (!string.IsNullOrWhiteSpace(PdfUrl))
            {
                string fileExtension = PdfUrl.Substring(PdfUrl.LastIndexOf('.') + 1, PdfUrl.Length - (PdfUrl.LastIndexOf('.') + 1));
                byte[] file = Utility.GetForm1099NecPdfS3ByFileName(PdfUrl);
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