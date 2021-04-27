namespace TodoApi.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
    }
}