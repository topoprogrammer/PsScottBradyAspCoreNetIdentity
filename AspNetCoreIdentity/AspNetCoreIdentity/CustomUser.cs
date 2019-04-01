using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentity
{
    //public class CustomUser
    //{
    //    public string Id { get; set; }
    //    public string UserName { get; set; }
    //    public string NormalizedUserName { get; set; }
    //    public string PasswordHash { get; set; }
    //}

    public class CustomUser : IdentityUser
    {
        public string Locale { get; set; } = "en-GB";
        public string OrgId { get; set; }
    }

    public class Organization
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
