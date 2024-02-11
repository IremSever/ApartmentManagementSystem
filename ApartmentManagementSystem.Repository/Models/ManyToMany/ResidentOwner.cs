﻿using System.Security.AccessControl;

namespace ApartmentManagementSystem.Repository.Models.ManyToMany
{
    public class ResidentOwner
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Tenant>? Tenant { get; set; }
    }
}
