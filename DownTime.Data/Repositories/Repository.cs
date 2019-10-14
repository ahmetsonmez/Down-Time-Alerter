using DownTime.Data.Context;
using DownTime.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DownTime.Data.Repositories
{
    public class Repository<T> : BaseRepository<T> where T : class
    {
        private AppDbContext db = null;
        private DbSet<T> dbSet = null;
        public Repository(DbContextOptions<AppDbContext> options) : base()
        {
            db = new AppDbContext(options);
            dbSet = db.Set<T>();
        }

        public override Task<BaseResponseModel<bool>> Create(T entity)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();

            try
            {
                response.Data = true;
                response.Total = 1;

                dbSet.AddAsync(entity);
                Save();

            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Errors.Add(ex.ToString());
            }

            return Task.FromResult(response);
        }

        public override Task<BaseResponseModel<bool>> Update(T entity)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();

            try
            {
                response.Data = true;
                response.Total = 1;
                db.Attach(entity);
                db.Entry(entity);
                db.Entry(entity).State = EntityState.Modified;
                                
                Save();

            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Errors.Add(ex.ToString());
            }

            return Task.FromResult(response);
        }

        public override Task<BaseResponseModel<bool>> Delete(int Id)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();

            try
            {
                response.Data = true;
                response.Total = 1;

                T entity = dbSet.Find(Id);
                dbSet.Remove(entity);
                Save();

            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Errors.Add(ex.ToString());
            }

            return Task.FromResult(response);
        }


        public override Task<BaseResponseModel<T>> Get(Expression<Func<T, bool>> filter)
        {
            BaseResponseModel<T> result = new BaseResponseModel<T> {Data = dbSet.FirstOrDefault(filter), Total = 1};
            return Task.FromResult(result);
        }

        public override Task<BaseResponseModel<T>> Get(object Id)
        {
            BaseResponseModel<T> result = new BaseResponseModel<T> {Data = dbSet.Find(Id), Total = 1};
            return Task.FromResult(result);
        }

        public override Task<BaseResponseModel<List<T>>> List(Expression<Func<T, bool>> filter, int take)
        {
            BaseResponseModel<List<T>> result = new BaseResponseModel<List<T>>
            {
                Data = dbSet.Where(filter).Take(take).ToList()
            };
            result.Total = result.Data.Count;
            return Task.FromResult(result);
        }

        public override Task<BaseResponseModel<IQueryable<T>>> List()
        {
            var result = new BaseResponseModel<IQueryable<T>>
            {
                Data = dbSet
            };
          //  result.Total = result.Data.Count;
            return Task.FromResult(result);
        }

        private void Save()
        {
            db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}
