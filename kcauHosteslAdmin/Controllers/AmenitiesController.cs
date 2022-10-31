using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL_CRUD.Data;
using DAL_CRUD.Models;
using kcauHosteslAdmin.Services;

namespace kcauHosteslAdmin.Controllers
{
    public class AmenitiesController : Controller
    {
        
        private readonly IRequestsService<Armenity> _requestsService;
      
        public AmenitiesController( IRequestsService<Armenity> requestsService)
        {
            _requestsService = requestsService;
        }

        // GET: Armenities
        public async Task<IActionResult> Index()
        {
              return await _requestsService.GetAll() != null ? 
                          View(await _requestsService.GetAll()) :
                          Problem("Entity set 'ApplicationDbContext.Armenities'  is null.");
        }

        // GET: Armenities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var armenity = await _requestsService.Get(id);
                
            if (armenity == null)
            {
                return NotFound();
            }

            return View(armenity);
        }

        // GET: Armenities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armenities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IconUrl")] Armenity armenity)
        {
            if (await _requestsService.Add(armenity))
            {
                
                return RedirectToAction(nameof(Index));
            }
           
            return View(armenity);
        }

        // GET: Armenities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var armenity = await _requestsService.Get(id);
            if (armenity == null)
            {
                return NotFound();
            }
            return View(armenity);
        }

        // POST: Armenities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IconUrl")] Armenity armenity)
        {
            if (id != armenity.Id)
            {
                return NotFound();
            }

           
                try
                {
                    await _requestsService.Update(id, armenity);
                return RedirectToAction(nameof(Index));
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArmenityExists(armenity.Id))
                    {
                        return NotFound();
                    }
                   
                }
                
           
            return View(armenity);
        }

        // GET: Armenities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var armenity = await _requestsService.Get(id);
                
            if (armenity == null)
            {
                return NotFound();
            }

            return View(armenity);
        }

        // POST: Armenities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_requestsService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Armenities'  is null.");
            }
            var armenity = await _requestsService.Get(id);
            if (armenity != null)
            {
               await _requestsService.Delete(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private async Task< bool> ArmenityExists(int id)
        {
          return ( await _requestsService.Get(id) == null ? false : true) ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_requestsService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
