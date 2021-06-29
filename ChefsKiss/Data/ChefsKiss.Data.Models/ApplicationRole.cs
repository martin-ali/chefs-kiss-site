namespace ChefsKiss.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using ChefsKiss.Data.Common.Models;

    public class ApplicationRole : IdentityRole, IAuditInfo
    {
        public ApplicationRole()
            : this(null) { }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
