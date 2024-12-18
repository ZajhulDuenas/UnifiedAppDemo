using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class BusinessUnit
{
    public int IdBusinessUnit { get; set; }

    public virtual ICollection<BusinessUnitAppUser> BusinessUnitAppUsers { get; set; } = new List<BusinessUnitAppUser>();
}
