using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Цена")]
        public long Price{ get; set; }
        
        public int Count { get; set; }
        public long TotalHours { get; set; }
        public bool CrossPlatformMultiplayer { get; set; }
        public Perspective Perspective { get; set; }
        public Movie BasedOnGameMovie { get; set; }
    }
}