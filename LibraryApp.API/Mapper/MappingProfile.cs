using AutoMapper;
using LibraryApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(a => a.Author, opt => opt.MapFrom(s => s.Author.Name))
                .ReverseMap();

            CreateMap<ArticleForCreationDto, Article>().ReverseMap();
            CreateMap<ArticleForUpdateDto, Article>().ReverseMap();

            CreateMap<Book, BookDto>()
               .ForMember(a => a.Author, opt => opt.MapFrom(s => s.Author.Name))
               .ReverseMap();
        }
    }
}
