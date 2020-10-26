﻿namespace Git.Services
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Data;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Username = username,
                Password = ComputeHash(password),
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            return user.Id;
        }

        public bool IsEmailAvailable(string email)
            => !this.db.Users.Any(x => x.Email == email);


        public string GetUserId(string username, string password)
           => this.db.Users
            .Where(x => x.Username == username && x.Password == ComputeHash(password))
            .Select(x => x.Id)
            .FirstOrDefault();

        public bool IsUsernameAvailable(string username)
            => !this.db.Users.Any(x => x.Username == username);


        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
