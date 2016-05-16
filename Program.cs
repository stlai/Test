using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Models;
using ConsoleApplication.Helpers;
using DropNet;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            const string dropboxClientId = "xxx";
            const string dropboxClientSecret = "xxx";
            const string dropboxAccessToken = "xxx";
            const string exactClientId = "xxx";
            const string exactClientSecret = "xxx";
            const string exactAccountId = "xxx";
            const string docCategoryId = "xxx";

            // initialize DropBox client
            var client = new DropNetClient(dropboxClientId, dropboxClientSecret, dropboxAccessToken);
            string fileName = "LICENSE";
            string fileExt = ".txt";
            var fileContent = client.GetFile("/" + fileName + fileExt);

            // set the client data
            var context = new ClientDataContext()
            {
                ClientInfo = new ClientInfo() { ClientId = exactClientId, ClientSecret = exactClientSecret, CallbackUrl = new Uri("http://www.exact.com") },
                FileContent = new FileContent() { Subject = fileName + "_100", FileName = fileName + fileExt, Content = fileContent },
                ExactAccount = new Account() { ID = new Guid(exactAccountId) },
                Category = new DocumentCategory() { ID = new Guid(docCategoryId) }
            };

            // upload the document
            IClientHelper helper = new UploadHelper();
            helper.Process(context);

            Console.ReadLine();
        }
    }
}

