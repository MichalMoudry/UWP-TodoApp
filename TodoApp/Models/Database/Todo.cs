using TodoApp.Models.Extensions;

namespace TodoApp.Models.Database
{
    public class Todo : Entity
    {
        public int ListID { get; set; }
        public bool IsCompleted { get; set; }
    }
}