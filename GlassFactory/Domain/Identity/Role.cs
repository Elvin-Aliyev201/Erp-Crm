using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public sealed class Role:AuditableEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        private readonly List<Permission> _permissions = new List<Permission>();
        private Role()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public Role(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Role name cannot be empty");
            }
            Description = description;
            
        }


        public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();


        public void GrantPermission(Permission permission)
        {
            ArgumentNullException.ThrowIfNull(permission);

            if (_permissions.Any(p => p.Code == permission.Code))
            {
                return;
            }

            _permissions.Add(permission);
        }

        public void RevokePermission(string permissionCode)
        {
         
            var existingPermission = _permissions.FirstOrDefault(p => p.Code == permissionCode);
            if (existingPermission  is not null)
                _permissions.Remove(existingPermission);

        }

    }
}
