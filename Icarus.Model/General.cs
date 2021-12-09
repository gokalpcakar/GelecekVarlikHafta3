using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Model
{
    public class General<T>
    {
        public bool IsSuccess { get; set; }
        public T Entity { get; set; }
        public List<T> List { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
