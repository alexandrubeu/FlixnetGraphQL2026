using BusinessLogic.Dtos;
using Entities;
using NSubstitute;
using Repository;

namespace BusinessLogic.Tests
{
    public class GenresServiceTests
    {
        [Fact]
        public void GetById_ReturnsNull_WhenGenreDoesNotExist()
        {

            var fakeRepo = Substitute.For<IGenreRepository>();
            var genresService = new GenresService(fakeRepo);

            var result = genresService.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void GetById_ReturnsMappedGenre_WhenItExists()
        {
            var fakeRepo = Substitute.For<IGenreRepository>();
            var genre = new EGenre { Id = 1, Name = "Drama" };
            fakeRepo.GetById(1).Returns(genre);

            var genresService = new GenresService(fakeRepo);
            var result = genresService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Drama", result.Name);
        }

        [Fact]
        public void Add_AssignsGeneratedId()
        {
            var fakeRepo = Substitute.For<IGenreRepository>();
            var genresService = new GenresService(fakeRepo);
            var input = new DInputCreateGenre { Name = "Sci-Fi" };

            var result = genresService.Add(input);

            fakeRepo.Received();

            Assert.Equal("Sci-Fi", result.Name);
        }

        [Fact]
        public void Delete_ReturnsFalse_WhenGenreDoesNotExist()
        {
            var fakeRepo = Substitute.For<IGenreRepository>();
            var genresService = new GenresService(fakeRepo);

            var result = genresService.Delete(42);

            Assert.False(result);
        }

        [Fact]
        public void Delete_ReturnsTrue_AndRemovesGenre_WhenItExists()
        {
            var fakeRepo = Substitute.For<IGenreRepository>();
            var genre = new EGenre { Id = 5, Name = "Horror"};
            fakeRepo.GetById(5).Returns(genre);

            var genresService = new GenresService(fakeRepo);

            var result = genresService.Delete(5);

            Assert.True(result);
            Assert.Empty(fakeRepo.GetAll());
        }
    }
}