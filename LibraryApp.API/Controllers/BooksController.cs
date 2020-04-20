using AutoMapper;
using LibraryApp.API.Mapper;
using LibraryApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryAppDataRepository _libraryAppDataRepository;
        private readonly IMapper _mapper;

        public BooksController(ILibraryAppDataRepository libraryAppDataRepository, IMapper mapper)
        {
            this._libraryAppDataRepository = libraryAppDataRepository
                ?? throw new ArgumentNullException(nameof(libraryAppDataRepository));

            this._mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get books by authorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookDto>>> GetBooks(Guid authorId)
        {
            try
            {
                var books = await _libraryAppDataRepository.GetBooksByAuthorIdAsync(authorId);
                var bookDtos = _mapper.Map<List<BookDto>>(books);
                return Ok(bookDtos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }

        /// <summary>
        /// Get books by authorId and bookId
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetBooks(Guid authorId, Guid id)
        {
            try
            {
                var book = await _libraryAppDataRepository.GetBookByAuthorIdAsync(authorId, id);

                if (book == null)
                {
                    return NotFound();
                }

                var bookDto = _mapper.Map<BookDto>(book);
                return Ok(bookDto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }
    }
}