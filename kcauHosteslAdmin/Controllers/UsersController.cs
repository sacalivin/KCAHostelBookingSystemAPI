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
    public class UsersController : Controller
    {
        
        private readonly IRequestsService<User> _requestsService;

        public UsersController(IRequestsService<User> requestService)
        {
            _requestsService = requestService;
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            return await _requestsService.GetAll() != null ?
                        View(await _requestsService.GetAll()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var user = await _requestsService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( User user)
        {
            if (await _requestsService.Add(user))
            {

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var user = await _requestsService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _requestsService.Update(id, user);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _requestsService.GetAll() == null)
            {
                return NotFound();
            }

            var user = await _requestsService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_requestsService.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var user = await _requestsService.Get(id);
            if (user != null)
            {
                await _requestsService.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            return (await _requestsService.Get(id) == null ? false : true);
        }
    }
}
