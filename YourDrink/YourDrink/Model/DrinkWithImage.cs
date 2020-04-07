using System;
using SQLite;
namespace YourDrink.Model
{
    public class DrinkWithImage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public int CategoryId { get; set; }

        public string Image { get; set; }

        public DrinkWithImage()
        {

    }   
    }
}
