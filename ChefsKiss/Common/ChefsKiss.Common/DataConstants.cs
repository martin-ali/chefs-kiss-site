namespace ChefsKiss.Common
{
    public class DataConstants
    {
        // Ingredients
        public const int IngredientNameMinLength = 2;
        public const int IngredientNameMaxLength = 50;

        // Recipes
        public const int RecipeTitleMinLength = 5;
        public const int RecipeTitleMaxLength = 100;

        public const int RecipeContentMinLength = 500;
        public const int RecipeContentMaxLength = 100000;

        // RecipeIngredients
        public const double RecipeIngredientMinQuantity = 0.001;
        public const double RecipeIngredientMaxQuantity = 1000;

        // Reviews
        public const int ReviewContentMinLength = 10;
        public const int ReviewContentMaxLength = 1000;

        public const int ReviewRatingMinValue = 1;
        public const int ReviewRatingMaxValue = 5;

        // MeasurementUnits
        public const int MeasurementUnitNameMinLength = 1;
        public const int MeasurementUnitNameMaxLength = 10;

        // Images
        public const int ImageMaxSizeBytes = 10 * 1024 * 1024; // 10MB
    }
}
