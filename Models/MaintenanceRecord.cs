using System.ComponentModel.DataAnnotations;

namespace InstrumentKaHealth.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }

        public int InstrumentId { get; set; }
        public virtual Instrument? Instrument { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(100)]
        public string MaintenanceType { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PerformedBy { get; set; } = string.Empty;

        public decimal Cost { get; set; }

        [Required]
        public string StatusAfterMaintenance { get; set; } = "Operational";

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public bool IsApproved { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
