using System;
using AgilityWall.Core.Infrastructure;

namespace AgilityWall.Core.Contracts
{
    public class Task
    {
        public Task()
        {
            Created = Clock.Now();
        }

        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public double Effort { get; set; }
    }
}