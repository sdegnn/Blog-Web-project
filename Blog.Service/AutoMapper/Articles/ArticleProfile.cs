using AutoMapper;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Articles
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            //articledto profile oluşturma
            CreateMap<ArticleDto, Article>().ReverseMap(); //articledto yu article a mapleyebilir ya da tam tersi yapıllabilir.Reverse map article ı  da dto ya maplemeye yarar.
            CreateMap<ArticleUpdateDto, Article>().ReverseMap(); 
            CreateMap<ArticleUpdateDto,ArticleDto>().ReverseMap(); //article hem articledtodan da mapp işlemi yaptığı için bunu belirtmemiz gerekir.
            CreateMap<ArticleAddDto, Article>().ReverseMap();


        }
        //bu yapınında dependecny injection olmalı service katmanında (service layer extension)

    }
}
