using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Banck : BaseDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char CountryCode { get; set; }
        public string SwiftCode { get; set; }
        public string ShortName { get; set; }
        public bool State { get; set; }

    }
}
