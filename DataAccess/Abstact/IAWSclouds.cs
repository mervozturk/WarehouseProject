using Core.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstact
{
    public interface IAWSclouds<T>
    {
        Result Add(T entity);
        Result Update(T entity);
        Result Delete(T entity);
        DataResult<ObservableCollection<T>> GetAll(Func<T, bool> filter = null);
        DataResult<T> Get(Func<T, bool> filter = null);
    }
}
