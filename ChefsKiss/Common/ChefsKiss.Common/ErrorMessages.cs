namespace ChefsKiss.Common
{
    public class ErrorMessages
    {
        public const string NoIngredients = "Ingredients must be present.";

        public const string EmptyCollection = "Collection cannot be empty.";

        public static string LengthBetween(int minLength, int maxLength, string parameterName) => $"{parameterName} must be between {minLength} and {maxLength} characters long.";

        public static string LengthMin(int minLength, string parameterName) => $"{parameterName} cannot be less than {minLength} characters long.";

        public static string LengthMax(int maxLength, string parameterName) => $"{parameterName} cannot be more than {maxLength} characters long.";
    }
}
