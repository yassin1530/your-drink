using System;
using SQLite;
namespace YourDrink.Model
{
    public class CategoryCount
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Icon { get; set; }

        public int Count { get; set; }
        public CategoryCount()
        {

        }
    }
}
