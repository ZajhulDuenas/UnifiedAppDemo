using System;
using System.Collections.Generic;

namespace Models.DataBase;

public partial class TransferRequest
{
    public int IdRequest { get; set; }

    public int IdRequester { get; set; }

    public int IdCtaContDestination { get; set; }

    public int MonthDestination { get; set; }

    public decimal InAmount { get; set; }

    public string Justification { get; set; } = null!;

    public int IdUserApprover { get; set; }

    public int IdUserExecutor { get; set; }

    public DateOnly RequestDate { get; set; }

    public DateOnly StatusChangeDate { get; set; }

    public int Status { get; set; }

    public string Comments { get; set; } = null!;
}
