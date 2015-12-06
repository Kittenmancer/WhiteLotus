using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Linq;
using PagedList;

namespace WhiteLotus.Models
{
    public interface IQuery<T>
    {
        T Execute(ISession session);
    }

    public interface IMultiQuery<T> : IQuery<IEnumerable<T>> { }
    public interface IPagedQuery<T> : IQuery<IPagedList<T>> { }
}