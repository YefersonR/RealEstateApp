using AutoMapper;
using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Estates.Commands.CreateEstates;
using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetAllEstatesQuery, GetAllEstatesParameters>()
                .ReverseMap();
            CreateMap<Estate, EstateRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt=>opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<Estate, CreateEstateCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

        }
    }
}
