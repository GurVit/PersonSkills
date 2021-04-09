using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonSkill.Models
{
    public class Person
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите отображаемое имя")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage ="Неправильно введены навыки")]
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
