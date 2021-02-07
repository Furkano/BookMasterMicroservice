using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.ElasticOption.IOption;
using Core.ElasticOption.Option;
using Core.Entities;
using Core.IRepository;
using Nest;
using Newtonsoft.Json;

namespace Infrastructure.Repository
{
    public class BookRepository:BaseRepository,IBookRepository
    {
        private IElasticClient Client { get; }
        public BookRepository(IElasticSearchConfigration elasticSearchConfigration) : base(elasticSearchConfigration)
        {
            Client = ElasticSearchClient ?? throw new ArgumentNullException(nameof(elasticSearchConfigration));
        }
        
        public async Task<IEnumerable<Book>> SuggestSearchAsync(string suggestText, int maxItemCount = 10)
        {
            try
            {
                var query = new Nest.SearchDescriptor<Book>()
                    .Suggest(suggest => suggest
                        .Completion("book_suggestions",
                            c => c.Field(f => f.Suggest)
                                .Analyzer("simple")
                                .Prefix(suggestText)
                                .Fuzzy(fe => fe.Fuzziness(Nest.Fuzziness.Auto))
                                .Size(maxItemCount))
                    );
                var returnData = await SimpleSearchAsync<Book, int>("books", query);
                // var data = JsonConvert.SerializeObject(returnData);
                var suggestList = returnData.Documents.AsEnumerable();
                return suggestList;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetSearchAsync(string searchText, int skipItemCount = 0, int maxItemCount = 5)
        {
            try
            {
                var searchQuery = new Nest.SearchDescriptor<Book>()
                    .From(skipItemCount)
                    .Size(maxItemCount)
                    .Query(q =>
                        q.MatchPhrase(m => 
                            m.Field(f => f.Name).Query(searchText)));
                var returnData = await SimpleSearchAsync<Book, int>("books", searchQuery);
                var data = returnData.Documents.AsEnumerable();
                return data;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        
    }
}