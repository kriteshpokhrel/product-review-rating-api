using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_review_rating_api.Data;
using product_review_rating_api.Models;

namespace product_review_rating_api.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Reviews)
                .ToListAsync();

            foreach (var product in products)
                product.UpdateCategory();

            return View(products);
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            product.UpdateCategory();
            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create() => View();

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name")] Product product)
        {
            ModelState.Remove("Code");
            if (ModelState.IsValid)
            {
                var maxId = await _context.Products.AnyAsync()
                            ? await _context.Products.MaxAsync(p => p.Id)
                            : 0;
                product.Code = $"PRD-{(maxId + 1):D4}";

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: /Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product); // Returns Edit.cshtml with the product
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Product product)
        {
            if (id != product.Id) return NotFound();

            // Manually set Code from database
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: /Product/HandleSelectedAction
        [HttpPost]
        public IActionResult HandleSelectedAction(string action, List<int> selectedProductIds)
        {
            if (selectedProductIds == null || !selectedProductIds.Any())
                return RedirectToAction("Index");

            return action switch
            {
                "Details" => RedirectToAction("Details", new { id = selectedProductIds.First() }),
                "Edit" => RedirectToAction("Edit", new { id = selectedProductIds.First() }),
                "Delete" => RedirectToAction("Delete", new { id = selectedProductIds.First() }),
                _ => RedirectToAction("Index"),
            };
        }

        // GET: /Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Product/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id);
    }
}
