using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class TransferRequestSource
{
    public int Id { get; set; }

    public int IdRequest { get; set; }

    public int IdCtaContSource { get; set; }

    public int MonthSource { get; set; }

    public decimal? OutAmount { get; set; }

    public string? Comments { get; set; }

    public int IdUserApprover { get; set; }

    public DateOnly StatusChangeDate { get; set; }

    public int Status { get; set; }

    public virtual CecoCtasCont IdNavigation { get; set; } = null!;

    public virtual TransferRequest IdRequestNavigation { get; set; } = null!;

    public virtual AppUser IdUserApproverNavigation { get; set; } = null!;
}
