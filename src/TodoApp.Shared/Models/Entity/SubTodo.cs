namespace TodoApp.Shared.Models.Entity
{
    public class SubTodo : Extensions.Entity
    {
        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public string TodoId { get; set; }
    }
}