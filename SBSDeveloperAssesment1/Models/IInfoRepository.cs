namespace SBSDeveloperAssesment1.Models
{
    public interface IInfoRepository
    {
        Info Find(int personId);
        void Save(Info info);
    }
}
