namespace ChefsKiss.Common
{
    public class WebConstants
    {
        public const string SystemName = "ChefsKiss";

        public const string UserRoleName = "User";

        public const string AuthorRoleName = "Author";

        public const string AdministratorRoleName = "Administrator";

        public const string ImagesDirectory = "images";

        public const string AdministrationArea = "Administration";

        public const string IdentityArea = "Identity";

        public const int PopularRecipesCount = 5;

        public const int ItemsPerPage = 6;

        public const int RecipesPerCategory = 5;

        public const int RecipeSummaryLength = 70;

        public const int ReviewSummaryLength = 100;

        public class Cache
        {
            public const string PopularRecipesCacheKey = nameof(PopularRecipesCacheKey);

            public const string CategoriesExploreCacheKey = nameof(CategoriesExploreCacheKey);
        }
    }
}
