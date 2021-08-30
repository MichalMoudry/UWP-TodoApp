using System;

namespace TodoApp.Shared.Models.Entity
{
    public class Todo : Extensions.Entity
    {
        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public DateTime AlertDate { get; set; }
    }
}