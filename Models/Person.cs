using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonSkill.Models
{
    public class Person
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string name { get; set; }

        [Required(ErrorMessage = "Введите отображаемое имя")]
        public string displayName { get; set; }

        [Required(ErrorMessage ="Неправильно введены навыки")]
        public virtual ICollection<Skill> skills { get; set; }
    }
}
