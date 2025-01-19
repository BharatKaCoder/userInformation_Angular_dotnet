using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICC_Champion_Trophy_2025.Model
{
    public class Players
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        public string Role { get; set; }
        public int Matches { get; set; }
        public int HighScore { get; set; }
        [Required]
        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Teams Teams { get; set; }
        public virtual PlayerDetails PlayerDetails { get; set; }
    }
}
