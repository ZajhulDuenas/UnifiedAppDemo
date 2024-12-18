using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request
{
    public class PaginationRequest
    {
        [Required(ErrorMessage = "pageIndex Requerido")] // Campo obligatorio
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Paginacion debe ser mayor a 0")]
        public int pageIndex { get; set; } = 1;
        [Required(ErrorMessage = "pageSize Requerido")] // Campo obligatorio
        [Range(minimum: 1, maximum: 50, ErrorMessage = "Paginacion debe ser mayor a 0")]
        public int pageSize { get; set; } = 10;
    }
}
