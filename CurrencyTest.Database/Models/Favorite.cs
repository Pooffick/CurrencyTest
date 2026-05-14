using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyTest.Database.Models
{
    [Table("favorites")]
    public class Favorite
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public required string UserId { get; set; }

        [ForeignKey(nameof(Currency))]
        [Column("currency_id")]
        public required string CurrencyId { get; set; }

        public User User { get; set; } = null!;
        public Currency Currency { get; set; } = null!;
    }
}
