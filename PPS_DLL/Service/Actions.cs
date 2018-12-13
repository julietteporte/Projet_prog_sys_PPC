using Microsoft.EntityFrameworkCore;
using PPS_DLL.Business.Mapper;
using PPS_DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Actions
    {
        ProjetContext context;

        public Actions()
        {
            context = new ProjetContext();
        }

        public void Add(Business.Actions business)
        {
            var entity = MapperActions.Map(business);
            context.Actions.Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = (from p in context.Actions where p.Id == id select p).FirstOrDefault();
            if (entity != null)
            {
                context.Actions.Remove(entity);
                context.SaveChanges();
            }

        }
        public void Update(Business.Actions business)
        {
            var entity = (from p in context.Actions where p.Id == business.Id select p).FirstOrDefault();
            entity.Name = business.Name;
            context.SaveChanges();
        }
        public List<Business.Actions> Select()
        {
            return (from p in context.Actions.Include(i => i.People) select MapperActions.Map(p)).ToList();

        }
    }

}
