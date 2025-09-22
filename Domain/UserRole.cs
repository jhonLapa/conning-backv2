using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserRole : BaseDomain
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool State { get; set; }
        public virtual User? Users { get; set; }
        public virtual Rol? Roles { get; set; }
    }
}
