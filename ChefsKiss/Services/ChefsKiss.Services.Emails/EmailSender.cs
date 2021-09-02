namespace ChefsKiss.Services.Emails
{
    using System;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    using static ChefsKiss.Common.EmailConstants;

    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient Client;

        public EmailSender()
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
            this.Client = new SendGridClient(apiKey);
        }

        private async Task<Response> Send(string email, string name, string subject, string plainTextContent, string htmlContent)
        {
            var from = new EmailAddress(SenderEmail, "Chef's Kiss admin team");
            var to = new EmailAddress(email, name);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await this.Client.SendEmailAsync(msg);
            return response;
        }

        public async Task Welcome(string email, string name)
        {
            var subject = "Welcome!";
            var plainTextContent = "Welcome to Chef's Kiss - the best place to share your recipes!";
            var htmlContent = $"<h2>{plainTextContent}</h2>";

            await this.Send(email, name, subject, plainTextContent, htmlContent);
        }

        public async Task AuthorApproved(string email, string name)
        {
            var subject = "Application approved";
            var plainTextContent = "Your application for a recipe author position has been approved! Congratulations!";
            var htmlContent = $"<h2>{plainTextContent}</h2>";

            await this.Send(email, name, subject, plainTextContent, htmlContent);
        }

        public async Task AuthorDenied(string email, string name)
        {
            var subject = "Application denied";
            var plainTextContent = "Unfortunately, your application for a recipe author position has been denied. You can try again in the future.";
            var htmlContent = $"<h2>{plainTextContent}</h2>";

            await this.Send(email, name, subject, plainTextContent, htmlContent);
        }

        public async Task AuthorApplied(string email, string name)
        {
            var subject = "Application received";
            var plainTextContent = "Your application for recipe author has been received.";
            var htmlContent = $"<h2>{plainTextContent}</h2>";

            await this.Send(email, name, subject, plainTextContent, htmlContent);
        }
    }
}
