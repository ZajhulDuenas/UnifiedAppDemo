using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class CatRol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public bool? Crear { get; set; }

    public bool? Actualizar { get; set; }

    public bool? Eliminar { get; set; }

    public bool? Listar { get; set; }

    public bool? Importar { get; set; }

    public bool? Exportar { get; set; }

    public virtual ICollection<CatUsuario> CatUsuarios { get; set; } = new List<CatUsuario>();
}
