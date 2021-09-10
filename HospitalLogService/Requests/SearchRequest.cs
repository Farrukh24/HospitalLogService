using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Requests
{
    public class SearchRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Username { get; set; }
    }
}
