namespace Dinnerplanner.Models.BigOven
{
    using System.Collections.Generic;

    public class BigOvenRecipe
    {
        public int? RecipeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public object Cuisine { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public object Microcategory { get; set; }
        public string PrimaryIngredient { get; set; }
        public double? StarRating { get; set; }
        public string WebURL { get; set; }
        public string ImageURL { get; set; }
        public int? ReviewCount { get; set; }
        public int? MedalCount { get; set; }
        public int? FavoriteCount { get; set; }
        public Poster Poster { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Instructions { get; set; }
        public int? YieldNumber { get; set; }
        public string YieldUnit { get; set; }
        public int? TotalMinutes { get; set; }
        public int? ActiveMinutes { get; set; }
        public NutritionInfo NutritionInfo { get; set; }
        public object IsPrivate { get; set; }
        public string CreationDate { get; set; }
        public string LastModified { get; set; }
        public bool? IsBookmark { get; set; }
        public object BookmarkURL { get; set; }
        public string BookmarkSiteLogo { get; set; }
        public object BookmarkImageURL { get; set; }
        public object IsRecipeScan { get; set; }
        public int? MenuCount { get; set; }
        public int? NotesCount { get; set; }
        public object AdTags { get; set; }
        public object IngredientsTextBlock { get; set; }
        public string AllCategoriesText { get; set; }
        public bool? IsSponsored { get; set; }
        public object VariantOfRecipeID { get; set; }
        public string Collection { get; set; }
        public object CollectionID { get; set; }
        public int? AdminBoost { get; set; }
        public string VerifiedDateTime { get; set; }
        public int? MaxImageSquare { get; set; }
        public List<int?> ImageSquares { get; set; }
        public string HeroPhotoUrl { get; set; }
        public string VerifiedByClass { get; set; }
    }

    public class Poster
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string ImageURL48 { get; set; }
        public string PhotoUrl { get; set; }
        public bool? IsPremium { get; set; }
        public bool? IsKitchenHelper { get; set; }
        public object PremiumExpiryDate { get; set; }
        public string MemberSince { get; set; }
        public bool? IsUsingRecurly { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
    }

    public class IngredientInfo
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public int? MasterIngredientID { get; set; }
        public bool? UsuallyOnHand { get; set; }
    }

    public class Ingredient
    {
        public int? IngredientID { get; set; }
        public int? DisplayIndex { get; set; }
        public bool? IsHeading { get; set; }
        public string Name { get; set; }
        public string HTMLName { get; set; }
        public double? Quantity { get; set; }
        public string DisplayQuantity { get; set; }
        public string Unit { get; set; }
        public double MetricQuantity { get; set; }
        public string MetricDisplayQuantity { get; set; }
        public string MetricUnit { get; set; }
        public object PreparationNotes { get; set; }
        public IngredientInfo IngredientInfo { get; set; }
        public bool? IsLinked { get; set; }
    }

    public class NutritionInfo
    {
        public string SingularYieldUnit { get; set; }
        public int? TotalCalories { get; set; }
        public double? TotalFat { get; set; }
        public int? CaloriesFromFat { get; set; }
        public double? TotalFatPct { get; set; }
        public double? SatFat { get; set; }
        public double? SatFatPct { get; set; }
        public double? MonoFat { get; set; }
        public double? PolyFat { get; set; }
        public double? TransFat { get; set; }
        public double? Cholesterol { get; set; }
        public double? CholesterolPct { get; set; }
        public double? Sodium { get; set; }
        public double? SodiumPct { get; set; }
        public double? Potassium { get; set; }
        public double? PotassiumPct { get; set; }
        public double? TotalCarbs { get; set; }
        public double? TotalCarbsPct { get; set; }
        public double? DietaryFiber { get; set; }
        public double? DietaryFiberPct { get; set; }
        public double? Sugar { get; set; }
        public double? Protein { get; set; }
        public double? ProteinPct { get; set; }
    }
}
