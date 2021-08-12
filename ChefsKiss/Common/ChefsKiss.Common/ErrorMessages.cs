namespace ChefsKiss.Common
{
    using static DataConstants;

    public class ErrorMessages
    {
        public const string NoIngredients = "Ingredients must be present.";

        public const string NotAuthorized = "You are not authorized to access this resource.";

        public const string MustBeWriter = "Only writers are able to access this page.";

        public const string MustBeAdmin = "Only admins are able to access this page.";

        public const string AlreadyAppliedForWriter = "You have already submitted a writer application.";

        public static string LengthBetween(int minLength, int maxLength, string parameterName) => $"{parameterName} must be between {minLength} and {maxLength} characters long.";

        public static string LengthMin(int minLength, string parameterName) => $"{parameterName} cannot be less than {minLength} characters long.";

        public static string LengthMax(int maxLength, string parameterName) => $"{parameterName} cannot be more than {maxLength} characters long.";
    }
}
