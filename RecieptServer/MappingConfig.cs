using AutoMapper;
using ReceiptServer.Models;
using RecieptServer.Models;

namespace RecieptServer
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Article
            CreateMap<ArticleDTO, Article>()
                .ForMember(dest => dest.ReceiptArticles, opt => opt.MapFrom(src => src.ReceiptArticles));

            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.ReceiptArticles, opt => opt.MapFrom(src => src.ReceiptArticles));


            // ReceiptArticle: DTO -> entity
            CreateMap<ReceiptArticleDTO, ReceiptArticle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Article, opt => opt.Ignore())
                .ForMember(dest => dest.Receipt, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.ArticleId))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            // ReceiptArticle: entity -> DTO
            CreateMap<ReceiptArticle, ReceiptArticleDTO>()
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.ArticleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Article != null ? src.Article.Name : string.Empty))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            // Receipt
            CreateMap<ReceiptDTO, Receipt>()
                .ForMember(dest => dest.ReceiptArticles, opt => opt.MapFrom(src => src.ReceiptArticles));
            CreateMap<Receipt, ReceiptDTO>();
        }
    }
}
