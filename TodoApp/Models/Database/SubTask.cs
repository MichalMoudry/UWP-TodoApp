using TodoApp.Models.Extensions;

namespace TodoApp.Models.Database
{
    public class Subtask : Entity
    {
        public int TodoID { get; set; }
        public bool IsCompleted { get; set; }
    }
}