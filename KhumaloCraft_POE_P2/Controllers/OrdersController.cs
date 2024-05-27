using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft_POE_P2.Data;
using KhumaloCraft_POE_P2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using KhumaloCraft_POE_P2.ViewModels;

namespace KhumaloCraft_POE_P2.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly KhumaloCraftDbContext _context;

        public OrdersController(KhumaloCraftDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Orders
       
        public async Task<IActionResult> Index()
        {
            //var khumaloCraftDbContext = _context.Order.Include(o => o.Product).Include(o => o.User);
            //return View(await khumaloCraftDbContext.ToListAsync());

            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var khumaloCraftDbContext = _context.Order.Include(o => o.Product).Include(o => o.User);
                return View(await khumaloCraftDbContext.ToListAsync());
            }
            else if (await _userManager.IsInRoleAsync(user, "Client"))
            {
                // Retrieve orders created by the current customer
                var orders = _context.Order.Where(o => o.UserId == user.Id).Include(o => o.Product).Include(o => o.User).ToList();
                return View(orders);
            }
            else
            {
                return Unauthorized();
            }

        }

       
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,ProductName,UserId,UserName,CreditCardName,CreditCardNumber,CreditCardExpDate,CreditCardCCVcode,ShippingAddress")] Order order)
        {
            if (!ModelState.IsValid)
            {

                order.Date = DateTime.Now;
                order.ModifiedDate = DateTime.Now;

                _context.Add(order);
                await _context.SaveChangesAsync();

                var history = new OrderHistory
                {
                    OrderId = order.OrderId,
                    ChangeDate = DateTime.Now,
                };
                _context.OrderHistories.Add(history);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", order.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", order.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UserId,CreditCardName,CreditCardNumber,CreditCardExpDate,CreditCardCCVcode,ShippingAddress")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    order.ModifiedDate = DateTime.Now;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                        
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", order.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> OrderHistory(OrderHistoryViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the current user is an Admin
            var isAdmin = User.IsInRole("Admin");

            var query = _context.Order.Include(e => e.Product).Include(e => e.User).AsQueryable();

            if (isAdmin)
            {
                // If the user is an Admin, allow access to all orders
                query = _context.Order.Include(e => e.Product).Include(e => e.User);
            }
            else
            {
                // If the user is not an Admin, restrict access to their own orders
                query = _context.Order.Include(e => e.Product).Include(e => e.User)
                                       .Where(e => e.User.Id == userId);
            }

            if (!string.IsNullOrEmpty(model.FilterProductName))
                {
                    query = query.Where(e => e.Product.ProductName.Contains(model.FilterProductName));
                }

                if (!string.IsNullOrEmpty(model.FilterUserName))
                {
                    query = query.Where(e => e.User.UserName.Contains(model.FilterUserName));
                }

                if (model.FilterOrderDate.HasValue)
                {
                    query = query.Where(e => e.Date >= model.FilterOrderDate.Value);
                }

                model.Orders = await query.OrderByDescending(e => e.ModifiedDate).ToListAsync();

                return View(model);
        }

//        var user = await _userManager.GetUserAsync(User);

//            if (await _userManager.IsInRoleAsync(user, "Admin"))
//            {
//                var khumaloCraftDbContext = _context.Order.Include(o => o.Product).Include(o => o.User);
//                return View(await khumaloCraftDbContext.ToListAsync());
//    }
//            else if (await _userManager.IsInRoleAsync(user, "Client"))
//            {
//                // Retrieve orders created by the current customer
//                var orders = _context.Order.Where(o => o.UserId == user.Id).Include(o => o.Product).Include(o => o.User).ToList();
//                return View(orders);
//}
//            else
//{
//    return Unauthorized();
//}

private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
