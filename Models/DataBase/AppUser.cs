using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class AppUser
{
    public int IdAppUser { get; set; }

    public string EmailUser { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<BusinessUnitAppUser> BusinessUnitAppUsers { get; set; } = new List<BusinessUnitAppUser>();

    public virtual ICollection<TransferRequest> TransferRequestIdRequesterNavigations { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestIdUserExecutorNavigations { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequestSource> TransferRequestSources { get; set; } = new List<TransferRequestSource>();
}
