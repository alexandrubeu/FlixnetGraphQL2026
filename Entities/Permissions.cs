namespace Entities;

public static class Permissions
{
    // example CRUD for current data; TODO: expand
    public const string MoviesCreate = "movies:create";
    public const string MoviesRead = "movies:read";
    public const string MoviesUpdate = "movies:update";
    public const string MoviesDelete = "movies:delete";

    public const string CollectionsCreate = "collections:create";
    public const string CollectionsRead = "collections:read";
    public const string CollectionsUpdate = "collections:update";
    public const string CollectionsDelete = "collections:delete";

    public const string GenresCreate = "genres:create";
    public const string GenresRead = "genres:read";
    public const string GenresUpdate = "genres:update";
    public const string GenresDelete = "genres:delete";

    public const string CastCrewCreate = "castcrew:create";
    public const string CastCrewRead = "castcrew:read";
    public const string CastCrewUpdate = "castcrew:update";
    public const string CastCrewDelete = "castcrew:delete";

    // for now, done by hand, later maybe generate based on reflection?
    public static readonly string[] All =
    [
        MoviesCreate,
        MoviesRead,
        MoviesUpdate,
        MoviesDelete,
        CollectionsCreate,
        CollectionsRead,
        CollectionsUpdate,
        CollectionsDelete,
        GenresCreate,
        GenresRead,
        GenresUpdate,
        GenresDelete,
        CastCrewCreate,
        CastCrewRead,
        CastCrewUpdate,
        CastCrewDelete,
    ];
}
