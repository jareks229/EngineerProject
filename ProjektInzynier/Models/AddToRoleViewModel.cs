using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjektInzynier.Models
{
    public class AddToRoleViewModel
    {
        public List<IdentityRole> IdentityRoles { get; set; }

        public IDictionary<string, string> IdentityRolesDictionary
        {
            get
            {
                return IdentityRoles?.ToDictionary(key => key.Id, value => value.Name);
            }
        }

        public List<IdentityUser> IdentityUsers { get; set; }

        public IDictionary<string, string> IdentityUsersDictionary
        {
            get
            {
                return IdentityUsers?.ToDictionary(key => key.Id, value => value.UserName);
            }
        }

        public string SelectedIdentityRoleId { get; set; }

        public string SelectedIdentityUserId { get; set; }
    }
}
