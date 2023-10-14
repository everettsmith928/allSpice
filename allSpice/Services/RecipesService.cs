namespace allSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null) throw new Exception("Unable to find recipe.");
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal string DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId) throw new Exception("Unauthorized");
    _repo.DeleteRecipe(recipeId);
    string message = "Recipe Deleted";
    return message;
  }

  internal Recipe EditRecipe(Recipe recipe)
  {
    Recipe foundRecipe = this.GetRecipeById(recipe.Id);
    if (foundRecipe.CreatorId != recipe.CreatorId) throw new Exception("Unauthorized");
    foundRecipe.Img = recipe.Img ?? foundRecipe.Img;
    foundRecipe.Title = recipe.Title ?? foundRecipe.Title;
    foundRecipe.Instructions = recipe.Instructions ?? foundRecipe.Instructions;
    foundRecipe.Category = recipe.Category ?? foundRecipe.Category;
    Recipe editedRecipe = _repo.EditRecipe(foundRecipe);
    return editedRecipe;
  }
}