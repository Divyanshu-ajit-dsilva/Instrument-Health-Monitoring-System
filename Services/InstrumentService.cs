using InstrumentKaHealth.Data;
using InstrumentKaHealth.Models;
using Microsoft.EntityFrameworkCore;
namespace InstrumentKaHealth.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly ApplicationDbContext _context;

        public InstrumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Instrument>> GetAllInstrumentsAsync()
        {
            return await _context.Instruments
                .Include(i => i.MaintenanceRecords)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<List<Instrument>> GetInstrumentsByDepartmentAsync(string department)
        {
            return await _context.Instruments
                .Where(i => i.Department == department)
                .Include(i => i.MaintenanceRecords)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<List<Instrument>> GetPendingApprovalsAsync()
        {
            return await _context.Instruments
                .Where(i => !i.IsApproved)
                .OrderBy(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Instrument?> GetInstrumentByIdAsync(int id)
        {
            return await _context.Instruments
                .Include(i => i.MaintenanceRecords)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> CreateInstrumentAsync(Instrument instrument)
        {
            try
            {
                _context.Instruments.Add(instrument);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateInstrumentAsync(Instrument instrument)
        {
            try
            {
                _context.Instruments.Update(instrument);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteInstrumentAsync(int id)
        {
            try
            {
                var instrument = await _context.Instruments.FindAsync(id);
                if (instrument != null)
                {
                    _context.Instruments.Remove(instrument);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ApproveInstrumentAsync(int id, string approvedBy)
        {
            try
            {
                var instrument = await _context.Instruments.FindAsync(id);
                if (instrument != null)
                {
                    instrument.IsApproved = true;
                    instrument.ApprovedBy = approvedBy;
                    instrument.ApprovedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MaintenanceRecord>> GetMaintenanceRecordsAsync(int instrumentId)
        {
            return await _context.MaintenanceRecords
                .Where(mr => mr.InstrumentId == instrumentId)
                .OrderByDescending(mr => mr.MaintenanceDate)
                .ToListAsync();
        }
    }
}
