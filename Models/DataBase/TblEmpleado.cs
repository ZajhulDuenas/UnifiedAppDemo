using System;
using System.Collections.Generic;

namespace infrastructure.DataBase;

public partial class TblEmpleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? Rfc { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
}
