using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PaginationRequest
    {
        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 6;

        public string? Sort { get; set; } = "";

        public string? Query { get; set; } = "";

        public string[]? Filters { get; set; }
    }
}
