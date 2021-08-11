namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Collections.Generic;

    public interface IWritersService
    {
        void Create(string userId, string firstName, string lastName);

        IEnumerable<T> GetAllUnapproved<T>();

        bool IsWriter(string userId);

        void Approve(int id);

        void Deny(int id);
    }
}
