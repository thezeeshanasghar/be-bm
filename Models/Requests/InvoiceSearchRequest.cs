using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet.Models
{
    public class InvoiceSearchRequest
    {
        public string Search { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
