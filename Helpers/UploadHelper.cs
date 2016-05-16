using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Models;

namespace ConsoleApplication.Helpers
{
    public class UploadHelper: IClientHelper
    {
        /// <summary>
        /// Upload file to Exact Online once it’s get placed on Dropbox
        /// </summary>
        /// <param name="context">The data context to be uploaded.</param>
        public void Process(ClientDataContext context)
        {
            var connector = new Connector(context.ClientInfo.ClientId, context.ClientInfo.ClientSecret, context.ClientInfo.CallbackUrl);
            var client = new ExactOnlineClient(connector.EndPoint, connector.GetAccessToken);            
            
            UploadDocument(client, context);
        }

        /// <summary>
        /// Upload document to Exact Online
        /// </summary>
        /// <param name="client">The ExactOnlineClient.</param>
        /// <param name="context">The data context to be uploaded.</param>
        private void UploadDocument(ExactOnlineClient client, ClientDataContext context)
        {

            var doc = new Document()
            {
                Subject = context.FileContent.Subject,
                Type = 149,
                Body = System.Text.Encoding.UTF8.GetString(context.FileContent.Content),
                Division = client.GetDivision(),
                AccountName = context.ExactAccount.Name,
                DocumentDate = DateTime.Now,
                Account = context.ExactAccount.ID,
                Category = context.Category.ID
            };
            client.For<Document>().Insert(ref doc);

            var docAttach = new DocumentAttachment()
            {
                FileName = System.IO.Path.GetFileName(context.FileContent.FileName),
                Attachment = context.FileContent.Content,
                FileSize = context.FileContent.Content.Length,
                Document = doc.ID,
                ID = Guid.NewGuid()
            };
            client.For<DocumentAttachment>().Insert(ref docAttach);
        }


    }
}
