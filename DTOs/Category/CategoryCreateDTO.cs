using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chetona_web_api.DTOs.Category
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [ Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }
}