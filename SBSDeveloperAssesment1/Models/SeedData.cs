using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SBSDeveloperAssesment1.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            Person bob = new Person
            {
                Name = "Bob",
                Surname = "Smith",
                Password = "password"
            };

            Info bobInfo = new Info
            {
                TelNo = "647-746-6515",
                CellNo = "647-746-7777",
                AddressLine1 = "1883 Zigzag Rd",
                AddressLine2 = "Meyerton",
                AddressLine3 = "",
                AddressCode = "1938",
                PostalAddress1 = "1883 Zigzag Rd",
                PostalAddress2 = "Meyerton",
                PostalCode = "1938"
            };

            if ((context.People.FirstOrDefault(p => p.Name == bob.Name && p.Surname == bob.Surname)) == null)
            {
                context.People.Add(bob);
                context.SaveChanges();


                Person entry = context.People.FirstOrDefault(p => p.Name == bob.Name && p.Surname == bob.Surname);
                bobInfo.PersonId = entry.Id;

                context.Info.Add(bobInfo);
            }

            context.SaveChanges();
        }
    }
}
