using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HospitalLogService.Model
{
    public class Department
    {      
        [JsonIgnore]
        public int Id { get; set; }        
        public string Name { get; set; }        
        [JsonIgnore]
        public ICollection<Log> Logs { get; set; }

    }
}
