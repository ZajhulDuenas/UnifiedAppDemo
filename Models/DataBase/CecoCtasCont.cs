using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class CecoCtasCont
{
    public int Id { get; set; }

    public int IdCeco { get; set; }

    public string? CodeCeco { get; set; }

    public int IdCtaCont { get; set; }

    public string? CodeCtasCont { get; set; }

    public virtual TransferRequestSource? TransferRequestSource { get; set; }

    public virtual ICollection<TransferRequest> TransferRequests { get; set; } = new List<TransferRequest>();
}
