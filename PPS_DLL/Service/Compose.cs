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
    public class Compose
    {
        ProjetContext context;

        public Compose()
        {
            context = new ProjetContext();
        }

        public void Add(Business.Compose business)
        {
            var entity = MapperCompose.Map(business);
            context.Compose.Add(entity);
            context.SaveChanges();
        }
        /*public void Delete(int id)
        {
            var entity = (from p in context.Compose where p.Id == id select p).FirstOrDefault();
            if (entity != null)
            {
                context.Compose.Remove(entity);
                context.SaveChanges();
            }

        }*/
        /*public void Update(Business.Compose business)
        {
            var entity = (from p in context.Compose where p.Id == business.Id select p).FirstOrDefault();
            entity.Name = business.Name;
            context.SaveChanges();
        }*/
        public List<Business.Compose> Select()
        {
            return (from p in context.Compose.Include(i => i.Actions).Include(i => i.Scenario)
                    select MapperCompose.Map(p)).ToList();

        }
    }

}
