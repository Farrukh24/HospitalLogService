using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Model
{
    public class Log
    {
        public Log()
        {
           
        }

        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedOn { get; set; }       
        public string Purpose { get; set; }
        public int DepartmentId { get; set; }
        public int VisitorId { get; set; }
        public Department Department { get; set; }
        public Visitor Visitor { get; set; }

    }
}
