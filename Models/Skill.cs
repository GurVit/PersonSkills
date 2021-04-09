using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonSkill.Models
{
    public class Skill
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Лажа с ID человека")]
        public long PersonId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Введите навык")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Введите уровень навыка")]
        [Range(1, 10, ErrorMessage = "Уровень навыка должен находиться в диапазоне от 1 до 10")]
        public byte Level { get; set; }
        
        public Person Person { get; set; }
    }
}
