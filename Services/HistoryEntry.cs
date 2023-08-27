using System;
using System.Collections.Generic;

namespace RecipeManagementSystem.Models
{
    public class HistoryEntry
    {
        public string CommandName { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Recipes { get; set; }
    }
}
