using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyTest.Database.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Required]
        [Column("name")]
        public required string Name { get; set; }

        [Required]
        [Column("password_hash")]
        public required string PasswordHash { get; set; }
    }
}
