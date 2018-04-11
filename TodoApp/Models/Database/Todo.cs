using System;
using TodoApp.Models.Extensions;

namespace TodoApp.Models.Database
{
    internal class Todo : Entity
    {
        public DateTime Added { get; set; }
    }
}