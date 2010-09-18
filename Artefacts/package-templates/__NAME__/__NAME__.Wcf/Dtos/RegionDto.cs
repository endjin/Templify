using Northwind.Core;
using SharpArch.Core;

namespace Northwind.Wcf.Dtos
{
    /// <summary>
    /// DTO for a region entity.
    /// </summary>
    public class RegionDto
    {
        /// <summary>
        /// Transfers the region entity's property values to the DTO.
        /// Strongly consider Jimmy Bogard's AutoMapper (http://automapper.codeplex.com/) 
        /// for doing this kind of work in a more automated fashion.
        /// </summary>
        public static RegionDto Create(Region region) {
            if (region == null)
                return null;

            return new RegionDto() {
                Id = region.Id,
                Description = region.Description
            };
        }

        private RegionDto() { }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}
