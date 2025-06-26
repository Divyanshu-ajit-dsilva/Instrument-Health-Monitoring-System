using InstrumentKaHealth.Data;
using InstrumentKaHealth.Models;    
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentKaHealth.Controllers
{
    [Authorize]
    public class InstrumentsController : Controller
    {
        private readonly IInstrumentService _instrumentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstrumentsController(IInstrumentService instrumentService,
            UserManager<ApplicationUser> userManager)
        {
            _instrumentService = instrumentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Instrument> instruments;

            if (User.IsInRole("SuperUser"))
            {
                instruments = await _instrumentService.GetAllInstrumentsAsync();
            }
            else
            {
                instruments = await _instrumentService.GetInstrumentsByDepartmentAsync(user.Department);
            }

            ViewBag.UserRole = await GetUserRole();
            return View(instruments);
        }

        [Authorize(Roles = "SuperUser,DataEntry")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperUser,DataEntry")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                instrument.CreatedBy = user.FullName;
                instrument.CreatedDate = DateTime.Now;
                instrument.Department = user.Department;

                if (User.IsInRole("SuperUser"))
                {
                    instrument.IsApproved = true;
                    instrument.ApprovedBy = user.FullName;
                    instrument.ApprovedDate = DateTime.Now;
                }

                var success = await _instrumentService.CreateInstrumentAsync(instrument);
                if (success)
                {
                    TempData["Success"] = "Instrument created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to create instrument.";
                }
            }

            return View(instrument);
        }

        public async Task<IActionResult> Details(int id)
        {
            var instrument = await _instrumentService.GetInstrumentByIdAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }

            ViewBag.UserRole = await GetUserRole();
            return View(instrument);
        }

        [Authorize(Roles = "SuperUser")]
        public async Task<IActionResult> Edit(int id)
        {
            var instrument = await _instrumentService.GetInstrumentByIdAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        [HttpPost]
        [Authorize(Roles = "SuperUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instrument instrument)
        {
            if (id != instrument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                instrument.LastModifiedBy = user.FullName;
                instrument.LastModifiedDate = DateTime.Now;

                var success = await _instrumentService.UpdateInstrumentAsync(instrument);
                if (success)
                {
                    TempData["Success"] = "Instrument updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to update instrument.";
                }
            }

            return View(instrument);
        }

        [Authorize(Roles = "SuperUser")]
        public async Task<IActionResult> PendingApprovals()
        {
            var pendingInstruments = await _instrumentService.GetPendingApprovalsAsync();
            return View(pendingInstruments);
        }

        [HttpPost]
        [Authorize(Roles = "SuperUser")]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _instrumentService.ApproveInstrumentAsync(id, user.FullName);

            if (success)
            {
                TempData["Success"] = "Instrument approved successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to approve instrument.";
            }

            return RedirectToAction(nameof(PendingApprovals));
        }

        [HttpPost]
        [Authorize(Roles = "SuperUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _instrumentService.DeleteInstrumentAsync(id);
            if (success)
            {
                TempData["Success"] = "Instrument deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete instrument.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetUserRole()
        {
            if (User.IsInRole("SuperUser")) return "SuperUser";
            if (User.IsInRole("DataEntry")) return "DataEntry";
            if (User.IsInRole("ViewOnly")) return "ViewOnly";
            return "Unknown";
        }
    }
}
