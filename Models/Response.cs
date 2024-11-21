namespace Api.Models
{
   public class Response<T> : Response
   where T : class
    {
        public T? Payload { get; set; }

        public IList<ErrorModel> ErrorsModel { get; } = [];

        public void AddError(ErrorModel error)
        {
            if (error == null)
                return;

            ErrorsModel.Add(error);
        }

        public Response<T> AddError(string message, int statusCode = -101, T? payLoad = default)
        {
            return AddError(message, null, statusCode, payLoad);
        }

        public Response<T> AddError(string message, string? descError, int statusCode = -101, T? payLoad = default)
        {
            this.Message = message;
            if (payLoad != null)
                this.Payload = payLoad;
            this.StatusCode = statusCode;
            this.Success = false;

            if (!string.IsNullOrEmpty(descError))
                Errors.Add(descError);

            return this;
        }

        public Response<T> AddPayload(T payLoad)
        {
            this.Payload = payLoad;

            return this;
        }

        public Response<T> AddStatusCode(int statusCode)
        {
            this.StatusCode = statusCode;
            return this;
        }
    }

    public class Response : ResponseBase
    {
        public IList<string> Errors { get; } = [];

        public int StatusCode { get; set; }

        public virtual Response AddError(string message, string? descError = null, int statusCode = -101)
        {
            this.Message = message;
            this.Success = false;
            this.StatusCode = statusCode;

            if (!string.IsNullOrEmpty(descError))
                Errors.Add(descError);

            return this;
        }

        public virtual Response AddErrors(string descError, int statusCode = -101)
        {
            this.Success = false;
            this.StatusCode = statusCode;

            if (!string.IsNullOrEmpty(descError))
                Errors.Add(descError);

            return this;
        }
    }

    public class ResponseBase
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "La operacion ha sido completada";
    }

    public class ErrorModel
    {
        public string? Column { get; set; }
        public string? Value { get; set; }
        public string? Error { get; set; }
    }
}
