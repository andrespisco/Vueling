using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Person.Data.Contract.Entities;
using VY.Person.Dtos;

namespace VY.Person.Business.Impl.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonEntity, PersonDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(entity => entity.LastName))
                .ReverseMap();

        }
    }
}
