using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.Identity
{
    public sealed class User: AuditableEntity
    {
        public string UserName { get; private set; }=default!;

        public string FullName { get; private set; } = default!;

        public string PasswordHash { get; private set; } = default!;

        public bool IsActive { get; private set; }

        private readonly List<Role> _roles = new List<Role>();

        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();


        private User()
        {
            UserName = string.Empty;
            FullName = string.Empty;
            PasswordHash = string.Empty;
            
        }

        private User(string userName, string fullName, string passwordHash)
        {
            UserName = userName;
            FullName = fullName;
            PasswordHash = passwordHash;
            IsActive = true;
        }

        public static User Create(string userName, string fullName, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new DomainException("User name cannot be empty");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name cannot be empty");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash cannot be empty");

            return new User(userName.Trim(), fullName.Trim(), passwordHash);
        }


        public void AssignRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role, nameof(role));
            if (_roles.Any(r => r.Id == role.Id))
            {
                return;
            }
            _roles.Add(role);
        }


        public void RemoveRole(Guid roleId)
        {
            var existingRole = _roles.FirstOrDefault(r => r.Id == roleId);
            if (existingRole is not null)
            {
                _roles.Remove(existingRole);
            }
        }


        public void ChangePasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new DomainException("New password hash cannot be empty");
            PasswordHash = newPasswordHash.Trim();
        }

        public void Activate()
        {
           if(IsActive)
                throw new DomainException("User is already active");
            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new DomainException("User is already inactive");
            IsActive = false;
        }


        public IReadOnlyCollection<string> GetAllPermissionCodes() =>

         _roles.SelectMany(r => r.Permissions)
            .Select(p => p.Code)
            .Distinct()
            .ToArray();

    }
}
