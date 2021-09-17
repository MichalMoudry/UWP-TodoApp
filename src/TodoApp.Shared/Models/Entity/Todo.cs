using System;
using TodoApp.Shared.Enums;

namespace TodoApp.Shared.Models.Entity
{
    public class Todo : Extensions.Entity
    {
        public DateTime AlertDate { get; set; }

        public DateTime CompletetionDate { get; set; }

        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public TodoRepetition Repetition { get; set; }
    }
}