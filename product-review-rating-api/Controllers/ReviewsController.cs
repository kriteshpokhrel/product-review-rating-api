using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_review_rating_api.Data;
using product_review_rating_api.Helpers;
using product_review_rating_api.Models;
using System.Security.Claims;

namespace product_review_rating_api.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;
        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? category)
        {
            var reviews = _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                reviews = reviews.Where(r => r.Product.Category.ToLower().Equals(category.ToLower()));

            ViewData["SelectedCategory"] = category;

            return View(reviews.ToList());
        }


        [HttpPost]
        public IActionResult Create(Review review)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return View(review);

            ModelState.Remove("UserId");
            review.UserId = userId;

            if (review.Rating < 0 || review.Rating > 5)
                ModelState.AddModelError("Rating", "Rating must be between 0 and 5.");

            if (!ModelState.IsValid)
                return View(review);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            // Update Product category
            var product = _context.Products
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == review.ProductId);

            if (product != null)
            {
                product.UpdateCategory();
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Export(string? category)
        {
            var reviews = _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                reviews = reviews.Where(r => r.Product.Category.ToLower().Equals(category));

            var csvData = CsvExportHelper.Export(reviews.Select(r => new
            {
                ProductCode = r.Product.Code,
                ProductName = r.Product.Name,
                UserEmail = r.User.Email,
                Rating = r.Rating,
                Category = ReviewHelper.GetCategory(r.Rating),
                Comment = r.Comment ?? ""
            }).ToList());

            return File(csvData, "text/csv", "reviews_export.csv");
        }
    }
}
