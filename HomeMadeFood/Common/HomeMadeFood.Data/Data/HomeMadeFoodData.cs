using System;
using Bytes2you.Validation;

using HomeMadeFood.Data.Repositories;
using HomeMadeFood.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeMadeFood.Data.Data
{
    public class HomeMadeFoodData : IHomeMadeFoodData
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IEfRepositoryFactory repositoryFactory;

        public HomeMadeFoodData(ApplicationDbContext dbContext, IEfRepositoryFactory repositoryFactory)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();
            this.dbContext = dbContext;

            Guard.WhenArgument(repositoryFactory, "repositoryFactory").IsNull().Throw();
            this.repositoryFactory = repositoryFactory;
        }

        public IEfRepository<FoodCategory> FoodCategories
        {
            get
            {
                return this.repositoryFactory.Create<FoodCategory>();
            }
        }

        public IEfRepository<Ingredient> Ingredients
        {
            get
            {
                return this.repositoryFactory.Create<Ingredient>();
            }
        }

        public IEfRepository<Recipe> Recipes
        {
            get
            {
                return this.repositoryFactory.Create<Recipe>();
            }
        }

        public IEfRepository<IdentityUserRole> Roles
        {
            get
            {
                return this.repositoryFactory.Create<IdentityUserRole>();
            }
        }

        public IEfRepository<ApplicationUser> Users
        {
            get
            {
               return this.repositoryFactory.Create<ApplicationUser>();
            }
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}