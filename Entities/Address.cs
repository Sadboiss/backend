using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities
{
    public class Address : Entity
    {
        public string StreetName { get; set; }
        public string CivicNumber { get; set; }
        public string ZipCode { get; set; }
    }
}