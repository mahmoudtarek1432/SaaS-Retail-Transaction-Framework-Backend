using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class WebSocketClientResponse<T>
    {
        public string message { get; set; }

        public int type { get; set; }

        public T Data { get; set; }
    }

    public class WebSocketClientResponse
    {
        public string transactionId { get; set; }

        public string message { get; set; }

        public int type { get; set; }
    }
}
