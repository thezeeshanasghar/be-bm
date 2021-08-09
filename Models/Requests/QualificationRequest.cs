using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class QualificationRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Certificate { get; set; }
        public string Description { get; set; }
        public string QualificationType { get; set; }
    }

}
