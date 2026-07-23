namespace Entities;

public static class RolePermissions
{
    public static readonly Dictionary<string, string[]> RolePermissionsMap = new()
    {
        //example, TODO: expand
        [Roles.Admin] = Permissions.All,
        [Roles.Editor] =
        [
            Permissions.MoviesCreate,
            Permissions.MoviesRead,
            Permissions.MoviesUpdate,
            Permissions.MoviesDelete,
            Permissions.GenresCreate,
            Permissions.GenresRead,
            Permissions.GenresUpdate,
            Permissions.GenresDelete,
        ],
        [Roles.Viewer] =
        [
            Permissions.MoviesRead,
            Permissions.CastCrewRead,
            Permissions.CollectionsRead,
            Permissions.GenresRead,
        ],
    };
}
