namespace allSpice.Controllers;

[Authorize]
[ApiController]
[Route("api/recipes")]

public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0;

  private readonly IngredientsService _ingredientsService;

  public RecipesController(Auth0Provider auth0, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _auth0 = auth0;
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
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

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetAllRecipes();
      return recipes;
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
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
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

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> EditRecipe([FromBody] Recipe recipe, int recipeId)
  {
    try
    {

      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      recipe.CreatorId = userInfo.Id;
      recipe.Id = recipeId;
      Recipe editedRecipe = _recipesService.EditRecipe(recipe);
      return editedRecipe;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipe(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipe(recipeId);
      return ingredients;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}