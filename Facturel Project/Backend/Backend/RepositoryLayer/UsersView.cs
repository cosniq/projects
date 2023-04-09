using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class UsersView
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
