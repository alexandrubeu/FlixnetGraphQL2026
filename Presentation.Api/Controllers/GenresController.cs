using BusinessLogic.Dtos.Genres;
using BusinessLogic.Dtos.Movies;
using BusinessLogic.Services;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GenresController(IMoviesService moviesService) : ControllerBase
{
    [HttpGet]
    public ActionResult<Pagination<DGenre>> GetAll(string? term, PaginationParam paginationParam)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public ActionResult<DGenre> GetOne(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult<DGenre> Create([FromBody] DMovie movie)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public ActionResult<bool> Update(int id, [FromBody] DMovie movie)
    {
        throw new NotImplementedException();
    }

    // Should be rarely used. Should it be included at all?
    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
