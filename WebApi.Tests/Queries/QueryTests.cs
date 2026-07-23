using BusinessLogic.Dtos;
using Moq;
using WebApi.GraphQL.Cursor;
using WebApi.GraphQL.Queries;
using WebApi.GraphQL.Services;

namespace WebApi.Tests.Queries;

[TestFixture]
public class QueryTests
{
    private Mock<IMovieServiceWithCursor> _movieServiceMock = null!;
    private Query _query = null!;

    [SetUp]
    public void SetUp()
    {
        _movieServiceMock = new Mock<IMovieServiceWithCursor>();
        _query = new Query();
    }

    [Test]
    public async Task GetMoviesWithCursor_ReturnsMappedEdgesAndPageInfo_WhenMoviesExist()
    {
        var movie1 = new DMovie(1, "trailer1", true, new DateTime(2026, 1, 10), new List<DGenre>());
        var movie2 = new DMovie(2, "trailer2", true, new DateTime(2026, 1, 5), new List<DGenre>());
        var pagedResult = new PagedResult<DMovie>
        {
            Items = [movie1, movie2],
            HasNextPage = true,
            HasPreviousPage = false
        };
        _movieServiceMock
            .Setup(s => s.GetMoviesAsync(2, null))
            .ReturnsAsync(pagedResult);

        var connection = await _query.GetMoviesWithCursor(2, null, _movieServiceMock.Object);

        Assert.That(connection.EdgesList, Has.Count.EqualTo(2));
        Assert.That(connection.EdgesList[0].Node, Is.EqualTo(movie1));
        Assert.That(connection.EdgesList[0].Cursor, Is.EqualTo(CursorEncoder.Encoder(movie1.CreatedAt, movie1.Id)));
        Assert.That(connection.EdgesList[1].Cursor, Is.EqualTo(CursorEncoder.Encoder(movie2.CreatedAt, movie2.Id)));

        Assert.That(connection.PageInfo.HasNextPage, Is.True);
        Assert.That(connection.PageInfo.HasPreviousPage, Is.False);
        Assert.That(connection.PageInfo.StartCursor, Is.EqualTo(connection.EdgesList.First().Cursor));
        Assert.That(connection.PageInfo.EndCursor, Is.EqualTo(connection.EdgesList.Last().Cursor));
    }

    [Test]
    public async Task GetMoviesWithCursor_ReturnsEmptyEdgesAndNullCursors_WhenNoMoviesMatch()
    {
        var pagedResult = new PagedResult<DMovie>
        {
            Items = [],
            HasNextPage = false,
            HasPreviousPage = true
        };
        _movieServiceMock
            .Setup(s => s.GetMoviesAsync(10, "someCursor"))
            .ReturnsAsync(pagedResult);

        var connection = await _query.GetMoviesWithCursor(10, "someCursor", _movieServiceMock.Object);

        Assert.That(connection.EdgesList, Is.Empty);
        Assert.That(connection.PageInfo.StartCursor, Is.Null);
        Assert.That(connection.PageInfo.EndCursor, Is.Null);
        Assert.That(connection.PageInfo.HasPreviousPage, Is.True);
    }

    [Test]
    public async Task GetMoviesWithCursor_PassesPageSizeAndCursorThroughToService()
    {
        _movieServiceMock
            .Setup(s => s.GetMoviesAsync(It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync(new PagedResult<DMovie>());

        await _query.GetMoviesWithCursor(5, "abc123", _movieServiceMock.Object);

        _movieServiceMock.Verify(s => s.GetMoviesAsync(5, "abc123"), Times.Once);
    }
}