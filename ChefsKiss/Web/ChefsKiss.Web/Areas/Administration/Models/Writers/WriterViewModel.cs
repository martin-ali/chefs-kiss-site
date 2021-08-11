namespace ChefsKiss.Web.Areas.Administration.Models.Writers
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class WriterViewModel : IMapFrom<Writer>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string Username { get; init; }

        public string FullName { get; init; }

        public string UserId { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Writer, WriterViewModel>()
                .ForMember(vm => vm.Username, cgf => cgf.MapFrom(m => m.User.UserName))
                .ForMember(vm => vm.FullName, cgf => cgf.MapFrom(m => $"{m.FirstName} {m.LastName}"));
        }
    }
}
