using System.ComponentModel.DataAnnotations;

namespace ICC_Champion_Trophy_2025.Model.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string Role { get; set; }
        public int Matches { get; set; }
        public int HighestScore { get; set; }
        public int Wickets { get; set; }
        public int TeamId { get; set; }
    }
}
