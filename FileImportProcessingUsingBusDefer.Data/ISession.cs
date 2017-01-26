﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileImportProcessingUsingBusDefer.Data
{
    public interface ISession : IDisposable
    {
        void Add<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
