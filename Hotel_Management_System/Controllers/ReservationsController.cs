using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Management_System.Models;
using Hotel_Management_System.ViewModels;

namespace Hotel_Management_System.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.reservation.Include(r => r.customer).Include(r => r.room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.reservation
                .Include(r => r.customer)
                .Include(r => r.room)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task <IActionResult> Create()
        {
            var avai = await _context.room.Where(x => x.IsAvailable == true).ToListAsync();
            var custo = await _context.customer.ToListAsync();

            var rsvm = new ReservationViewModel()
            {
                availablerooms = avai,
                customers = custo,
                ReservationTotalPrice = 0
            };

            return View(rsvm);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(ReservationViewModel rvm)
        {
            var ppn = _context.room.Where(x => x.RoomID == rvm.RoomID).Select(x => x.PricePerNight).FirstOrDefault();

            var room = await _context.room.FindAsync(rvm.RoomID);
            var numberofnights = (rvm.CheckOutDate) - (rvm.CheckInDate);
            var nmb = numberofnights.TotalDays;


            var total = rvm.ReservationTotalPrice = nmb * ppn;

            var reservation = new Reservation
            {
                RoomID = rvm.RoomID,
                CustomerID = rvm.CustomerID,
                CheckInDate = rvm.CheckInDate,
                CheckOutDate = rvm.CheckOutDate,
                ReservationTotalPrice = total
            };

            await _context.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            //ViewData["CustomerID"] = new SelectList(_context.customer, "CustomerID", "CustomerID", reservation.CustomerID);
            //ViewData["RoomID"] = new SelectList(_context.room, "RoomID", "RoomID", reservation.RoomID);
            //return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.customer, "CustomerID", "CustomerID", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.room, "RoomID", "RoomID", reservation.RoomID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,RoomID,CustomerID,CheckInDate,CheckOutDate,ReservationTotalPrice")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationID))
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
            ViewData["CustomerID"] = new SelectList(_context.customer, "CustomerID", "CustomerID", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.room, "RoomID", "RoomID", reservation.RoomID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.reservation
                .Include(r => r.customer)
                .Include(r => r.room)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.reservation.Any(e => e.ReservationID == id);
        }
    }
}
