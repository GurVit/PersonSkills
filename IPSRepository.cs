using PersonSkill.Models;
using System.Collections.Generic;

namespace PersonSkill
{
    public interface IPSRepository
    {
        IEnumerable<Person> Get();
        Person Get(long id);
        void PostPerson(Person item);
        void PutPerson(Person item);
        Person Delete(long id);
    }
}
