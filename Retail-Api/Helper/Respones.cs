namespace Retail_Api.Helper
{
    public class ResponesBody<T> where T : class
    {
        public bool IsTure { get; set; } //issuc
        public string Massage { get; set; }
        public T entity { get; set; } //data
    }
}
