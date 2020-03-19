using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    public class Person : ICommonData<Person>
    {
        private ICommonData<Person> _icommonData;
        public string Name { private get; set; }
    
        public int Age {  get;  set; }

        public int Id { private get; set; }
        
        public string GetName() => this.Name;
        
        public int GetAge() => this.Age;

        public Person(ICommonData<Person> icommonData)
        {
            _icommonData = icommonData;
        }


        public Person(string name, int age)
        {
    //        if (age < 18) throw new WrongPersonAgeException(age);

            this.Name = name;
            this.Age = age;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Name))
                throw new MissingPersonNameException();

            return $"Hi {this.Name}!";
        }

        public IList<Person> GetAll()
        {
            return new List<Person>()
            {
                new Person("Jorge", 15),
                new Person("Juan", 10),
                new Person("Matías", 11),
                new Person("Bárbara", 13),
                new Person("Josefina", 6),
                new Person("María", 9),
            };
        }

        public Person GetById(int id)
        {
           return _icommonData.GetAll().FirstOrDefault(z => z.Id == id);
        }

        public IList<Person> AddYearsToPeople()
        {
            var persons = _icommonData.GetAll();

            if (persons != null && persons.Any())
            {
                foreach (var person in persons)
                {
                    if (person.GetAge() >= 18)
                        person.Age++;
                }
            }

            return persons;
        }
    }
}
