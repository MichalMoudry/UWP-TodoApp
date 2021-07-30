namespace TodoApp.Shared.Models.Entity
{
    public class Todo : Extensions.Entity
    {
        public bool IsCompleted { get; set; }

        public string Name { get; set; }
    }
}