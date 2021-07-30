using System;

namespace TodoApp.Models.Extensions
{
    public abstract class Entity
    {
        public string Id { get; set; }

        public DateTime Added { get; set; }

        public DateTime Updated { get; set; }
    }
}