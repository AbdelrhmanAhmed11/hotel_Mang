using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Management_System.Models;

namespace Hotel_Management_System.Controllers
{
    public class HotelController : Controller
    {
        private readonly AppDbContext _context;

        public HotelController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.hotel.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel
            .FirstOrDefaultAsync(m => m.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

  
        [HttpPost]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(hotel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id , Hotel hotel)
        {
            if (id != hotel.HotelID)
            {
                return NotFound();
            }

           
                
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                
              
                return RedirectToAction(nameof(Index));
            
            //return View(hotel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotel
                .FirstOrDefaultAsync(m => m.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Hotel hotel)
        {
            var hotell = await _context.hotel.FindAsync(hotel.HotelID);
            if (hotell != null)
            {
                _context.hotel.Remove(hotel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool HotelExists(int id)
        //{
        //    return _context.hotel.Any(e => e.HotelID == id);
        //}
    }
}
