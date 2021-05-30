using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class ClientResponseManager<T>
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T ResponseObject { get; set; }
    }

    public class ClientResponseManager
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
