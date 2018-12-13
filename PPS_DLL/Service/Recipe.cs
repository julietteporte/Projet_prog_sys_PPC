using PPS_DLL.Business.Mapper;
using PPS_DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Recipe
    {
        ProjetContext context;
        public int Price;
        public string RecipeName;
        public List<string> Categories;
        public int TimePrepare;
        public List<Recipe> RecipeList;
        public bool IsFinished;

        public Recipe(int price, string name, List<string> cat, int timePrepare)
        {
            context = new ProjetContext();
            this.Price = price;
            this.RecipeName = name;
            this.Categories = cat;
            this.TimePrepare = timePrepare;
        }

        public void Add(Business.Recipe business)
        {
            var entity = MapperRecipe.Map(business);
            context.Recipe.Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = (from c in context.Recipe where c.Id == id select c).FirstOrDefault();
            if (entity != null)
            {
                context.Recipe.Remove(entity);
                context.SaveChanges();
            }

        }
        public void Update(Business.Recipe business)
        {
            var entity = (from c in context.Recipe where c.Id == business.Id select c).FirstOrDefault();
            entity.Name = business.Name;
            context.SaveChanges();
        }

        public List<Business.Recipe> Select()
        {
            return (from c in context.Recipe select MapperRecipe.Map(c)).ToList();
        }
    }

}

