using Northwind.Core;
using Northwind.Core.Organization;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace Northwind.Wcf.Dtos
{
    public class TerritoryDto
    {
        /// <summary>
        /// Transfers the territory entity's property values to the DTO.
        /// Strongly consider Jimmy Bogard's AutoMapper (http://automapper.codeplex.com/) 
        /// for doing this kind of work in a more automated fashion.
        /// </summary>
        public static TerritoryDto Create(Territory territory) {
            if (territory == null)
                return null;

            TerritoryDto territoryDto = new TerritoryDto();
            territoryDto.Id = territory.Id;
            territoryDto.RegionBelongingTo = RegionDto.Create(territory.RegionBelongingTo);
            territoryDto.Description = territory.Description;

            foreach (Employee employee in territory.Employees) {
                territoryDto.Employees.Add(EmployeeDto.Create(employee));
            }

            return territoryDto;
        }

        private TerritoryDto() {
            Employees = new List<EmployeeDto>();
        }

        public string Id { get; set; }
        public RegionDto RegionBelongingTo { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// I'd prefer to have a protected setter, but since we need it to be XML-serializable, the setter must be public
        /// </summary>
        //[XmlArray("Employees")]
        //[XmlArrayItem("Employee", typeof(EmployeeDto))]
        public List<EmployeeDto> Employees { get; set; }
    }
}
