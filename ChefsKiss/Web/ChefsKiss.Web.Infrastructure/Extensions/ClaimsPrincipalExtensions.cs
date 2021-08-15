namespace ChefsKiss.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static ChefsKiss.Common.WebConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user) => user.IsInRole(AdministratorRoleName);

        public static bool IsAuthor(this ClaimsPrincipal user) => user.IsInRole(AuthorRoleName);
    }
}
