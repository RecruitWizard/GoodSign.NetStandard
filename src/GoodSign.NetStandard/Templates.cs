using GoodSign.NetStandard.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace GoodSign.NetStandard
{
    public class Templates
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static Templates instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        public static Templates Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (instance == null)
                    {
                        instance = new Templates();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton Pattern

        /// <summary>
        /// Get all Templates
        /// </summary>
        /// <returns></returns>
        public List<Template> GetTemplates()
        {
            var client = new RestClient(Configuration.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());
            var request = new RestRequest("templates");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");

            var response = client.GetAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;

                var results = JsonConvert.DeserializeObject<List<Template>>(content);

                return results;
            }
            else if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<Template>();
            }
            else
            {
                throw new Exception($"HttpStatusCode: {response.StatusCode}");
            }
        }

        public GetDocumentResponse GetDocument(Guid documentId)
        {
            var client = new RestClient(Configuration.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());
            var request = new RestRequest($"document\\{documentId}");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");

            var response = client.GetAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;

                var results = JsonConvert.DeserializeObject<GetDocumentResponse>(content);

                return results;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception($"HttpStatusCode: {response.StatusCode}");
            }
        }

        public enum FileType
        {
            MainDoc = 0,
            Attachments = 1
        }

        public byte[] GetDocumentDownload(GetDocumentResponse doc, FileType fileType)
        {
            if (doc.MasterDoc.Status != "complete")
                throw new Exception("Documents only available for download for completed requests");

            var url = "";

            switch(fileType)
            {
                case FileType.MainDoc:
                    url = doc.MasterDoc.MainDocumentDownloadUrl;
                    break;
                case FileType.Attachments:
                    url = doc.MasterDoc.OtherDocumentsDownloadUrl;
                    break;
            }

            var client = new RestClient();
            var request = new RestRequest($"{url}");

            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");

            var response = client.DownloadDataAsync(request).Result;

            return response;
        }

        public SignatureResponse SendTemplate(SignatureRequest signatureRequest)
        {
            var client = new RestClient(Configuration.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());
            var request = new RestRequest($"usetemplate");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");

            request.AddJsonBody(signatureRequest);

            var response = client.PostAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;

                var results = JsonConvert.DeserializeObject<SignatureResponse>(content);

                return results;
            }
            else
            {
                throw new Exception($"HttpStatusCode: {response.StatusCode} {response.Content}");
            }
        }

        public SendReminderResponse SendReminderToAllUnsigned(Guid documentId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{Configuration.BaseUrl}/document/{documentId}/remindall"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Configuration.ApiKey}");

                    var content = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<SendReminderResponse>(content);

                    return result;
                }
            }
        }

        public VoidDocumentResponse VoidDocument(Guid documentId, bool notify = false, string message = null)
        {            
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{Configuration.BaseUrl}/document/{documentId}/void"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Configuration.ApiKey}");

                    var multipartContent = new MultipartFormDataContent();

                    multipartContent.Add(new StringContent(notify.ToString()), "notify");

                    if(notify && !string.IsNullOrEmpty(message))
                        multipartContent.Add(new StringContent(message), "msg");

                    request.Content = multipartContent;
                                                      
                    var content = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<VoidDocumentResponse>(content);

                    return result;
                }
            }           
        }

        public Document UploadPDF(string fileName, byte[] fileContents)
        {
            var client = new RestClient(Configuration.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());

            var request = new RestRequest($"uploadpdf");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");

            request.AddFile("file", fileContents, fileName);

            var response = client.PostAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;

                var results = JsonConvert.DeserializeObject<Document>(content);

                return results;
            }
            else
            {
                throw new Exception($"HttpStatusCode: {response.StatusCode} {response.Content}");
            }
        }
        
        public Document UploadAttachment(Guid mainDocumentId, string fileName, byte[] fileContents)
        {
            var client = new RestClient(Configuration.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());

            var request = new RestRequest($"uploadpdf");

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Configuration.ApiKey}");
            
            request.AddFile("file", fileContents, fileName);
            request.AddParameter("uuid", mainDocumentId);
            request.AddParameter("attachment", "true");

            var response = client.PostAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content;

                var results = JsonConvert.DeserializeObject<Document>(content);

                return results;
            }
            else
            {
                throw new Exception($"HttpStatusCode: {response.StatusCode} {response.Content}");
            }
        }
    }
}