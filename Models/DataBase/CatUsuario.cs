namespace Models.DataBase;

public partial class CatUsuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public string? Password { get; set; }

    public int? IdRol { get; set; }

    public virtual CatRol? IdRolNavigation { get; set; }
}
