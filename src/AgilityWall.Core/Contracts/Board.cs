using System;
using System.Collections.Generic;
using AgilityWall.Core.Infrastructure;

namespace AgilityWall.Core.Contracts
{
    public class Board
    {
        public Board()
        {
            Columns = new List<Column>();
            Created = Clock.Now();
        }

        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public IList<Column> Columns { get; set; }

    }
}