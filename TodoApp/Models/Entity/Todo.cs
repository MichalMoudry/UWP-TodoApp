using System.Collections.Generic;

namespace TodoApp.Models.Entity
{
    public class Todo : Extensions.Entity
    {
        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public List<SubTodo> SubTodos { get; set; }
    }
}