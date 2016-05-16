using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication.Models;

namespace ConsoleApplication.Helpers
{
    public interface IClientHelper
    {
        void Process(ClientDataContext context);
    }
}
