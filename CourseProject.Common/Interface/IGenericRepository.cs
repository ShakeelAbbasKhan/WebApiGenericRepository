﻿using CourseProject.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetFilterAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<int> InsertAsync(T entity);
        void UpdateAsync(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
