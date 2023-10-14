
namespace allSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal Ingredient CreateIngredient(Ingredient ingredient, Account userInfo)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
    if (recipe.CreatorId != userInfo.Id) throw new Exception("This is not your Recipe");
    Ingredient newIngredient = _repo.CreateIngredient(ingredient);
    return newIngredient;
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    Recipe recipe = _recipesService.GetRecipeById(recipeId);
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
    return ingredients;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetIngredientById(ingredientId);
    if (ingredient == null) throw new Exception("Ingredient not found");
    return ingredient;
  }

  internal string DeleteIngredient(int ingredientId, Account userInfo)
  {
    Ingredient ingredient = this.GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
    if (userInfo.Id != recipe.CreatorId) throw new Exception("Unauthorized");
    _repo.DeleteIngredient(ingredientId);
    string message = "Deleted Ingredient";
    return message;
  }

}