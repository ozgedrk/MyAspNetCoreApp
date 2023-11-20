using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModel
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name could be max 50 character.")]
        [Required(ErrorMessage = "Namespace must be filled.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Price must be filled.")]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        public decimal? Price { get; set; }


        [Required(ErrorMessage = "Stock must be filled.")]
        [Range(1, 200, ErrorMessage = "Stock must be between 1 and 200.")]
        public int? Stock { get; set; }


        [Required(ErrorMessage = "Color must be filled.")]
        public string? Color { get; set; }


        [StringLength(300, MinimumLength = 50, ErrorMessage = "Description should be between 50 and 300 character.")]
        [Required(ErrorMessage = "Description must be filled.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "PublishDate must be filled.")]
        public DateTime? PublishDate { get; set; }


        [Required(ErrorMessage = "Expire Date must be filled.")]
        public int? Expire { get; set; }


        public bool IsPublish { get; set; }
        public bool IsDeleted { get; set; }

        //[Required(ErrorMessage = "Image field is required.")]
        //public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Category must be filled.")]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
