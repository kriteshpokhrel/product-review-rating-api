using Microsoft.AspNetCore.Identity;

namespace product_review_rating_api.Models
{
    /// <summary>
    /// Represents Review of a <see cref="Models.Product"/>
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Represents id of the Review.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Presents Id of the Product associated.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Navigation property to the related Product entity.
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// Represents ID of the user who submitted the review.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property to the user who submitted the review.
        /// </summary>

        public IdentityUser? User { get; set; }

        /// <summary>
        /// Gets or sets the rating provided. (0–5).
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Optional text comment provided by the user.
        /// </summary>
        public string? Comment { get; set; }
    }
}
