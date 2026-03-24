using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chetona_web_api.DTOs.Category;
using chetona_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace chetona_web_api.Controller
{
    [ApiController]
    [Route("/api/v1/category")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> Categories = new List<Category>{
            new Category
            {
                Id = Guid.Parse("ca8f3d5d-77dd-40bf-8564-8a33fd01147a"),
                Name = "Story",
                Description = "Story Description",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.Parse("ca8f3d5d-77dd-40bf-8564-8a33fd01147b"),
                Name = "History",
                Description = "History Description",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = Guid.Parse("ca8f3d5d-77dd-40bf-8564-8a33fd01147c"),
                Name = "Motivational",
                Description = "Motivational Description",
                CreatedAt = DateTime.UtcNow
            },
       };

        [HttpGet]
        public IActionResult GetAllCategory([FromQuery] string SearchValue = "")
        {
            // if (!string.IsNullOrEmpty(SearchValue))
            // {
            //     var foundedCategories = Categories.Where(Category => Category.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            //     return Ok(foundedCategories);
            // }

            var allCategories = Categories.Select(Category => new CategoryReadDTO
            {
                Name = Category.Name,
                Description = Category.Description
            }).ToList();

            return Ok(allCategories);
        }

        [HttpPost]
        public IActionResult AddNewCategory([FromBody] CategoryCreateDTO CategoryData)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = CategoryData.Name,
                Description = CategoryData.Description,
                CreatedAt = DateTime.UtcNow
            };

            Categories.Add(newCategory);

            var addedCategory = new CategoryReadDTO
            {
              Name = newCategory.Name,
              Description = newCategory.Description  
            };

            return Created($"/api/v1/category/{newCategory.Id}", addedCategory);
        }

        [HttpPut("{Id:guid}")]
        public IActionResult UpdateCategory(Guid Id ,[FromBody] CategoryUpdateDTO CategoryData)
        {
            var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Id);
            if (foundedCategory == null)
            {
                return NotFound("Category with this Id does not Exist");
            }

            foundedCategory.Name = CategoryData.Name;
            foundedCategory.Description = CategoryData.Description;

            var updatedCategory = new CategoryUpdateDTO
            {
                Name = foundedCategory.Name,
                Description = foundedCategory.Description
            };

            return Ok(updatedCategory);
        }

        [HttpDelete("{Id:guid}")]
        public IActionResult DeleteCategory(Guid Id)
        {
            var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Id);

            if (foundedCategory == null)
            {
                return NotFound("Category with this Id does not exist");
            }

            Categories.Remove(foundedCategory);

            return NoContent();
        }
    }
}