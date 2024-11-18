namespace Models.DataBase;

public partial class TransferRequestSource
{
    public int Id { get; set; }

    public int IdRequest { get; set; }

    public int IdCtaContSource { get; set; }

    public int MonthSource { get; set; }

    public decimal OutAmount { get; set; }
}
