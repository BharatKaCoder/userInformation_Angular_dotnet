using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ICC_Champion_Trophy_2025.Model
{
    public class Teams
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Flag { get; set; }
        public int Champions { get; set; }
        public string Captain {  get; set; }
        public virtual ICollection<Players> Players { get; set; }
    }
}
