using PPS_DLL.Business.Mapper;
using PPS_DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public abstract class People
    {
        ProjetContext context;
        public abstract void Wait();
        public abstract int Id { get; }
        public People() {
            context = new ProjetContext();
}
        public void Add(Business.People business)
        {
            var entity = MapperPeople.Map(business);
            context.People.Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = (from c in context.People where c.Id == id select c).FirstOrDefault();
            if (entity != null)
            {
                context.People.Remove(entity);
                context.SaveChanges();
            }

        }
        public void Update(Business.People business)
        {
            var entity = (from c in context.People where c.Id == business.Id select c).FirstOrDefault();
            entity.Type = business.Type;
            context.SaveChanges();
        }

        public List<Business.People> Select()
        {
            return (from c in context.People select MapperPeople.Map(c)).ToList();

        }
    }

}
}
