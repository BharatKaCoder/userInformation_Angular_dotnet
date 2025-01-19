using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ICC_Champion_Trophy_2025.Model
{
    public class PlayerDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public virtual Players Player { get; set; }
    }
}
