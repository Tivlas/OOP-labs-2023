﻿using FinanceManagementAppCore.Interfaces;

namespace FinanceManagementAppCore
{
    public class User : IEntity
    {
        private static int s_IdController = 0;

        public User(string email, string password)
        {
            Email = email;
            Password = password;
            Id = s_IdController;
            ++s_IdController;
        }

        public int Id { get; init; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}