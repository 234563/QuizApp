using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp;

namespace QuizApp.Controllers
{
    public class InternetNetworksController : Controller
    {
        private readonly ApplicationDbContext Dbcontext;

        public InternetNetworksController(ApplicationDbContext context)
        {
            Dbcontext = context;
        }

        // GET: InternetNetworks
        public async Task<IActionResult> Index()
        {
            return View(await Dbcontext.InternetNetworks.ToListAsync());
        }

        // GET: InternetNetworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internetNetwork = await Dbcontext.InternetNetworks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internetNetwork == null)
            {
                return NotFound();
            }

            return View(internetNetwork);
        }

        // GET: InternetNetworks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InternetNetworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code1,NetworkName1,Code2,NetworkName2,Code3,NetworkName3,Code4,NetworkName4,Code5,NetworkName5")] InternetNetwork internetNetwork)
        {
            if (ModelState.IsValid)
            {
                Dbcontext.Add(internetNetwork);
                await Dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internetNetwork);
        }

        // GET: InternetNetworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internetNetwork = await Dbcontext.InternetNetworks.FindAsync(id);
            if (internetNetwork == null)
            {
                return NotFound();
            }
            return View(internetNetwork);
        }

        // POST: InternetNetworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code1,NetworkName1,Code2,NetworkName2,Code3,NetworkName3,Code4,NetworkName4,Code5,NetworkName5")] InternetNetwork internetNetwork)
        {
            if (id != internetNetwork.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Dbcontext.Update(internetNetwork);
                    await Dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternetNetworkExists(internetNetwork.ID))
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
            return View(internetNetwork);
        }

        // GET: InternetNetworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internetNetwork = await Dbcontext.InternetNetworks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internetNetwork == null)
            {
                return NotFound();
            }

            return View(internetNetwork);
        }

        // POST: InternetNetworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internetNetwork = await Dbcontext.InternetNetworks.FindAsync(id);
            if (internetNetwork != null)
            {
                Dbcontext.InternetNetworks.Remove(internetNetwork);
            }

            await Dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternetNetworkExists(int id)
        {
            return Dbcontext.InternetNetworks.Any(e => e.ID == id);
        }
    }
}
