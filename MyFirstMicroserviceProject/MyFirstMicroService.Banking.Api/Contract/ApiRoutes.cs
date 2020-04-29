using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Api.Contract
{
    public static class ApiRoutes
    {
       public const string Version = "1";
       public const string Root = "api";
       public const string Base = Root + "/" + Version;

        public static class Banking
        {
            public const string Base = "/banking";

        }
    }
 
}
