using System;
using System.Collections.Generic;

namespace GoodSign.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NetStandard.GoodSignClient("8fbfafd63c81a3b8d7841689d32115b4e79dd2f920ba63338f69a56195aa6ba7e4868a");

            var file = System.IO.File.ReadAllBytes(@"c:\temp\test.pdf");

            var doc = client.Templates.UploadPDF("test.pdf", file);

            var request = new GoodSign.NetStandard.Models.SignatureRequest();

            request.DocumentID = doc.ID;
            request.DocumentName = "Test Contract";
            request.Webhook = "https://webhook.site/ee1c6454-7c56-4024-847a-a08b16068cf9";

            var signer = new NetStandard.Models.Signer();

            signer.Key = "signer1";
            signer.Name = "Ben Macpherson";
            signer.Email = "ben@jbmac.com.au";

            request.Signers.Add(signer);

            request.Metadata.Add(new KeyValuePair<string, string>("PlacementID", "123"));


            request.EmailFromName = "Simon Moss";
            request.EmailFromAddress = "ben@recruitwizard.com";
            request.EmailSubject = "TRC needs your signature !";
            request.EmailMessage = "Yo it's simon, please sign the bits and bobs";

            var resp = client.Templates.SendTemplate(request);

            var voidResp = client.Templates.VoidDocument(resp.Document.ID);


            Console.WriteLine("Hello World!");
        }
    }
}
