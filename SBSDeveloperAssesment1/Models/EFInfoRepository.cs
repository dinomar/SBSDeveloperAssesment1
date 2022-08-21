using System.Linq;

namespace SBSDeveloperAssesment1.Models
{
    public class EFInfoRepository : IInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public EFInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Info Find(int personId)
        {
            return _context.Info.FirstOrDefault(i => i.PersonId == personId);
        }

        public void Save(Info info)
        {
            Info entry = _context.Info.FirstOrDefault(i => i.PersonId == info.PersonId);
            if (entry == null)
            {
                _context.Info.Add(info);
            }
            else
            {
                entry.TelNo = info.TelNo;
                entry.CellNo = info.CellNo;
                entry.AddressLine1 = info.AddressLine1;
                entry.AddressLine2 = info.AddressLine2;
                entry.AddressLine3 = info.AddressLine3;
                entry.AddressCode = info.AddressCode;
                entry.PostalAddress1 = info.PostalAddress1;
                entry.PostalAddress2 = info.PostalAddress2;
                entry.PostalCode = entry.PostalCode;
            }

            _context.SaveChanges();
        }
    }
}
