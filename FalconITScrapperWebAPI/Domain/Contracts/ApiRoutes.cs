using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string ApiVersion = "v1";
        public const string Base = Root + "/" + ApiVersion;
        public static class SIV
        {
            public const string GetNumberPlates = Base + "/SIV/GetNumberPlates";
        }
    }
}
