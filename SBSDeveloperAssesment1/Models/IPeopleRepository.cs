namespace SBSDeveloperAssesment1.Models
{
    public interface IPeopleRepository
    {
        Person Find(int id);
        Person Find(string username);
        void Save(Person person);
        void UpdateLastLogin(Person person);
    }
}
