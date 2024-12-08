namespace Todo.Models
{
    public class TodoModel
    {
        public TodoModel() { Title = "Não definido"; }
        public TodoModel(string title)
        {
            Title = title;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
