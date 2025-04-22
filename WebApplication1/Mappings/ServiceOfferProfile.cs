using AutoMapper;
using AutoServiceApi.Models.DataTransferObjects;
using WebApplication1.AutoServiceApi.Models;

namespace AutoServiceApi.Mappings;

public class ServiceOfferProfile : Profile
{
    public ServiceOfferProfile()
    {
        CreateMap<ServiceOffer, ServiceOfferResponseDto>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
    }
}

