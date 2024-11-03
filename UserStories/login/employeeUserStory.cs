using infrastructure.DataBase;
using interfaces.DataBase;
using Models;
using Models.DTOs;
using common;
using ClosedXML.Excel;

namespace UserStories.login
{
    public class employeeUserStory(IMyUnitOfWork unitOfWork) : IemployeeUserStory
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;

        public async Task<Response<List<TblEmpleado>>> GetEmployeList()
        {
            Response<List<TblEmpleado>> result = new Response<List<TblEmpleado>>();
  
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var employeList = await RepoEmpleado.SearchAllAsync().ConfigureAwait(false);

            if (employeList == null)
            {
                result.StatusCode = 400;
                result.Message = "No se cuenta con empleados";

                return result;
            }

            result.StatusCode = 200;
            result.Message = "ok";
            result.Payload = employeList.ToList();

            return result;
        }

        public async Task<Response<List<TblEmpleado>>> GetEmployeList(PaginationDto request)
        {
            Response<List<TblEmpleado>> result = new Response<List<TblEmpleado>>();

            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var employeList = await RepoEmpleado.SearchPageAsync(
                pageIndex: request.pageIndex, 
                pageSize: request.pageSize, 
                predicate: x => true,
                orderby: x => x.IdEmpleado,
                desc: false).ConfigureAwait(false);

            if (employeList == null)
            {
                result.StatusCode = 400;
                result.Message = "No se cuenta con empleados";

                return result;
            }

            result.StatusCode = 200;
            result.Message = "ok";
            result.Payload = employeList.Items.ToList();

            return result;
        }


        public async Task<Response<TblEmpleado>> GetEmployeById(EmployeeIdDto request)
        {
            Response<TblEmpleado> result = new Response<TblEmpleado>();
            // check user exist on database
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();


            var employee = await RepoEmpleado.FirstOrDefaultAsync(x => x.IdEmpleado == request.EmployeeId).ConfigureAwait(false);

            if (employee == null)
            {
                result.StatusCode = 400;
                result.Message = "Empleado no encontrado";

                return result;
            }


            result.StatusCode = 200;
            result.Message = "ok";
            result.Payload = employee;

            return result;
        }

        public async Task<Response<TblEmpleado>> AddEmployee(EmployeeDto request)
        {
            Response<TblEmpleado> result = new Response<TblEmpleado>();
            // check user exist on database
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var employeList = await RepoEmpleado.AddAsync(new TblEmpleado()
            {
                FechaNacimiento = DateOnly.Parse(request.FechaNacimiento.ToString("yyyy-MM-dd")),
                Nombre = request.Nombre,
                Rfc = request.Rfc
            }).ConfigureAwait(false);

            var res = await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var employee = await RepoEmpleado.FirstOrDefaultAsync(x => x.IdEmpleado == res).ConfigureAwait(false);

            result.StatusCode = 201;
            result.Message = "Created";
            result.Payload = employee;

            return result;
        }

        public async Task<Response<TblEmpleado>> DeleteEmployee(EmployeeIdDto request)
        {
            Response<TblEmpleado> result = new Response<TblEmpleado>();
            // check user exist on database
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var response = await GetEmployeById(request).ConfigureAwait(false);

            if (response.StatusCode >= 400) return response;

            var Empleado = response.Payload;

            var employeList = await RepoEmpleado.RemoveAsync(x => x.IdEmpleado == request.EmployeeId).ConfigureAwait(false);

            var res = await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            result.StatusCode = 201;
            result.Message = "Done";
            result.Payload = Empleado;

            return result;
        }

        public async Task<Response<TblEmpleado>> ModifyEmployee(Employee2Dto request)
        {
            Response<TblEmpleado> result = new Response<TblEmpleado>();
            // check user exist on database
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var response = await GetEmployeById(new EmployeeIdDto() { EmployeeId = request.EmployeeId }).ConfigureAwait(false);

            if (response.StatusCode >= 400 || response.Payload == null) return response;

            var employeeModel = response.Payload;
            var isEqual = (employeeModel.Nombre == request.Nombre ||
                           employeeModel.Rfc == request.Rfc ||
                           employeeModel.FechaNacimiento == DateOnly.Parse(request.FechaNacimiento.ToString("yyyy-MM-dd"))
                          );

            if (isEqual)
            {
                result.StatusCode = 400;
                result.Message = "No se identificaron cambios";

                return result;
            }

            var res1 = await RepoEmpleado.UpdateAsync(x => x.IdEmpleado == request.EmployeeId,
                                                             u1 => u1
                                                             .SetProperty(x => x.Nombre, request.Nombre)
                                                             .SetProperty(x => x.Rfc, request.Rfc)
                                                             .SetProperty(x => x.FechaNacimiento, DateOnly.Parse(request.FechaNacimiento.ToString("yyyy-MM-dd")))
                                                            )
                                                .ConfigureAwait(false);

            var res = await unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var employee = await GetEmployeById(new EmployeeIdDto() { EmployeeId = request.EmployeeId }).ConfigureAwait(false);

            result.StatusCode = 201;
            result.Message = "Done";
            result.Payload = employee.Payload;

            return result;
        }

        public async Task<Response<FileStreamDto>> ExportData()
        {
            Response<FileStreamDto> response = new Response<FileStreamDto>();

            var responseData = await GetEmployeList();

            if (responseData.StatusCode >= 400) return new Response<FileStreamDto>() { StatusCode = 400, Message = responseData.Message };

            var data = responseData.Payload;
            int row = 1;

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Datos");

            // Agregar algunos datos de ejemplo
            worksheet.Cell(row, 1).Value = "num";
            worksheet.Cell(row, 2).Value = "Nombre";
            worksheet.Cell(row, 3).Value = "RFC";
            worksheet.Cell(row, 4).Value = "FechaNacimiento";

            data.ToList().ForEach(x =>
            {
                row++;
                worksheet.Cell(row, 1).Value = x.IdEmpleado;
                worksheet.Cell(row, 2).Value = x.Nombre;
                worksheet.Cell(row, 3).Value = x.Rfc;
                worksheet.Cell(row, 4).Value = x.FechaNacimiento != null ? x.FechaNacimiento.Value.ToString("yyyy-MM-dd") : "";
            });


            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = "Data_Export.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            response.Message = "OK";
            response.StatusCode = 200;
            response.Success = true;
            response.Payload = new FileStreamDto()
            {
                fileName = fileName,
                mimeType = mimeType,
                stream = stream
            };

            return response;

        }

        public async Task<Response> ImportData(MemoryStream stream)
        {
            Response result = new Response();

            List<TblEmpleado> employeList = new List<TblEmpleado>();
            // var data = new List<Dictionary<string, string>>(); // Para almacenar los datos del archivo Excel

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1); // Usa la primera hoja de cálculo

            // Suponiendo que la primera fila contiene los nombres de las columnas
            var headers = new List<string>();
            foreach (var cell in worksheet.Row(1).CellsUsed())
            {
                headers.Add(cell.GetString());
            }

            // Leer el resto de las filas
            foreach (var row in worksheet.RowsUsed().Skip(1)) // Empieza en la segunda fila
            {
                var rowData = new Dictionary<string, string>();

                for (int i = 0; i < headers.Count; i++)
                {
                    var cellValue = row.Cell(i + 1).GetString(); // Lee cada celda de la fila
                    rowData[headers[i]] = cellValue; // Asocia el valor a la columna correspondiente
                }
             
                employeList.Add(TblEmpleadoFromDiccionary(rowData));

               // data.Add(rowData);
            }

            //var EmplRepository = unitOfWork.Repository<TblEmpleado>();

            await unitOfWork.BulkInsertAsync(employeList);

            result.Success = true;
            result.Message = "Done";
            result.StatusCode = 200;

            return result;
        }

        private TblEmpleado TblEmpleadoFromDiccionary(Dictionary<string, string> data)
        {
            TblEmpleado result = new TblEmpleado();

            if (data.ContainsKey("num"))
            {
                result.IdEmpleado = Convert.ToInt32(data["num"]);
            }

            if (data.ContainsKey("Nombre"))
            {
                result.Nombre = data["Nombre"] as string;
            }

            if (data.ContainsKey("RFC"))
            {
                result.Rfc = data["RFC"] as string;
            }

            if (data.ContainsKey("FechaNacimiento"))
            {
                var datet = DateTime.Parse(data["FechaNacimiento"] as string).ToString("yyy-MM-dd");

                result.FechaNacimiento = data["FechaNacimiento"] != null ? DateOnly.Parse(datet) : null ;
            }

            return result;
        }

    }

}
