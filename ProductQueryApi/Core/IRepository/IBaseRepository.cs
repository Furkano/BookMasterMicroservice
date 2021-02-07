using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ElasticOption.Option;
using Core.Entities;
using Nest;


namespace Core.IRepository
{
    public interface IBaseRepository
    {
        Task<Boolean> CreateAsync<T, TKey>( T model) where T : ElasticEntity<TKey>;
        Task<Boolean> UpdateAsync<T, TKey>( T model) where T : ElasticEntity<TKey>;
        Task<Boolean> DeleteAsync<T, TKey>( T model) where T : ElasticEntity<TKey>;
        Task<ISearchResponse<T>> SimpleSearchAsync<T, TKey>(string indexName, SearchDescriptor<T> query) where T : ElasticEntity<TKey>;
        Task<IReadOnlyCollection<Book>> SearchAsyncWithBookId<T, TKey>(int bookId) where T : ElasticEntity<TKey>;
        
    }
}