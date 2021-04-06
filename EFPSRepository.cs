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
            IEnumerable<Person> persons = Context.Persons.Include(q => q.skills);
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
            foreach (var skill in item.skills)
            {
                Context.Skills.Add(skill);
            }
            Context.SaveChanges();
        }

        public void PutPerson(Person updatedPerson)
        {
            Person currentItem = Context.Persons.Include(q => q.skills).FirstOrDefault(w => w.id == updatedPerson.id);
            currentItem.name = updatedPerson.name;
            currentItem.displayName = updatedPerson.displayName;
            foreach (var skill in currentItem.skills)
            {
                Skill updatedSkill = Context.Skills.FirstOrDefault(q => q.skillName == skill.skillName && q.personid == skill.personid);
                if (updatedSkill == null)
                {
                    Skill newSkill = new Skill() { personid = skill.personid, skillName = skill.skillName, level = skill.level, person = currentItem };
                }
                else
                {
                    updatedSkill.level = updatedPerson.skills.FirstOrDefault(q => q.skillName == skill.skillName && q.personid == skill.personid).level;
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
