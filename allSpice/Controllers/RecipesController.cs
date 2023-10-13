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

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      return recipe;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DeleteRecipe([FromBody] int recipeId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipesService.DeleteRecipe(recipeId, userInfo.Id);
      return message;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}