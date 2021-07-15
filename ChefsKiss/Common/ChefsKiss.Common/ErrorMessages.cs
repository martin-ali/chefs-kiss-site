namespace ChefsKiss.Common
{
    using static DataConstants;

    public class ErrorMessages
    {
        public const string NoIngredients = "Ingredients must be present.";

        // FIXME: Coupling. Move validation from attribute to controller maybe?
        public const string ImageOverMaxSize = "Image size if over allowed maximum size of 10MB. Please choose a smaller image";

        public static string LengthBetween(int minLength, int maxLength, string parameterName) => $"{parameterName} must be between {minLength} and {maxLength} characters long.";

        public static string LengthMin(int minLength, string parameterName) => $"{parameterName} cannot be less than {minLength} characters long.";

        public static string LengthMax(int maxLength, string parameterName) => $"{parameterName} cannot be more than {maxLength} characters long.";
    }
}
