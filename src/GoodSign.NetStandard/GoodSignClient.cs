using System;

namespace GoodSign.NetStandard
{
    public class GoodSignClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="token"></param>
        public GoodSignClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("Empty apiKey not Allowed");

            Configuration.ApiKey = apiKey;
            Configuration.BaseUrl = "https://goodsign.io/api";

            var templates = Templates.Instance.GetTemplates();
        }

        public Templates Templates
        {
            get { return Templates.Instance; }
        }       
    }
}
