namespace ChefsKiss.Data.Models
{

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using ChefsKiss.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public IEnumerable<Review> Reviews { get; init; } = new List<Review>();
    }
}
