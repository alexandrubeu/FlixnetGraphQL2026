namespace Entities;

public class EUser
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}

// Couldn't remember what we decided on. raw permissions or roles
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}

public class Permission
{
    public int Id { get; set; }

    // EG: movies:create,
    public string Name { get; set; } = string.Empty;
}
