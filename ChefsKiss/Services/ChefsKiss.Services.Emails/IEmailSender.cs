namespace ChefsKiss.Services.Emails
{
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task Welcome(string email, string name);

        Task AuthorApplied(string email, string name);

        Task AuthorApproved(string email, string name);

        Task AuthorDenied(string email, string name);
    }
}
