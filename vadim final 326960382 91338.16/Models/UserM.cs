using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DAL;

namespace vadim_final_326960382_91338._16.Models
{
    public class UserM : GenericRepository<Entities, User>, IEntityRep<User>
    {
        public User GetById(int id)
        {
            var q = SearchFor(u => u.id == id);
            return q.First();
        }
        public IEnumerable<User> GetByIdNum(int id)
        {
            var q = SearchFor(u => u.IdNumber == id);
            return q;
        }
        public IEnumerable<User> GetbyName(string name)
        {
            var q = SearchFor(u => u.FullName ==name);
            return q;
        }
        public override void Delete(User entity)
        {
            base.Delete(entity);
            base.Save();
        }

        public override void Create(User entity)
        {
            base.Create(entity);
            base.Save();
        }

        public void SaveWithImage(User entity, HttpPostedFileBase img)
        {
            entity.Image = new byte[img.ContentLength];
            img.InputStream.Read(entity.Image, 0, img.ContentLength);
            Create(entity);
        }

        public void UpdateWithImage(User entity, HttpPostedFileBase img)
        {
            entity.Image = new byte[img.ContentLength];
            img.InputStream.Read(entity.Image, 0, img.ContentLength);
            base.Update(entity);
        }
        public bool Any(User entity)
        {
            return Context.Users.Any(user => user.FullName == entity.FullName && user.Password == entity.Password);
        }
    }
}