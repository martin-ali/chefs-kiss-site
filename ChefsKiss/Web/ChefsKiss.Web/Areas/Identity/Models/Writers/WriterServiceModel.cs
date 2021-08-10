namespace ChefsKiss.Web.Areas.Identity.Models.Writers
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class WriterServiceModel : IMapFrom<Writer>
    {
        public string Username { get; init; }

        public string UserId { get; init; }

        public int WriterId { get; init; }
    }
}
