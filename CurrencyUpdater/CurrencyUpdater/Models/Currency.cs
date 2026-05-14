using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyUpdater.Models
{
    [Table("currency")]
    public class Currency
    {
        [Key]
        [Column("id")]
        public required string Id { get; set; }

        [Required]
        [Column("name")]
        public required string Name { get; set; }

        [Column("rate")]
        public decimal Rate { get; set; }
    }
}
