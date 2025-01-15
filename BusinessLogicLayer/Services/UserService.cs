using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.IRepositorys;
using System;

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO Login(string username, string password)
        {
            ValidateLoginCredentials(username, password); 
            return _userRepository.GetUser(username, password);
        }

        public bool Register(string username, string password)
        {
            ValidateRegistrationCredentials(username, password); 

            if (_userRepository.UserExists(username))
            {
                throw new ArgumentException("De gebruikersnaam is al in gebruik. Kies een andere gebruikersnaam.");
            }

            return _userRepository.AddUser(username, password);
        }

        private void ValidateLoginCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("De gebruikersnaam mag niet leeg zijn.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Het wachtwoord mag niet leeg zijn.");
            }
        }

        private void ValidateRegistrationCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("De gebruikersnaam mag niet leeg zijn.");
            }

            if (username.Length < 3 || username.Length > 20)
            {
                throw new ArgumentException("De gebruikersnaam moet tussen de 3 en 20 tekens lang zijn.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Het wachtwoord mag niet leeg zijn.");
            }

            if (password.Length < 6)
            {
                throw new ArgumentException("Het wachtwoord moet minstens 6 tekens lang zijn.");
            }

            if (!HasUpperCase(password) || !HasDigit(password))
            {
                throw new ArgumentException("Het wachtwoord moet minimaal één hoofdletter en één cijfer bevatten.");
            }
        }

        private bool HasUpperCase(string input)
        {
            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasDigit(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
