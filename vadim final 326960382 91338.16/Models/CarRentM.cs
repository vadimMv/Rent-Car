using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DAL;

namespace vadim_final_326960382_91338._16.Models
{
    public class RentCar : GenericRepository<Entities, CarsRent>, IEntityRep<CarsRent>
    {
        public CarsRent GetById(int id)
        {
            var q = SearchFor(u => u.id == id);
            return q.First();
        }

        public override void Create(CarsRent entity)
        {
            base.Create(entity);
            base.Save();
        }


        public IEnumerable<CarsRent> GetList(string name)
        {
            User ur = Context.Users.Where(u => u.FullName == name).FirstOrDefault();
            //  return SearchFor(rent => (rent.NumUser == ur.IdNumber && rent.FinishRent != rent.RealFinishRent));
            return SearchFor(rent => (rent.NumUser == ur.IdNumber));
        }

        public IEnumerable<CarsRent> GetByIdOrNum(int uid)
        {

            return SearchFor(rent => (rent.NumCar == uid || rent.NumUser == uid));
        }
    }
}