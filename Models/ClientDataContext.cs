using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExactOnline.Client.Models;

namespace ConsoleApplication.Models
{
    public class ClientDataContext
    {
        public FileContent FileContent { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public Account ExactAccount { get; set; }
        public DocumentCategory Category { get; set; }
    }
}
