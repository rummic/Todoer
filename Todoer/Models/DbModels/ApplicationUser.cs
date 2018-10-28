using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Todoer.Models.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Task> Tasks { get; set; }
        [PersonalData]
        public string Bio { get; set; }
        [PersonalData]
        public string PictureUrl { get; set; }
    }
}
