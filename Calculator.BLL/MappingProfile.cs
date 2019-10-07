using AutoMapper;
using Calculator.DTO;
using Calculator.Models;
using System.Collections.Generic;

namespace Calculator.BLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ExampleDTO, Example>();
            CreateMap<Example, ExampleDTO>();
            //CreateMap<IEnumerable<ExampleDTO>, IEnumerable<Example>>().ReverseMap();
        }
    }
}
