namespace Domain;
public class BaseDomain {
    public int? AuditCreateUser { get; set; } 
    public DateTime? AuditCreateDate { get; set; } 
    public int? AuditUpdateUser { get; set; }
    public DateTime? AuditUpdateDate { get; set; }
    public int? AuditDeleteUser { get; set; }
    public DateTime? AuditDeleteDate { get; set; }
}