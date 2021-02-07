using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ElasticOption.IOption;
using Core.ElasticOption.Option;
using Core.Entities;
using Core.IRepository;
using Nest;

namespace Infrastructure.Repository
{
    public class BaseRepository :IBaseRepository
    {
        public IElasticClient ElasticSearchClient { get; set; }
        private readonly IElasticSearchConfigration _elasticSearchConfigration;

        public BaseRepository(IElasticSearchConfigration elasticSearchConfigration)
        {
            _elasticSearchConfigration = elasticSearchConfigration;
            ElasticSearchClient = GetClient();
        }
        private ElasticClient GetClient()
        {
            var str = _elasticSearchConfigration.ConnectionString;
            var strs = str.Split('|');
            var nodes = strs.Select(s => new Uri(s)).ToList();

            var connectionString = new ConnectionSettings(new Uri(str))
                .DefaultIndex("books")
                .DisablePing()
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false);

            if (!string.IsNullOrEmpty(_elasticSearchConfigration.AuthUserName) && !string.IsNullOrEmpty(_elasticSearchConfigration.AuthPassWord))
                connectionString.BasicAuthentication(_elasticSearchConfigration.AuthUserName, _elasticSearchConfigration.AuthPassWord);

            return new ElasticClient(connectionString);
        }

        public virtual async Task<Boolean> CreateAsync<T, TKey>(T model) where T : ElasticEntity<TKey>
        {
            var result = await ElasticSearchClient.IndexAsync(model,
                ss => ss.Id(model.Id.ToString()));
            if (result.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync<T, TKey>(T model) where T : ElasticEntity<TKey>
        {
            var exist = await ElasticSearchClient.DocumentExistsAsync(DocumentPath<T>.Id(model), u => u.Index("books"));
            if (exist.Exists)
            {
                var result = await ElasticSearchClient.UpdateAsync(DocumentPath<T>.Id(model),
                        ss=>ss.Index("books").Doc(model).RetryOnConflict(3));
                if (result.IsValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync<T, TKey>(T model) where T : ElasticEntity<TKey>
        {
            var response = await ElasticSearchClient.DeleteAsync(DocumentPath<T>.Id(model),
                ss => ss.Index("books"));
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ISearchResponse<T>> SimpleSearchAsync<T, TKey>(string indexName, SearchDescriptor<T> query) where T : ElasticEntity<TKey>
        {
            query.Index(indexName);
            var response = await ElasticSearchClient.SearchAsync<T>(query);
            return response;
        }

        public async Task<IReadOnlyCollection<Book>> SearchAsyncWithBookId<T, TKey>(int bookId) where T : ElasticEntity<TKey>
        {
            var response = await ElasticSearchClient.SearchAsync<Book>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Id)
                        .Query(bookId.ToString())
                    )
                )
            );
            return  response.Documents;
        }
        
    }
}