namespace STProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public IEnumerable<Community> Products { get; init; } = new List<Community>();
    }
}