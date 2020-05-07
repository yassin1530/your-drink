using System;
using SQLite;
namespace YourDrink.Model
{
    public class Recept
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int DrinkId { get; set; }
        [NotNull]
        public string Name { get; set; }

        public string Amount { get; set; }
   

        public Recept()
        {
        }
    }
}
