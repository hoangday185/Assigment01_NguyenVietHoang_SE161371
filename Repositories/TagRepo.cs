using BO;
using DAO;

namespace Repositories
{
    public class TagRepo : ITagRepo
    {
        public void CreateTag(Tag tag)
        {
            //use TagDAO to create new tag
            TagDAO.Instance.CreateTag(tag);
        }

        public void DeleteTag(int id)
        {
            TagDAO.Instance.DeleteTag(id);
        }

        public Tag? GetTagById(int id)
        => TagDAO.Instance.GetTagById(id);

        public List<Tag> Tags()
        => TagDAO.Instance.Tags();

        public void UpdateTag(Tag tag)
        {
            TagDAO.Instance.UpdateTag(tag);
        }
    }
}
