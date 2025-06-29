using System.ComponentModel.DataAnnotations;
namespace InstrumentKaHealth.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Operational";

        public DateTime LastMaintenanceDate { get; set; } = DateTime.Now;

        public DateTime NextMaintenanceDate { get; set; } = DateTime.Now.AddMonths(6);

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public bool IsApproved { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
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
