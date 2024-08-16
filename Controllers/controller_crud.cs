using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impacta_Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Ecommerce _context;

        public ProductsController(Ecommerce context)
        {
            _context = context;
        }

        // Create 
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Read 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // Update 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Delete 
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
