namespace ChefsKiss.Common
{
    public class ErrorMessages
    {
        public const string NoIngredients = "Ingredients must be present.";

        public const string NotAuthorized = "You are not authorized to access this resource.";

        public const string MustBeAuthor = "Only authors are able to access this page.";

        public const string AuthorsCantReviewOwnRecipes = "Authors cannot review their own recipe.";

        public const string AlreadyAppliedForAuthor = "You have already submitted an author application.";

        public const string MustBeAdmin = "Only admins are able to access this page.";

        public const string InvalidSearchTerm = "The search term is invalid.";

        public static string LengthBetween(int minLength, int maxLength, string parameterName) => $"{parameterName} must be between {minLength} and {maxLength} characters long.";

        public static string LengthMin(int minLength, string parameterName) => $"{parameterName} cannot be less than {minLength} characters long.";

        public static string LengthMax(int maxLength, string parameterName) => $"{parameterName} cannot be more than {maxLength} characters long.";
    }
}
