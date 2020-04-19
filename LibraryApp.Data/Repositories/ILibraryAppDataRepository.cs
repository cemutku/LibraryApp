using LibraryApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Data.Repositories
{
    public interface ILibraryAppDataRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<List<Article>> GetArticlesAsync();
        Task<List<Article>> GetArticlesByReleaseDateAsync(DateTime date);

        Task<Article> GetArticleAsync(Guid id);

        Task<List<Book>> GetBooksByAuthorIdAsync(Guid authorId);

        Task<Book> GetBookByAuthorIdAsync(Guid authorId, Guid bookId);
    }
}
