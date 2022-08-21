using System;
using System.Linq;

namespace SBSDeveloperAssesment1.Models
{
    public class EFPeopleRepository : IPeopleRepository
    {
        private readonly ApplicationDbContext _context;

        public EFPeopleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Person Find(int id)
        {
            return _context.People.FirstOrDefault(p => p.Id == id);
        }

        public Person Find(string username)
        {
            return _context.People.FirstOrDefault(p => p.Name.ToLower() == username.ToLower());
        }

        public void Save(Person person)
        {
            if (person.Id == 0)
            {
                person.LastLogin = DateTime.Now;
                _context.People.Add(person);
            }
            else
            {
                Person entry = _context.People.FirstOrDefault(p => p.Id == person.Id);
                if (entry != null)
                {
                    entry.Password = person.Password;
                    entry.LastLogin = person.LastLogin;
                }
            }

            _context.SaveChanges();
        }

        public void UpdateLastLogin(Person person)
        {
            Person entry = _context.People.FirstOrDefault(p => p.Id == person.Id);
            if (entry != null)
            {
                entry.LastLogin = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }
}
