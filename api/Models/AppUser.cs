using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    // Extension of IdentityUser that has default values for user
    public class AppUser : IdentityUser<int>
    {

    }

    public class AppRole : IdentityRole<int>
    {

    }
}