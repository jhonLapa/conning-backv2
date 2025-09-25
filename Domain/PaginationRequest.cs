using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PaginationRequest
    {
        public int? Page { get; set; } = 1;

        public int? Take { get; set; } = 6;

        public string? Sort { get; set; } = "createAt.desc";

        public string[]? Filters { get; set; }
    }
}
