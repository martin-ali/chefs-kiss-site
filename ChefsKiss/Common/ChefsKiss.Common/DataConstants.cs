namespace ChefsKiss.Common
{
    public class DataConstants
    {
        public class Ingredients
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public class Recipes
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 100;

            public const int ContentMinLength = 500;
            public const int ContentMaxLength = 100000;
        }

        public class RecipeIngredients
        {
            public const double MinQuantity = 0.001;
            public const double MaxQuantity = 1000;
        }

        public class Reviews
        {
            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 500;

            public const int RatingMinValue = 1;
            public const int RatingMaxValue = 5;
        }

        public class MeasurementUnits
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 10;
        }

        public class Images
        {
            public const int MaxSizeBytes = 10 * 1024 * 1024; // 10MB
        }
    }
}
