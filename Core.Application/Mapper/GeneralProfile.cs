using AutoMapper;
using Core.Application.DTOS.Account;
using Core.Application.DTOS.Estates;
using Core.Application.Feactures.Estates.Commands.CreateEstates;
using Core.Application.Feactures.Estates.Commands.UpdateEstates;
using Core.Application.Feactures.Estates.Queries.GetAllEstates;
using Core.Application.Feactures.EstatesImgs.Commands.CreateEstateImg;
using Core.Application.Feactures.EstatesImgs.Commands.UpdateEstateImg;
using Core.Application.Feactures.EstatesImgs.Queries.GetAllEstateImgs;
using Core.Application.Feactures.EstateTypes.Commands.CreateEstateType;
using Core.Application.Feactures.EstateTypes.Commands.UpdateEstateType;
using Core.Application.Feactures.EstateTypes.Queries.GetAllEstateTypes;
using Core.Application.Feactures.Favorites.Commands.CreateFavorite;
using Core.Application.Feactures.Favorites.Queries.GetFavoritesById;
using Core.Application.Feactures.Feactures.Commands.CreateFeacture;
using Core.Application.Feactures.Feactures.Commands.CreateFeaturesEstates;
using Core.Application.Feactures.Feactures.Commands.UpdateFeacture;
using Core.Application.Feactures.Feactures.Queries.GetAllFeactures;
using Core.Application.Feactures.SellTypes.Commands.CreateSellType;
using Core.Application.Feactures.SellTypes.Commands.UpdateSellType;
using Core.Application.Feactures.SellTypes.Queries.GetAllSellTypes;
using Core.Application.ViewModels;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;


namespace Core.Application.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Estate Feactures
            CreateMap<GetAllEstatesQuery, GetAllEstatesParameters>()
                .ReverseMap();

            CreateMap<EstatesImg, CreateEstateImgCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<Estate, UpdateEstateCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.FeaturesIds, opt => opt.Ignore());


            CreateMap<FeaturesRelations, CreateFeaturesEstatesCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());


            CreateMap<Estate, EstateRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<Estate, FavoriteRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<Estate, CreateEstateCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<Estate, EstateRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());


            CreateMap<AgentesViewModel, AuthenticationResponse>()
                .ReverseMap();



            CreateMap<SellType, SellTypeRequest>()
                .ReverseMap()
                .ForMember(x => x.Estates, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            CreateMap<EstateType, EstateTypeRequest>()
                .ReverseMap()
                .ForMember(x => x.Estates, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<UpdateEstateCommand, UpdateEstateCommandResponse>()
              .ReverseMap();
            #endregion
            #region EstateImg Feactures
            CreateMap<GetAllEstateImgQuery, GetAllEstateImgParameters>()
                .ReverseMap();
            CreateMap<EstatesImg, EstateImgRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<Estate, CreateEstateImgCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<UpdateEstateImgCommand, UpdateEstateImgCommandResponse>()
              .ReverseMap();
            #endregion

            #region EstateType Feactures
            CreateMap<GetAllEstateTypesQuery, GetAllEstateTypesParameters>()
                .ReverseMap();
            CreateMap<EstateType, EstateTypeRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<EstateType, CreateEstateTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<UpdateEstateTypeCommand, UpdateEstateTypeCommandResponse>()
              .ReverseMap();
            #endregion
            #region Favorites Feactures
            CreateMap<GetFavoritesByIdQuery, GetFavoritesByIdParameters>()
                .ReverseMap();
            CreateMap<Favorite, FavoriteRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<Favorite, CreateFavoriteCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            #endregion

            #region Feactures Feactures
            CreateMap<GetAllFeacturesQuery, GetAllFeacturesParameters>()
                .ReverseMap();
            CreateMap<Feature, FeaturesRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<Feature, CreateFeactureCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<UpdateFeacturesCommand, UpdateFeacturesCommandResponse>()
              .ReverseMap();
            #endregion
            #region SellTypes Feactures
            CreateMap<GetAllSellTypesQuery, GetAllSellTypesParameters>()
                .ReverseMap();
            CreateMap<SellType, SellTypeRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<SellType, CreateSellTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());
            CreateMap<UpdateSellTypeCommand, UpdateSellTypeCommandResponse>()
              .ReverseMap();
            #endregion

            CreateMap<FeaturesRelations, FeaturesRelationsRequest>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Modified, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.Ignore());

            #region User
            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ForMember(x=>x.HasError,opt=>opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationRequest, LoginViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<UserSaveViewModel, AgentesViewModel>()
               .ReverseMap();
            #endregion
        }
    }
}
