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
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new HashSet<IdentityUserLogin<string>>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual ICollection<Review> Reviews { get; init; } = new List<Review>();
    }
}
