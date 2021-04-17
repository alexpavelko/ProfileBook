using SQLite;
using System;

namespace ProfileBook.Models
{
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }        
        public string NickName { get; set; }
        public string Name { get; set; }
        public string CreationTime { get; set; }
        public string ProfileImage { get; set; }
    }
}
