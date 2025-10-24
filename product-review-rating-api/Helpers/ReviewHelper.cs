namespace product_review_rating_api.Helpers
{
    /// <summary>
    /// Helper methods for handling Review logic.
    /// </summary>
    public static class ReviewHelper
    {
        /// <summary>
        /// Returns the category for a given rating.
        /// </summary>
        /// <param name="rating">The rating value (0-5)</param>
        /// <returns>Category as string: Good, Bad, Worst</returns>
        public static string GetCategory(int rating)
        {
            return rating switch
            {
                >= 4 => "Good",
                2 or 3 => "Bad",
                _ => "Worst"
            };
        }
    }
}
