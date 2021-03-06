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

        public const string InvalidRequest = "The request is invalid.";

        public const string LengthBetween = "{0} must be between {2} and {1} characters long.";

        public static string LengthBetweenWithParameters(string parameterName, int minLength, int maxLength) => string.Format(LengthBetween, parameterName, maxLength, minLength);

        public static string LengthMin(int minLength, string parameter) => $"{parameter} cannot be less than {minLength} characters long.";

        public static string LengthMax(int maxLength, string parameter) => $"{parameter} cannot be more than {maxLength} characters long.";

        public static string InvalidParameter(string parameter) => $"This {parameter} is invalid.";
    }
}
