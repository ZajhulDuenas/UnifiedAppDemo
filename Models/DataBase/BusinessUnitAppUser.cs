using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class BusinessUnitAppUser
{
    public int Id { get; set; }

    public int? IdAppUserRole { get; set; }

    public int? IdAppUser { get; set; }

    public int IdBusinessUnit { get; set; }

    public decimal? TopAmount { get; set; }

    public virtual AppUser? IdAppUserNavigation { get; set; }

    public virtual AppUserRole? IdAppUserRoleNavigation { get; set; }

    public virtual BusinessUnit IdBusinessUnitNavigation { get; set; } = null!;
}
