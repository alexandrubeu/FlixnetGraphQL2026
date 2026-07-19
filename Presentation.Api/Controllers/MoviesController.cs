using BusinessLogic.Dtos.Movies;
using BusinessLogic.Services;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController(IMoviesService moviesService) : ControllerBase
{
    [HttpGet]
    public ActionResult<Pagination<DMovie>> GetAll(String? term, PaginationParam paginationParam)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public ActionResult<DMovie> GetOne(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult<DMovie> Create([FromBody] DMovie movie)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public ActionResult<bool> Update(int id, [FromBody] DMovie movie)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
