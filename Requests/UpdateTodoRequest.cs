namespace TodoApi.Requests
{
    public class UpdateTodoRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
    }
}