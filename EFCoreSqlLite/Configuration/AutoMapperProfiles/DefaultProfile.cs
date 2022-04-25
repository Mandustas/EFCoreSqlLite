using AutoMapper;
using EFCoreSqlLite.Dto;
using EFCoreSqlLite.Model;

namespace EFCoreSqlLite.Profiles
{
    /// <summary>
    /// Default profile for AutoMapper
    /// </summary>
    /// <remarks>See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#automapper</remarks>
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<BookCreateDto, Book>();
            CreateMap<AuthorCreateDto, Author>();
        }
    }
}
