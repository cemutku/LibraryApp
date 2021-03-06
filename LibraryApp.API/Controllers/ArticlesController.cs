﻿using AutoMapper;
using LibraryApp.API.Helpers;
using LibraryApp.API.Mapper;
using LibraryApp.API.ResourceParameters;
using LibraryApp.Data.Repositories;
using LibraryApp.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryApp.API.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ILibraryAppDataRepository _libraryAppDataRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(ILibraryAppDataRepository libraryAppDataRepository,
            IMapper mapper,
            ILogger<ArticlesController> logger)
        {
            this._libraryAppDataRepository = libraryAppDataRepository
                ?? throw new ArgumentNullException(nameof(libraryAppDataRepository));

            this._mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));

            this._logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetArticles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ArticleDto>>> GetArticles([FromQuery] ArticlesResourceParameters articlesResourceParameters)
        {
            var articles = await _libraryAppDataRepository.GetArticlesAsync(articlesResourceParameters);

            var previousPageLink = articles.HasPrevious
                ? CreateArticlesResourceUri(articlesResourceParameters, ResourceUriType.PreviousPage)
                : null;

            var nextPageLink = articles.HasNext
                ? CreateArticlesResourceUri(articlesResourceParameters, ResourceUriType.NextPage)
                : null;

            var paginationMetadata = new
            {
                totalCount = articles.TotalCount,
                pageSize = articles.PageSize,
                currentPage = articles.CurrentPage,
                totalPages = articles.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata,
                new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));

            var articleDtos = _mapper.Map<List<ArticleDto>>(articles);

            return Ok(articleDtos);
        }

        /// <summary>
        /// Get an article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}", Name = "GetArticle")]
        public async Task<ActionResult<ArticleDto>> GetArticle(Guid id)
        {
            var article = await _libraryAppDataRepository.GetArticleAsync(id);

            if (article == null)
            {
                _logger.LogInformation($"Article with id {id} was not found in the database.");
                return NotFound();
            }

            var articleDto = _mapper.Map<ArticleDto>(article);

            return Ok(articleDto);
        }

        /// <summary>
        /// Create an article
        /// </summary>
        /// <param name="articleItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleForCreationDto articleItem)
        {
            _logger.LogDebug("Creating article...");
            var article = _mapper.Map<Article>(articleItem);
            _libraryAppDataRepository.Add(article);
            await _libraryAppDataRepository.SaveChangesAsync();

            return CreatedAtRoute("GetArticle", new { id = article.Id }, article);
        }

        /// <summary>
        /// Update an article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="articleItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ArticleDto>> UpdateArticle(Guid id, [FromBody] ArticleForUpdateDto articleItem)
        {
            if (id != articleItem.Id)
            {
                _logger.LogInformation($"Article with id {id} was not match articleItem");
                return BadRequest();
            }

            var currentArticle = await _libraryAppDataRepository.GetArticleAsync(id);

            if (currentArticle == null)
            {
                _logger.LogInformation($"Article with id {id} was not found in the database.");
                return NotFound();
            }

            _logger.LogDebug("Updating article...");

            _mapper.Map(articleItem, currentArticle);
            await _libraryAppDataRepository.SaveChangesAsync();
            var articleDto = _mapper.Map<ArticleDto>(currentArticle);

            return Created("", articleDto);
        }

        /// <summary>
        /// Delete an article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var article = await _libraryAppDataRepository.GetArticleAsync(id);

            if (article == null)
            {
                _logger.LogInformation($"Article with id {id} was not found in the database.");
                return NotFound();
            }

            _logger.LogDebug("Deleting article...");

            _libraryAppDataRepository.Delete(article);
            await _libraryAppDataRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Search articles by release date
        /// </summary>
        /// <param name="releaseDate"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ArticleDto>>> SearchByReleaseDate(DateTime releaseDate)
        {
            var results = await _libraryAppDataRepository.GetArticlesByReleaseDateAsync(releaseDate);

            if (!results.Any())
            {
                return NotFound();
            }

            return _mapper.Map<List<ArticleDto>>(results);
        }

        private string CreateArticlesResourceUri(ArticlesResourceParameters articlesResourceParameters, ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetArticles",
                      new
                      {
                          pageNumber = articlesResourceParameters.PageNumber - 1,
                          pageSize = articlesResourceParameters.PageSize,
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetArticles",
                      new
                      {
                          pageNumber = articlesResourceParameters.PageNumber + 1,
                          pageSize = articlesResourceParameters.PageSize,
                      });

                default:
                    return Url.Link("GetArticles",
                    new
                    {
                        pageNumber = articlesResourceParameters.PageNumber,
                        pageSize = articlesResourceParameters.PageSize,
                    });
            }
        }
    }
}