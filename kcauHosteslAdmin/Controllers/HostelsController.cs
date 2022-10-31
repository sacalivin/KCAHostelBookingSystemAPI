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
using kcauHosteslAdmin.Models;

namespace kcauHosteslAdmin.Controllers
{
    public class HostelsController : Controller
    {
        private readonly IRequestsService<Hostel> _requestsService;

        public HostelsController(IRequestsService<Hostel> requestsService)
        {
            _requestsService = requestsService;
        }


        // GET: Hostels
        public async Task<IActionResult> Index()
        {
            return await _requestsService.GetAll() != null ?
                        View(await _requestsService.GetAll()) :
                        Problem("Entity set 'ApplicationDbContext.Hostels'  is null.");
        }

        // GET: Hostels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var hostel = await _requestsService.Get(id);

            if (hostel == null)
            {
                return NotFound();
            }

            return View(hostel);
        }

        // GET: Hostels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hostels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]HostelFormViewModel hostel)
        {

            
            try
            {
                if(hostel.Image.Length > 0)
                {
                   //create file in folder 
                }
            }
            catch (Exception e)
            {

                return NotFound();
            }

            if (await _requestsService.Add(hostel))
            {

                return RedirectToAction(nameof(Index));
            }

            return View(hostel);
        }

        // GET: Hostels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var hostel = await _requestsService.Get(id);
            if (hostel == null)
            {
                return NotFound();
            }
            return View(hostel);
        }

        // POST: Hostels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Hostel hostel)
        {
            if (id != hostel.Id)
            {
                return NotFound();
            }

         
                try
                {
                    await _requestsService.Update(id, hostel);
                return RedirectToAction(nameof(Index));
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HostelExists(hostel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
         
            return View(hostel);
        }

        // GET: Hostels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var hostel = await _requestsService.Get(id);

            if (hostel == null)
            {
                return NotFound();
            }

            return View(hostel);
        }

        // POST: Hostels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_requestsService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hostels'  is null.");
            }
            var hostel = await _requestsService.Get(id);
            if (hostel != null)
            {
                await _requestsService.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> HostelExists(int id)
        {
            return (await _requestsService.Get(id) == null ? false : true);
        }
    }
}
