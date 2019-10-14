using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DownTime.Data.ViewModels;

namespace DownTime.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public abstract Task<BaseResponseModel<bool>> Create(T entity);
        public abstract Task<BaseResponseModel<bool>> Update(T entity);
        public abstract Task<BaseResponseModel<bool>> Delete(int Id);
        public abstract Task<BaseResponseModel<T>> Get(Expression<Func<T, bool>> filter);
        public abstract Task<BaseResponseModel<T>> Get(object Id);
        public abstract Task<BaseResponseModel<List<T>>> List(Expression<Func<T, bool>> filter, int take);
        public abstract Task<BaseResponseModel<IQueryable<T>>> List();
    }
}
