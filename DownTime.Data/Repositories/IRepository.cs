using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DownTime.Data.ViewModels;

namespace DownTime.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
       Task<BaseResponseModel<List<T>>> List(Expression<Func<T, bool>> filter, int take);
       Task<BaseResponseModel<IQueryable<T>>> List();
       Task<BaseResponseModel<T>> Get(Expression<Func<T, bool>> filter);
       Task<BaseResponseModel<T>> Get(object Id);
       Task<BaseResponseModel<bool>> Create(T entity);
       Task<BaseResponseModel<bool>> Update(T entity);
       Task<BaseResponseModel<bool>> Delete(int Id);
    }
}
