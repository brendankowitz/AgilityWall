using System;
using System.Collections.Generic;
using AgilityWall.Core.Infrastructure;

namespace AgilityWall.Core.Contracts
{
    public class Column
    {
        public Column()
        {
            Tasks = new List<Task>();
            Created = Clock.Now();
        }

        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public IList<Task> Tasks { get; set; }
    }
}