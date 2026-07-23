using AutoFixture;
using BusinessLogic;
using BusinessLogic.Dtos;
using Entities;
using NSubstitute;
using Repository;
using Shouldly;

namespace Tests;

public class GenresServiceTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IGenreRepository _repo = Substitute.For<IGenreRepository>();
    private readonly GenresService _sut;

    public GenresServiceTests()
    {
        // EGenre are o colectie Movies care poate cauza recursivitate la AutoFixture
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _sut = new GenresService(_repo);
    }

    // --- GetAll ---

    [Fact]
    public void GetAll_WhenRepositoryReturnsGenres_ReturnsMappedDtos()
    {
        var genres = _fixture.CreateMany<EGenre>(3).ToList();
        _repo.GetAll().Returns(genres);

        var result = _sut.GetAll().ToList();

        result.Count.ShouldBe(3);
        result[0].Id.ShouldBe(genres[0].Id);
        result[0].Name.ShouldBe(genres[0].Name);
    }

    [Fact]
    public void GetAll_WhenRepositoryReturnsEmpty_ReturnsEmptyCollection()
    {
        _repo.GetAll().Returns([]);

        var result = _sut.GetAll();

        result.ShouldBeEmpty();
    }

    // --- GetById ---

    [Fact]
    public void GetById_WhenGenreExists_ReturnsMappedDto()
    {
        var genre = _fixture.Create<EGenre>();
        _repo.GetById(genre.Id).Returns(genre);

        var result = _sut.GetById(genre.Id);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(genre.Id);
        result.Name.ShouldBe(genre.Name);
    }

    [Fact]
    public void GetById_WhenGenreDoesNotExist_ReturnsNull()
    {
        _repo.GetById(Arg.Any<int>()).Returns((EGenre?)null);

        var result = _sut.GetById(99);

        result.ShouldBeNull();
    }

    // --- Add ---

    [Fact]
    public void Add_CallsRepositoryWithCorrectEntity()
    {
        var input = _fixture.Create<DInputCreateGenre>();

        _sut.Add(input);

        _repo.Received(1).Add(Arg.Is<EGenre>(e => e.Name == input.Name));
    }

    [Fact]
    public void Add_ReturnsDtoWithCorrectName()
    {
        var input = _fixture.Create<DInputCreateGenre>();

        var result = _sut.Add(input);

        result.Name.ShouldBe(input.Name);
    }

    // --- Update ---

    [Fact]
    public void Update_WhenGenreDoesNotExist_ReturnsNull()
    {
        _repo.GetById(Arg.Any<int>()).Returns((EGenre?)null);
        var input = _fixture.Create<DInputUpdateGenre>();

        var result = _sut.Update(99, input);

        result.ShouldBeNull();
    }

    [Fact]
    public void Update_WhenGenreExists_UpdatesNameAndReturnsDto()
    {
        var genre = _fixture.Create<EGenre>();
        _repo.GetById(genre.Id).Returns(genre);
        var input = _fixture.Create<DInputUpdateGenre>();

        var result = _sut.Update(genre.Id, input);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(input.Name);
        _repo.Received(1).Update(Arg.Is<EGenre>(e => e.Name == input.Name));
    }

    // --- Delete ---

    [Fact]
    public void Delete_WhenGenreDoesNotExist_ReturnsFalseAndDoesNotCallDelete()
    {
        _repo.GetById(Arg.Any<int>()).Returns((EGenre?)null);

        var result = _sut.Delete(99);

        result.ShouldBeFalse();
        _repo.DidNotReceive().Delete(Arg.Any<int>());
    }

    [Fact]
    public void Delete_WhenGenreExists_ReturnsTrueAndCallsDelete()
    {
        var genre = _fixture.Create<EGenre>();
        _repo.GetById(genre.Id).Returns(genre);

        var result = _sut.Delete(genre.Id);

        result.ShouldBeTrue();
        _repo.Received(1).Delete(genre.Id);
    }
}
