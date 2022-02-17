using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWebAPI.DTOs.RolesDTOs
{
    public class AddRolesDto
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
    }
}
