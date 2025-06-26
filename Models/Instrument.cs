using System.ComponentModel.DataAnnotations;
namespace InstrumentKaHealth.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InstrumentCode { get; set; }

        [Required]
        [StringLength(200)]
        public string InstrumentName { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [StringLength(100)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(50)]
        public string SerialNumber { get; set; }

        public DateTime InstallationDate { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceDate { get; set; }

        [Required]
        public HealthStatus Status { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public List<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
    }

    public enum HealthStatus
    {
        Excellent = 1,
        Good = 2,
        Fair = 3,
        Poor = 4,
        Critical = 5,
        OutOfService = 6
    }
}
