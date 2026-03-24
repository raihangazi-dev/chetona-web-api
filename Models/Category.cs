using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chetona_web_api.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}