using LibraryApp.API.ResourceParameters;
using LibraryApp.Data.ResourceParameters;
using LibraryApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Data.Repositories
{
    public class LibraryAppDataRepository : ILibraryAppDataRepository, IDisposable
    {
        private LibraryAppDbContext _libraryAppDbContext;

        public LibraryAppDataRepository(LibraryAppDbContext libraryAppDbContext)
        {
            this._libraryAppDbContext = libraryAppDbContext ?? throw new ArgumentNullException(nameof(libraryAppDbContext));
        }

        public void Add<T>(T entity) where T : class
        {
            _libraryAppDbContext.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _libraryAppDbContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _libraryAppDbContext.Remove(entity);
        }

        public async Task<Article> GetArticleAsync(Guid id)
        {
            return await _libraryAppDbContext.Articles.FindAsync(id);
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _libraryAppDbContext.Articles.Include(a => a.Author).ToListAsync();
        }

        public async Task<PagedList<Article>> GetArticlesAsync(ArticlesResourceParameters articlesResourceParameters)
        {
            var queryableArticles = _libraryAppDbContext.Articles.AsQueryable();

            queryableArticles = _libraryAppDbContext.Articles
                .Include(a => a.Author);

            return await PagedList<Article>.Create(queryableArticles,
                articlesResourceParameters.PageNumber,
                articlesResourceParameters.PageSize);
        }


        public async Task<List<Article>> GetArticlesByReleaseDateAsync(DateTime date)
        {
            return await _libraryAppDbContext.Articles
                .Include(a => a.Author)
                .Where(a => a.ReleaseDate == date)
                .OrderByDescending(a => a.ReleaseDate)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthorIdAsync(Guid authorId)
        {
            return await _libraryAppDbContext.Books
                .Include(a => a.Author)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<Book> GetBookByAuthorIdAsync(Guid authorId, Guid bookId)
        {
            return await _libraryAppDbContext.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(b => b.AuthorId == authorId && b.Id == bookId);
        }

        public async Task<List<Book>> GetBooksByAuthorIdAndCreationTime(Guid authorId, DateTime createTime)
        {
            return await _libraryAppDbContext.Books
                .Include(a => a.Author)
                .Where(b => b.AuthorId == authorId && EF.Property<DateTime>(b, "Created") >= createTime)                
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _libraryAppDbContext.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_libraryAppDbContext != null)
                {
                    _libraryAppDbContext.Dispose();
                    _libraryAppDbContext = null;
                }
            }
        }
    }
}
