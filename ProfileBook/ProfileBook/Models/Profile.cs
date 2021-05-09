using SQLite;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string CreationTime { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
