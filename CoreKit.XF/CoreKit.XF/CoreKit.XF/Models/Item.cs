using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKit.XF.Models
{
    [Table("Item")]
    public class Item
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Text")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}