using BO;
using Microsoft.EntityFrameworkCore;
namespace DAO
{
    public class TagDAO
    {
        private static TagDAO instance;

        //create private funewsManagementContext
        private FunewsManagementContext funewsManagementContext;

        //create private constructor

        public TagDAO()
        {
            funewsManagementContext = new FunewsManagementContext();
        }

        //create public static instance use singleTon

        public static TagDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TagDAO();
                }
                return instance;
            }
        }

        //function private createDB context
        private FunewsManagementContext CreateDBContext() => new FunewsManagementContext();

        //function get all tag

        public List<Tag> Tags()
        {
            using (var db = CreateDBContext())
            {
                return db.Tags.ToList();
            }
        }

        //create new tag
        public void CreateTag(Tag tag)
        {
            using (var db = CreateDBContext())
            {
                var result = GetTagById(tag.TagId);
                if (result == null)
                {
                    db.Tags.Add(tag);
                    db.SaveChanges();
                }

            }
        }

        //get tag by id
        public Tag? GetTagById(int id)
        {
            using (var db = CreateDBContext())
            {
                return db.Tags.AsNoTracking().FirstOrDefault(m => m.TagId == id);
            }
        }

        //update tag
        public void UpdateTag(Tag tag)
        {
            using (var db = CreateDBContext())
            {
                var result = GetTagById(tag.TagId);
                if (result != null)
                {
                    db.Tags.Update(tag);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteTag(int id)
        {
            using (var db = CreateDBContext())
            {

                var result = GetTagById(id);
                //var newTags = db.NewsArticles.
                if (result != null)
                {
                    db.Tags.Remove(result);
                    db.SaveChanges();
                }
            }
        }
    }
}
