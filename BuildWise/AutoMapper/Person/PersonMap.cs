using AutoMapper;
using BuildWise.DTO.Address;
using BuildWise.DTO.Person;
using BuildWise.Entities;
using BuildWise.Payload.Address;
using BuildWise.Payload.Person;

namespace BuildWise.AutoMapper.Person
{
    public class PersonMap : Profile
    {
        public PersonMap()
        {
            CreateMap<PersonInsertPayload, Entities.Person>();
            CreateMap<PersonUpdatePayload, Entities.Person>();
            CreateMap<Entities.Person, PersonDTO>();


            CreateMap<AddressInsertPayload, Address>();
            CreateMap<AddressUpdatePayload, Address>();
            CreateMap<Entities.Address, AddressDTO>();
        }
    }
}
