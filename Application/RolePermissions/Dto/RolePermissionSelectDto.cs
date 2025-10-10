namespace Application.RolePermissions.Dto
{
       
        public class RolePermissionSelectDto
        {
            public int RoleId { get; set; }
            public int PermissionId { get; set; }
            public char State { get; set; }

            public DateTime AuditCreateDate { get; set; }
            public string? AuditCreateUser { get; set; }

            public DateTime? AuditUpdateDate { get; set; }
            public string? AuditUpdateUser { get; set; }

            public DateTime? AuditDeleteDate { get; set; }
            public string? AuditDeleteUser { get; set; }

            public string? RoleName { get; set; }
            public string? PermissionName { get; set; }
            public string? PermissionDescription { get; set; } 
    }
}
