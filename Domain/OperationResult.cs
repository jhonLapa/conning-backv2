using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OperationResult<T>
    {

        public string? Message { get; set; }
        public bool? State { get; set; }
        public T? Data { get; set; }
    }
}
