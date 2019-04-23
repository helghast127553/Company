using AutoMapper;
using Company.Domain;
using Company.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.WebUI.Infrastructure
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<EmployeeDomainModel, EmployeeViewModel>();
            CreateMap<EquipmentDomainModel, EquipmentViewModel>();
            CreateMap<EmployeeViewModel, EmployeeDomainModel>();
            CreateMap<EquipmentViewModel, EquipmentDomainModel>();
        }

        public static void Run()
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperWebProfile>());
        }
    }
}