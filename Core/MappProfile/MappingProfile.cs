using AutoMapper;
using Core.Entities;
using Core.Models;
using System;

namespace Core.MappProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<MobileScannedItem, MobileScannedItemModel>()
         .ReverseMap();
           
        }
    }
}
