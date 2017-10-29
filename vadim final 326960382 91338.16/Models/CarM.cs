using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DAL;
using BL;

namespace vadim_final_326960382_91338._16.Models
{
    public class CarM : GenericRepository<Entities, Car>, IEntityRep<Car>
    {
        public Car GetById(int id)
        {
            var q = SearchFor(c => c.id == id);
            return q.First();
        }

        public Car GetByNumId(int id)
        {
            var q = SearchFor(c => c.CarNumber == id);
            return q.FirstOrDefault();
        }

        public IEnumerable<string>GetBranches(string model)
        {
            var q = SearchFor(c => c.CarModel == model && c.FreeForRent==true);
            return q.Select(b=>b.Branch).Distinct();
        }
        public void SaveWithImage(Car entity,HttpPostedFileBase img ) {

            entity.Image = new byte[img.ContentLength];
            img.InputStream.Read(entity.Image, 0, img.ContentLength);
            Create(entity);
        }

        public override void Create(Car entity)
        {
            base.Create(entity);
            base.Save();
        }

    }

}