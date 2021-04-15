using System;

namespace ProfileBook.Models
{
    class ProfileModel
    {
        public int Id { get; set; }        
        public string NickName { get; set; }
        public string Name { get; set; }
        public string CreationTime { get; set; }
        public string ProfileImage { get; set; }
    }
}
