using product_review_rating_api.Helpers;

namespace product_review_rating_api.Models
{
    /// <summary>
    /// Represents a Product entity.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Unique code of the product.
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets Name of the product.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets Collection of <see cref="Review"/> associated with this product.
        /// </summary>
        public ICollection<Review>? Reviews { get; } = [];

        /// <summary>
        /// Represents the aggregated category of the product based on all reviews.
        /// </summary>
        public string Category { get; set; } = "No Reviews";

        /// <summary>
        /// Updates the product category based on the average rating of its reviews.
        /// </summary>
        public void UpdateCategory()
        {
            if (Reviews == null || !Reviews.Any())
            {
                Category = "No Reviews";
                return;
            }

            var averageRating = Reviews.Average(r => r.Rating);
            Category = ReviewHelper.GetCategory((int)Math.Round(averageRating));
        }
    }
}
