namespace ChefsKiss.Web.Areas.Administration.Models.Authors
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class AuthorViewModel : IMapFrom<Author>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string Username { get; init; }

        public string FullName { get; init; }

        public string UserId { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Author, AuthorViewModel>()
                .ForMember(vm => vm.Username, cgf => cgf.MapFrom(m => m.User.UserName))
                .ForMember(vm => vm.FullName, cgf => cgf.MapFrom(m => $"{m.FirstName} {m.LastName}"));
        }
    }
}
