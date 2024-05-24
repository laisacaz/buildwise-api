﻿namespace BuildWise.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RegisteredNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
