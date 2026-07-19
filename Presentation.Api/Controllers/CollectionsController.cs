using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

public class CollectionsController(ICollectionsService service) : ControllerBase { }
