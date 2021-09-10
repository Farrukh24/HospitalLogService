using HospitalLogService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HospitalLogService.Model
{
    public class Visitor
    {
        public Visitor()
        {
           
        }

        public int Id { get; set; }        
        public string FullName { get; set; }      
        public Gender Gender { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [JsonIgnore]
        public ICollection<Log> Logs { get; set; }       
        public VisitorType Type { get; set; }
        

    }
}
