using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DAL;

namespace vadim_final_326960382_91338._16.Models
{
    public class BranchM : GenericRepository<Entities, Branch>, IEntityRep<Branch>
    {
        public Branch GetById(int id)
        {
            var q = SearchFor(b => b.id == id);
            return q.First();
        }

        public List<string> GetNames()
        {
            var list = base.GetAll().ToList();
            List<string> l = new List<string>();
            foreach (var i in list)
            {
                l.Add(i.NameBranch);
            }
            return l;
        }
    }

}