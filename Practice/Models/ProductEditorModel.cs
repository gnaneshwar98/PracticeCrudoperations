using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Practice.Models
{
    public class ProductEditorModel
    {
        [Required(ErrorMessage = "Please enter name for the product")]
        [StringLength(50)]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is requred")]
        public decimal Price { get; set; }

        [HiddenInput]
        public int? ProductId { get; set; }
    }
}
