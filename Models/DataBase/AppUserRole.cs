using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class AppUserRole
{
    public int IdAppUserRole { get; set; }

    public string? NameUserRole { get; set; }

    public virtual ICollection<BusinessUnitAppUser> BusinessUnitAppUsers { get; set; } = new List<BusinessUnitAppUser>();
}
