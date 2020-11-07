using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BlazorAuthenticationSample.Client.CustomProvider
{
    public class ApplicationUser //: IIdentity
    {
        public string UserId { get; set; }
        public string PassWord { get; set; }

        //public string AuthenticationType { get; set; }

        //public bool IsAuthenticated { get; set; }

        //public string Name { get; set; }
    }
}
