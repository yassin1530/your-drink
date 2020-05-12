using System;
using SQLite;
namespace YourDrink.Model
{
    public class Drink
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int CategoryId { get; set; }

   

        public Drink()
        {
        }
    }
}
