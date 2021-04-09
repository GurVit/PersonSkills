using Microsoft.EntityFrameworkCore;
using PersonSkill.Models;
using System.Collections.Generic;
using System.Linq;

namespace PersonSkill
{
    public class EFPSRepository : IPSRepository
    {
        private readonly PSContext Context;
        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> persons = Context.Persons.Include(q => q.Skills);
            return persons;
        }
        public Person Get(long id)
        {
            return Context.Persons.Find(id);
        }
        public EFPSRepository(PSContext context)
        {
            Context = context;
        }
        public void PostPerson(Person item)
        {
            Context.Persons.Add(item);
            foreach (var skill in item.Skills)
            {
                Context.Skills.Add(skill);
            }
            Context.SaveChanges();
        }

        public void PutPerson(Person updatedPerson)
        {
            Person currentItem = Context.Persons.Include(q => q.Skills).FirstOrDefault(w => w.Id == updatedPerson.Id);
            currentItem.Name = updatedPerson.Name;
            currentItem.DisplayName = updatedPerson.DisplayName;
            foreach (var skill in currentItem.Skills)
            {
                Skill updatedSkill = Context.Skills.FirstOrDefault(q => q.SkillName == skill.SkillName && q.PersonId == skill.PersonId);
                if (updatedSkill == null)
                {
                    Skill newSkill = new Skill() { PersonId = skill.PersonId, SkillName = skill.SkillName, Level = skill.Level, Person = currentItem };
                }
                else
                {
                    updatedSkill.Level = updatedPerson.Skills.FirstOrDefault(q => q.SkillName == skill.SkillName && q.PersonId == skill.PersonId).Level;
                }
            }
            Context.Persons.Update(currentItem);
            Context.SaveChanges();
        }

        public Person Delete(long id)
        {
            Person person = Get(id);

            if (person != null)
            {
                Context.Persons.Remove(person);
                Context.SaveChanges();
            }

            return person;
        }
    }
}
