using SQLite;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class User : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
