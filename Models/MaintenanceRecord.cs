using System.ComponentModel.DataAnnotations;

namespace InstrumentKaHealth.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }

        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(100)]
        public string MaintenanceType { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string PerformedBy { get; set; }

        public decimal Cost { get; set; }

        public HealthStatus StatusAfterMaintenance { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
