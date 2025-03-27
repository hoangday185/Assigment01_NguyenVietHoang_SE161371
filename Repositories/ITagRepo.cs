using BO;
namespace Repositories
{
    public interface ITagRepo
    {
        //crud 
        public List<Tag> Tags();

        public void CreateTag(Tag tag);

        public void UpdateTag(Tag tag);

        public Tag? GetTagById(int id);

        public void DeleteTag(int id);

    }
}
