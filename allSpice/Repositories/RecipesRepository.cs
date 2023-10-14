namespace allSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@title, @instructions, @img, @category, @creatorId);

    SELECT
    rec.*,
    act.*
    FROM recipes rec
    JOIN accounts act ON act.id = rec.creatorId
    WHERE rec.id = LAST_INSERT_ID()
    ;";
    Recipe newRecipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
         {
           recipe.Creator = account;
           return recipe;
         }, recipeData).FirstOrDefault();
    return newRecipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT 
    act.*,
    rec.*
    FROM recipes rec
    JOIN accounts act ON act.id = rec.creatorId;";
    List<Recipe> recipes = _db.Query<Account, Recipe, Recipe>(sql, (account, recipe) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT 
    rec.*,
    act.* 
    FROM recipes rec
    JOIN accounts act ON rec.creatorId = act.id
    WHERE rec.id = @recipeId
    ;";
    Recipe foundRecipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return foundRecipe;
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = @"
    DELETE FROM recipes
    WHERE id = @recipeId;";
    _db.Execute(sql, new { recipeId });
  }

  internal Recipe EditRecipe(Recipe recipe)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @title,
    instructions = @instructions,
    img = @img,
    category = @category
    WHERE id = @id;
    SELECT 
    rec.*,
    act.* 
    FROM recipes rec
    JOIN accounts act ON act.id = rec.creatorId
    WHERE rec.id = @id
    ;";
    Recipe editedRecipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipe).FirstOrDefault();
    return editedRecipe;
  }
}