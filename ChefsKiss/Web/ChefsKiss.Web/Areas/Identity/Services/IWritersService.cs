namespace ChefsKiss.Web.Areas.Identity.Services
{
    public interface IWritersService
    {
        void Create(string userId, string firstName, string lastName);

        bool IsWriter(string userId);

        void Approve(int id);
    }
}
