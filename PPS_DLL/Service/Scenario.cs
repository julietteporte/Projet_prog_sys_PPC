using PPS_DLL.Business.Mapper;
using PPS_DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Scenario
    {
        ProjetContext context;
        public int Id { get; set; }
        public int nbrCookChief { get; set; }
        public int nbrChiefRank { get; set; }
        public int nbrCustomer { get; set; }
        public int nbrSlowCustomer { get; set; }
        public int nbrFastCustomer { get; set; }
        public int nbrReservedCustomer { get; set; }
        public int nbrServer { get; set; }
        public int nbrTable { get; set; }

        public Scenario()
        {
            context = new ProjetContext();
        }

        public void Add(Business.Scenario business)
        {
            var entity = MapperScenario.Map(business);
            context.Scenario.Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = (from c in context.Scenario where c.Id == id select c).FirstOrDefault();
            if (entity != null)
            {
                context.Scenario.Remove(entity);
                context.SaveChanges();
            }

        }
        public void Update(Business.Scenario business)
        {
            var entity = (from c in context.Scenario where c.Id == business.Id select c).FirstOrDefault();
            entity.Name = business.Name;
            context.SaveChanges();
        }

        public List<Business.Scenario> Select()
        {
            return (from c in context.Scenario select MapperScenario.Map(c)).ToList();

        }

    }
}
