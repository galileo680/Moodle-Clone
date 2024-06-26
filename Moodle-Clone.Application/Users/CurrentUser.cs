﻿namespace MoodleClone.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, string? Name, string? Surname)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
