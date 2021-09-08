using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Model
{
    public class Department
    {      

        public int Id { get; set; }        
        public string Name { get; set; }
                
        public ICollection<Log> Logs { get; set; }

    }
}
