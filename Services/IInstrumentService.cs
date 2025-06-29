using InstrumentKaHealth.Models;

namespace InstrumentKaHealth.Services
{
    public interface IInstrumentService
    {
        Task<List<Instrument>> GetAllInstrumentsAsync();
        Task<List<Instrument>> GetInstrumentsByDepartmentAsync(string department);
        Task<List<Instrument>> GetPendingApprovalsAsync();
        Task<Instrument?> GetInstrumentByIdAsync(int id);
        Task<bool> CreateInstrumentAsync(Instrument instrument);
        Task<bool> UpdateInstrumentAsync(Instrument instrument);
        Task<bool> DeleteInstrumentAsync(int id);
        Task<bool> ApproveInstrumentAsync(int id, string approvedBy);
        Task<List<MaintenanceRecord>> GetMaintenanceRecordsAsync(int instrumentId);
    }
}
