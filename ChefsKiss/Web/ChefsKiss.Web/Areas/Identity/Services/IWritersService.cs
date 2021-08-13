namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWritersService
    {
        void Create(string userId, string firstName, string lastName);

        IEnumerable<T> GetAllUnapproved<T>();

        bool IsWriter(string userId);

        bool HasApplied(string userId);

        Task Approve(int id);

        void Deny(int id);
    }
}
