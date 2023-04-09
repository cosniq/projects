using System;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public partial class UsersWithDetailsView
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
