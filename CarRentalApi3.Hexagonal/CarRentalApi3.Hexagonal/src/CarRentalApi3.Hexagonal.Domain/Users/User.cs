using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi3.Hexagonal.Domain.Abstractions;
using CarRentalApi3.Hexagonal.Domain.Rentals;

namespace CarRentalApi3.Hexagonal.Domain.Users
{
    public class User : Entity<Guid>
    {
        private User() { }
        private User(Guid id, string username, string email)
        {
            Username = username;
            Email = email;
            Id = id;
        }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static User Create(string username, string email)
        {
            var user = new User(Guid.NewGuid(), username, email);
            return user;
        }

        
    }
}