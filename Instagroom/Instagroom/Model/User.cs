using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagroom.Model
{
    public class User
    {

        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }
        
        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

    }
}
