using BusinessLogic.Dtos;

namespace WebApi.GraphQL;

public static class Db
{
    public static List<DGenre> Genres { get; } =
    [
        new(1, "Drama"),
        new(2, "Horror"),
        new(3, "Action"),
        new(4, "Adventure"),
        new(5, "Comedy"),
        new(6, "Musical")
    ];

    public static List<DMovie> Movies { get; } =
    [
        new(1, "https://youtube.com/trailer1", true, new DateTime(2026, 1, 1), [Genres[0], Genres[2]])
        {
            Title = "The Last Hero",
            ImageUrl = "/images/last-hero.jpg",
            VideoSource = "/videos/last-hero.mp4"
        },

        new(2, "https://youtube.com/trailer2", true, new DateTime(2026, 1, 2), [Genres[1]])
        {
            Title = "Night Terrors",
            ImageUrl = "/images/night-terrors.jpg",
            VideoSource = "/videos/night-terrors.mp4"
        },

        new(3, "https://youtube.com/trailer3", true, new DateTime(2026, 1, 3), [Genres[2], Genres[3]])
        {
            Title = "Sky Raiders",
            ImageUrl = "/images/sky-raiders.jpg",
            VideoSource = "/videos/sky-raiders.mp4"
        },

        new(4, "https://youtube.com/trailer4", false, new DateTime(2026, 1, 4), [Genres[0], Genres[5]])
        {
            Title = "Broadway Dreams",
            ImageUrl = "/images/broadway-dreams.jpg",
            VideoSource = "/videos/broadway-dreams.mp4"
        },

        new(5, null, true, new DateTime(2026, 1, 5), [Genres[4]])
        {
            Title = "Laugh Out Loud",
            ImageUrl = "/images/laugh-out-loud.jpg",
            VideoSource = "/videos/laugh-out-loud.mp4"
        }
    ];

    public static List<DCollection> Collections { get; } =
    [
        new(1, true)
        {
            Name = "Trending",
            Movies =
            [
                new(1, "The Last Hero", "/images/last-hero.jpg", [Genres[0].Name]),
                new(2, "Night Terrors", "/images/night-terrors.jpg", [Genres[1].Name]),
                new(3, "Sky Raiders", "/images/sky-raiders.jpg", [Genres[3].Name])
            ]
        },

        new(2, true)
        {
            Name = "New Releases",
            Movies =
            [
                new(3, "Sky Raiders", "/images/sky-raiders.jpg", [Genres[3].Name]),
                new(4, "Broadway Dreams", "/images/broadway-dreams.jpg", [Genres[5].Name])
            ]
        },

        new(3, true)
        {
            Name = "Comedy Picks",
            Movies =
            [
                new(5, "Laugh Out Loud", "/images/laugh-out-loud.jpg", [Genres[4].Name])
            ]
        }
    ];
}
