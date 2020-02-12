using System;
using SQLite;

namespace YourDrink.Model
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Icon { get; set; }


        public Category()
        {
        }
    }
}
