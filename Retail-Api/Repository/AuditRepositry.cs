namespace Retail_Api.Repository
{
    public class AuditRepositry
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Product> _logger;

        public AuditRepositry(ApplicationDbContext context, ILogger<Product> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Add(AuditTrail auditTrail)
        {
             _context.AuditTrail.Add(auditTrail);
        }
        public void save()
        {
             _context.SaveChanges();
        }
    }
}
