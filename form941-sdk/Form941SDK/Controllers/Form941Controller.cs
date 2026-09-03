using Form941SDK.Models.Business;
using Form941SDK.Models.Form941Create;
using Form941SDK.Models.Form941Delete;
using Form941SDK.Models.Form941Get;
using Form941SDK.Models.Form941GetPdf;
using Form941SDK.Models.Form941List;
using Form941SDK.Models.Form941Status;
using Form941SDK.Models.Form941Transmit;
using Form941SDK.Models.Form941Update;
using Form941SDK.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Form941SDK.Controllers
{
    public class Form941Controller : Controller
    {
        #region CreateForm 941 View
        public ActionResult CreateForm941View(Guid businessId, string businessName, string tin)
        {
            if (businessId != Guid.Empty && !string.IsNullOrWhiteSpace(businessName) && !string.IsNullOrWhiteSpace(tin))
            {
                Form941CreateRequest createRequest = new Form941CreateRequest
                {
                    Form941Records = new List<Form941Details> { new Form941Details { ReturnHeader = new Form941ReturnHeader { Business = new Business { BusinessId = businessId, BusinessNm = businessName, EINorSSN = tin } } } }
                };
                return View(createRequest);
            }
            return View();
        }
        #endregion

        #region Form 941 Create 
        [HttpPost]
        public ActionResult CreateForm941(Form941CreateRequest createRequest)
        {
            var form941CreateResponseJson = string.Empty;
            var form941CreateResponse = new Form941CreateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            createRequest.Form941Records.ForEach(record => record.ReturnHeader.ReturnType = "Form941");
            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for K Create
                    string requestUri = Constants.CREATE_FORM941_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <Form941CreateResponse>
                        ().Result;
                        if (createResponse != null)
                        {
                            form941CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form941CreateResponse object
                            form941CreateResponse = JsonConvert.DeserializeObject
                            <Form941CreateResponse>
                            (form941CreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <Form941CreateResponse>
                        ().Result;
                        form941CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941CreateResponse object
                        form941CreateResponse = JsonConvert.DeserializeObject
                        <Form941CreateResponse>
                        (form941CreateResponseJson);
                    }
                }
            }
            return PartialView("_Form941CreateResponse", form941CreateResponse);
        }
        #endregion

        #region Form 941 List
        [HttpGet]
        public ActionResult Get941List(Guid businessId, string businessName, string tin)
        {
            var form941ListReturnResponse = new Form941RecordsResponse();
            var form941GetReturnResponseJSON = string.Empty;
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
                    //API URL to Get form941 List Return
                    string requestUri = Constants.FORM941_LIST_URL + "?BusinessId=" + businessId + "&Page=1&PageSize=10";
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form941RecordsResponse>().Result;
                        if (listResponse != null)
                        {
                            form941GetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                            form941ListReturnResponse = JsonConvert.DeserializeObject<Form941RecordsResponse>(form941GetReturnResponseJSON);
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var listResponse = apiResponse.Content.ReadAsAsync<Form941RecordsResponse>().Result;
                        form941GetReturnResponseJSON = JsonConvert.SerializeObject(listResponse, Formatting.Indented);
                        form941ListReturnResponse = JsonConvert.DeserializeObject<Form941RecordsResponse>(form941GetReturnResponseJSON);
                        var businessDetails = new Form941ListResponse();
                        form941ListReturnResponse.Form941Records = new List<Form941ListResponse>();
                        businessDetails.BusinessId = businessId;
                        businessDetails.BusinessNm = businessName;
                        businessDetails.EIN = tin;
                        form941ListReturnResponse.Form941Records.Add(businessDetails);
                    }
                }
            }

            return View("GetForm941List", form941ListReturnResponse.Form941Records);
        }
        #endregion

        #region Get 941Detail By Submissionid and recordid
        [HttpGet]
        public IActionResult Get941Detail(Guid submissionId, Guid recordId)
        {
            var form941GetResponse = new Form941GetRecordResponse();
            var form941GetResponseJSON = string.Empty;
            var form941Details = new Form941CreateRequest();
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
                    //API URL to Get form941 Return using SubmissionId And RecordId
                    string requestUri = Constants.GET_941_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(apiUrl);
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, generatedAccessToken);
                    //Get Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form941GetRecordResponse>().Result;
                        if (getResponse != null)
                        {
                            form941GetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                            form941GetResponse = JsonConvert.DeserializeObject<Form941GetRecordResponse>(form941GetResponseJSON);
                            if (form941GetResponse != null)
                            {
                                form941Details.SubmissionId = form941GetResponse.SubmissionId;
                                form941Details.Form941Records = new List<Form941Details>();
                                foreach (var record in form941GetResponse.Form941Records)
                                {
                                    form941Details.Form941Records.Add(new Form941Details// Replace YourRecordType with the actual type of your records
                                    {
                                        SequenceId=record.SequenceId,
                                        RecordId=record.RecordId,
                                        ReturnData = new Form941ReturnDataDetail // Replace YourReturnDataType with the actual type of your ReturnData
                                        {
                                            Form941 = record.ReturnData.Form941,
                                            DepositScheduleType=record.ReturnData.DepositScheduleType,
                                            IRSPaymentType=record.ReturnData.IRSPaymentType,
                                            IRSPayment=record.ReturnData.IRSPayment
                                        },
                                        ReturnHeader = record.ReturnHeader
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        //Read Response from API
                        var getResponse = apiResponse.Content.ReadAsAsync<Form941GetRecordResponse>().Result;
                        form941GetResponseJSON = JsonConvert.SerializeObject(getResponse, Formatting.Indented);
                        form941GetResponse = JsonConvert.DeserializeObject<Form941GetRecordResponse>(form941GetResponseJSON);
                    }
                }
            }
            return View("CreateForm941View", form941Details);
        }
        #endregion

        #region Form 941 Update 
        [HttpPost]
        public ActionResult UpdateForm941(Form941UpdateRequest updateRequest)
        {
            var form941CreateResponseJson = string.Empty;
            var form941CreateResponse = new Form941UpdateResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            updateRequest.Form941Records.ForEach(record => record.ReturnHeader.ReturnType = "Form941");


            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for K Create
                    string requestUri = Constants.UPDATE_FORM941_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response from Api
                    var apiResponse = apiClient.PutAsJsonAsync(requestUri, updateRequest).Result;
                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <Form941UpdateResponse>
                        ().Result;
                        if (createResponse != null)
                        {
                            form941CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form941CreateResponse object
                            form941CreateResponse = JsonConvert.DeserializeObject
                            <Form941UpdateResponse>
                            (form941CreateResponseJson);
                        }
                    }
                    else
                    {
                        var createResponse = apiResponse.Content.ReadAsAsync
                        <Form941UpdateResponse>
                        ().Result;
                        form941CreateResponseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941CreateResponse object
                        form941CreateResponse = JsonConvert.DeserializeObject
                        <Form941UpdateResponse>
                        (form941CreateResponseJson);
                    }
                }
            }
            return PartialView("_Form941UpdateResponse", form941CreateResponse);
        }
        #endregion

        #region Get 941 Status 
        [HttpGet]
        public IActionResult GetStatus(string submissionId, string recordId)
        {
            var form941StatusResponse = new Form941EfileStatusResponse();
            var form941StatusResponseJSON = string.Empty;
            //Get  token from GetAccessToken Class
            GetAccessToken accessToken = new GetAccessToken(HttpContext);
            //Get Access token from OAuth API response
            var jwt = accessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(jwt))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to Get Status using SubmissionId and RecordId 
                    string requestUrl = Constants.FORM941_STATUS_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    //Get URLs from App.Config
                    apiClient.BaseAddress = new Uri(Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL));
                    //Construct HTTP headers
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, jwt);
                    //Get Status Response
                    var apiResponse = apiClient.GetAsync(requestUrl).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Form941EfileStatusResponse>().Result;
                        form941StatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to getStatusResponse object
                        form941StatusResponse = JsonConvert.DeserializeObject<Form941EfileStatusResponse>(form941StatusResponseJSON);
                    }
                    else
                    {
                        //Read Response from API
                        var getStatusResponse = apiResponse.Content.ReadAsAsync<Object>().Result;
                        form941StatusResponseJSON = JsonConvert.SerializeObject(getStatusResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to getStatusResponse object
                        form941StatusResponse = JsonConvert.DeserializeObject<Form941EfileStatusResponse>(form941StatusResponseJSON);
                    }
                }
            }

            return PartialView("_Form941StatusResponse", form941StatusResponse);
        }
        #endregion

        #region Form 941 ValidateForm 
        [HttpPost]
        public ActionResult ValidateForm(Form941CreateRequest createRequest)
        {
            var form941ValidateFormResponseJson = string.Empty;
            var form941ValidateFormResponse = new ValidateForm941Response();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();
            createRequest.Form941Records.ForEach(record => record.ReturnHeader.ReturnType = "Form941");

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for ValidateForm Create
                    string requestUri = Constants.FORM941_VALIDATEFORM_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, createRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<ValidateForm941Response>().Result;
                        if (validateFormResponse != null)
                        {
                            form941ValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form941ValidateFormResponse object
                            form941ValidateFormResponse = JsonConvert.DeserializeObject<ValidateForm941Response>(form941ValidateFormResponseJson);
                        }
                    }
                    else
                    {
                        var validateFormResponse = apiResponse.Content.ReadAsAsync<ValidateForm941Response>().Result;
                        form941ValidateFormResponseJson = JsonConvert.SerializeObject(validateFormResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941ValidateFormResponse object
                        form941ValidateFormResponse = JsonConvert.DeserializeObject<ValidateForm941Response>(form941ValidateFormResponseJson);
                    }
                }
            }

            return PartialView("_Form941ValidateFormResponse", form941ValidateFormResponse);
        }
        #endregion

        #region Form 941 Delete 
        [HttpGet]
        public ActionResult Delete(string submissionId, string recordId)
        {
            var form941DeleteResponseJson = string.Empty;
            var form941DeleteResponse = new Form941DeleteResponse();
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL for 941 Delete
                    string requestUri = Constants.FORM941_DELETE_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.DeleteAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form941DeleteResponse>().Result;
                        form941DeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to form941DeleteResponse object
                        form941DeleteResponse = JsonConvert.DeserializeObject<Form941DeleteResponse>(form941DeleteResponseJson);
                    }
                    else
                    {
                        var apiDeleteResponse = apiResponse.Content.ReadAsAsync<Form941DeleteResponse>().Result;
                        form941DeleteResponseJson = JsonConvert.SerializeObject(apiDeleteResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941DeleteResponse object
                        form941DeleteResponse = JsonConvert.DeserializeObject<Form941DeleteResponse>(form941DeleteResponseJson);
                    }
                }
            }

            return PartialView("_Form941DeleteResponse", form941DeleteResponse);
        }
        #endregion

        #region Form 941 Transmit 
        [HttpGet]
        public ActionResult Transmit(Guid submissionId, Guid recordId)
        {
            var form941TransmitResponseJson = string.Empty;
            var transmitRequest = new Form941TransmitRequest();
            var form941TransmitResponse = new Form941TransmitResponse();
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
                    //API URL to 941 Transmit
                    string requestUri = Constants.FORM941_TRANSMIT_URL;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.PostAsJsonAsync(requestUri, transmitRequest).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form941TransmitResponse>().Result;
                        if (transmitResponse != null)
                        {
                            form941TransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form941TransmitResponse object
                            form941TransmitResponse = JsonConvert.DeserializeObject<Form941TransmitResponse>(form941TransmitResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form941TransmitResponse>().Result;
                        form941TransmitResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941TransmitResponse object
                        form941TransmitResponse = JsonConvert.DeserializeObject<Form941TransmitResponse>(form941TransmitResponseJson);
                    }
                }
            }

            return PartialView("_Form941TransmitResponse", form941TransmitResponse);
        }
        #endregion

        #region Form 941 GetPdf 
        [HttpGet]
        public ActionResult GetPdf(Guid submissionId, Guid recordId)
        {
            var form941GetPdfResponseJson = string.Empty;
            var form941GetPdfResponse = new Form941PdfResponse();
            
            //Get APIUrl From Appsetting 
            string ApiUrl = Utility.GetAppSettings(Constants.TBS_PUBLIC_API_BASE_URL);
            //Get Access token from OAuth API response 
            GetAccessToken AccessToken = new GetAccessToken(HttpContext);
            string GeneratedAccessToken = AccessToken.GetGeneratedAccessToken();

            if (!string.IsNullOrWhiteSpace(GeneratedAccessToken))
            {
                using (var apiClient = new HttpClient())
                {
                    //API URL to 941 Transmit
                    string requestUri = Constants.FORM941_GETPDF_URL + "?SubmissionId=" + submissionId + "&RecordIds=" + recordId;
                    apiClient.BaseAddress = new Uri(ApiUrl);
                    //Construct HTTP headers in Generated Token.
                    OAuthGenerator.ConstructHeadersWithAccessToken(apiClient, GeneratedAccessToken);
                    //Post Response
                    var apiResponse = apiClient.GetAsync(requestUri).Result;

                    if (apiResponse != null && apiResponse.IsSuccessStatusCode)
                    {
                        //Read Response
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form941PdfResponse>().Result;
                        if (transmitResponse != null)
                        {
                            form941GetPdfResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                            //Deserializing JSON (Success Response) to form941TransmitResponse object
                            form941GetPdfResponse = JsonConvert.DeserializeObject<Form941PdfResponse>(form941GetPdfResponseJson);
                        }
                    }
                    else
                    {
                        var transmitResponse = apiResponse.Content.ReadAsAsync<Form941PdfResponse>().Result;
                        form941GetPdfResponseJson = JsonConvert.SerializeObject(transmitResponse, Formatting.Indented);
                        //Deserializing JSON (Error Response) to form941TransmitResponse object
                        form941GetPdfResponse = JsonConvert.DeserializeObject<Form941PdfResponse>(form941GetPdfResponseJson);
                    }
                }
            }

            return PartialView("_Form941GetPdfResponse", form941GetPdfResponse);
        }
        #endregion
    }
}
