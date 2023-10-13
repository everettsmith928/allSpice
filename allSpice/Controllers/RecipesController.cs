namespace allSpice.Controllers;

[Authorize]
[ApiController]
[Route("api/recipes")]

public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0;

  public RecipesController(Auth0Provider auth0, RecipesService recipesService)
  {
    _auth0 = auth0;
    _recipesService = recipesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      recipe.CreatorId = userInfo.Id;
      Recipe newRecipe = _recipesService.CreateRecipe(recipe);
      return newRecipe;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}