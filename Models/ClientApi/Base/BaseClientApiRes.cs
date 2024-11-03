namespace Models.ClientApi.Base
{
    public abstract class BaseClientApiRes<T> where T : class
    {
        public T Payload { get; set; }
        public List<object> errorsModel { get; set; }
        public List<object> errors { get; set; }
        public int statusCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}
