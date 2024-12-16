using System.ComponentModel.DataAnnotations;

namespace ProductInformation_CRUD_Operations.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }= string.Empty;
    }
}
