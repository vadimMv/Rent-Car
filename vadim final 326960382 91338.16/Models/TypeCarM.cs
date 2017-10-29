using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DAL;
using BL;
using System.Web.Mvc;

namespace vadim_final_326960382_91338._16.Models
{
    public class TypeCarM : GenericRepository<Entities, CarType>, IEntityRep<CarType>, BL.IList<CarType>
    {
        public CarType GetById(int id)
        {
            var q = SearchFor(u => u.id == id);
            return q.FirstOrDefault();
        }

        public override void Create(CarType entity)
        {
            base.Create(entity);
            base.Save();
        }
        public CarType GetByCarNum(string model)
        {
            var ct = SearchFor(c => c.Model == model);
            return ct.First();
        }

        public IEnumerable<CarType> GetItems(SearchCarsModel searchModel)
        {

            var result = Context.CarTypes.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.FreeText))
                {
                    DateTime dDate;
                    var text = searchModel.FreeText;
                    var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = text.Split().Select(x => x.Trim(punctuation));
                    foreach (var w in words)
                    {

                        if (!string.IsNullOrEmpty(w))
                        {
                            if (char.IsDigit(w[0]))
                            {
                                if (DateTime.TryParse(w, out dDate))
                                {
                                    result = result.Where(x => x.YearManufacture == dDate);
                                }
                            } 
                        }
                        if (result.Count(x => x.Manufacturer.Contains(w.ToUpper()) || x.Model.Contains(w.ToUpper()) || x.Geer.Contains(w.ToUpper())) > 0)
                            result = result.Where(x => x.Manufacturer.Contains(w.ToUpper()) || x.Model.Contains(w.ToUpper()) || x.Geer.Contains(w.ToUpper()));
                      
                    }

                    //.Where(x => x.Model.Contains(searchModel.FreeText))
                    //   .Where(x => x.Model.Contains(searchModel.FreeText))
                    //     .Where(x => x.Geer.Contains(searchModel.Geer));
                }
                else
                {
                    if (searchModel.DailyCost.HasValue)
                        result = result.Where(x => x.DailyCost == searchModel.DailyCost);
                    if (searchModel.DelayCost.HasValue)
                        result = result.Where(x => x.DelayCost == searchModel.DelayCost);
                    if (!string.IsNullOrEmpty(searchModel.Manufacturer))
                        result = result.Where(x => x.Manufacturer.Contains(searchModel.Manufacturer));
                    if (!string.IsNullOrEmpty(searchModel.Model))
                        result = result.Where(x => x.Model.Contains(searchModel.Model));
                    if (searchModel.YearManufacture.HasValue)
                        result = result.Where(x => x.YearManufacture == searchModel.YearManufacture);
                    if (!string.IsNullOrEmpty(searchModel.Geer) && !searchModel.Geer.Equals("All"))
                        result = result.Where(x => x.Geer.Contains(searchModel.Geer));

                }
            }
            return result.OrderBy(x => x.id);
        }
 
        public List<string> GetModelNames()
        {

            var result = Context.CarTypes.AsEnumerable();
            List<string> l = new List<string>();
            foreach (var i in result)
            {
                l.Add(i.Model);
            }
            return l;
        }
        public List<string> GetManufactures()
        {

            var result = Context.CarTypes.AsEnumerable();
            List<string> l = new List<string>();
            foreach (var i in result)
            {
                l.Add(i.Manufacturer);
            }
            return l;
        }
    }
}