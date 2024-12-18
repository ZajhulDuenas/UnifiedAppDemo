using System;
using System.Collections.Generic;

namespace Api.Models.DataBase;

public partial class TblEmpleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? Rfc { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
}
