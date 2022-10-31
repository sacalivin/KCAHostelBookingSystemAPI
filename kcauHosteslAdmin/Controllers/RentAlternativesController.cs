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
    public class RentAlternativesController : Controller
    {
        private readonly IRequestsService<RentAlternative> _requestsService;

        public RentAlternativesController(IRequestsService<RentAlternative> requestsService)
        {
            _requestsService = requestsService;
        }


        // GET: RentAlternatives
        public async Task<IActionResult> Index()
        {
            return await _requestsService.GetAll() != null ?
                        View(await _requestsService.GetAll()) :
                        Problem("Entity set 'ApplicationDbContext.RentAlternatives'  is null.");
        }

        // GET: RentAlternatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var rentAlternative = await _requestsService.Get(id);

            if (rentAlternative == null)
            {
                return NotFound();
            }

            return View(rentAlternative);
        }

        // GET: RentAlternatives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RentAlternatives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentAlternative rentAlternative)
        {
            if (await _requestsService.Add(rentAlternative))
            {

                return RedirectToAction(nameof(Index));
            }

            return View(rentAlternative);
        }

        // GET: RentAlternatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var rentAlternative = await _requestsService.Get(id);
            if (rentAlternative == null)
            {
                return NotFound();
            }
            return View(rentAlternative);
        }

        // POST: RentAlternatives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  RentAlternative rentAlternative)
        {
            if (id != rentAlternative.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _requestsService.Update(id, rentAlternative);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RentAlternativeExists(rentAlternative.Id))
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
            return View(rentAlternative);
        }

        // GET: RentAlternatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var rentAlternative = await _requestsService.Get(id);

            if (rentAlternative == null)
            {
                return NotFound();
            }

            return View(rentAlternative);
        }

        // POST: RentAlternatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_requestsService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RentAlternatives'  is null.");
            }
            var rentAlternative = await _requestsService.Get(id);
            if (rentAlternative != null)
            {
                await _requestsService.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RentAlternativeExists(int id)
        {
            return (await _requestsService.Get(id) == null ? false : true);
        }

    }
}
