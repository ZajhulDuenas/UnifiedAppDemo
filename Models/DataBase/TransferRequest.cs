using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class TransferRequest
{
    public int IdRequest { get; set; }

    public int IdRequester { get; set; }

    public int IdCtaContDestination { get; set; }

    public int MonthDestination { get; set; }

    public decimal? InAmount { get; set; }

    public string? Justification { get; set; }

    public int IdUserExecutor { get; set; }

    public DateOnly RequestDate { get; set; }

    public DateOnly StatusChangeDate { get; set; }

    public int Status { get; set; }

    public virtual CecoCtasCont IdCtaContDestinationNavigation { get; set; } = null!;

    public virtual AppUser IdRequesterNavigation { get; set; } = null!;

    public virtual AppUser IdUserExecutorNavigation { get; set; } = null!;

    public virtual ICollection<TransferRequestSource> TransferRequestSources { get; set; } = new List<TransferRequestSource>();
}
