namespace allSpice.Controllers;

[Authorize]
[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0 = null)
  {
    _ingredientsService = ingredientsService;
    _auth0 = auth0;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredient)
  {
    Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
    Ingredient newIngredient = _ingredientsService.CreateIngredient(ingredient, userInfo);
    return newIngredient;
  }

  [Authorize]
  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
    string message = _ingredientsService.DeleteIngredient(ingredientId, userInfo);
    return message;
  }


}