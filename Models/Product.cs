using System.ComponentModel.DataAnnotations;

namespace ProductInformation_CRUD_Operations.Models
{
    public class Product
    {
        [Key]
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get;set; }



       // public Category Category { get; set; }
    }
}
