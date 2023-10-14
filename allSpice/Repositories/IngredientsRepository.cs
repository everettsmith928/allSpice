

namespace allSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredient)
  {
    string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      VALUES
      (@name, @quantity, @recipeId);

      SELECT * FROM ingredients
      WHERE id = LAST_INSERT_ID()
      ;";
    Ingredient newIngredient = _db.Query<Ingredient>(sql, ingredient).FirstOrDefault();
    return newIngredient;
  }

  internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
  {
    string sql = @"
      SELECT * FROM ingredients
      WHERE recipeId = @recipeId
      ;";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
  internal void DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE id = @ingredientId;";
    _db.Execute(sql, new { ingredientId });
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
      SELECT * FROM ingredients
      WHERE id = @ingredientId
      ;";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

}

