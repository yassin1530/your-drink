using System;
using SQLite;
namespace YourDrink.Model
{
    public class DrinkDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int DrinkId { get; set; }

        public string Content { get; set; }

        public string Making { get; set; }

        public string Image { get; set; }

        [MaxLength(5)]
        public int Rating { get; set; }

        public bool Favorite { get; set; }


        public DrinkDetail()
        {
        }
    }
}
