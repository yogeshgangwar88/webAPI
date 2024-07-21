namespace WebAPI.Models
{
    public class CustomResponse<T>
    {
        public CustomResponse()
        {
            this.dataList = new List<T>();
        }
        public int statusCode { get; set; }
        public string? statusMessage { get; set; }

        public List<T> dataList { get; set; }

    }
}
