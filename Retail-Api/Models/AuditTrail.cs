using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Api.Models
{
    public class AuditTrail
    {
        public int AuditTrailID { get; set; }
        public string ProductName { get; set; }
        public string Action { get; set; } // 'Create', 'Update', 'Delete'
        public DateTime ActionDate { get; set; } 
    }
}
