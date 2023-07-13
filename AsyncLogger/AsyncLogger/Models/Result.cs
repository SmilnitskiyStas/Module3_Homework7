using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncLogger.Models
{
    internal class Result
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public Result(string message, bool status)
        {
            Message = message;
            Status = status;
        }
    }
}
