
namespace allSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<RecipeFavoriteView> GetFavoritesByAccount(string accountId)
  {
    string sql = @"
    SELECT
    fav.*,
    rec.*
    FROM favorites fav
    JOIN recipes rec ON rec.id = fav.recipeId
    WHERE fav.accountId = @accountId
    ;";
    List<RecipeFavoriteView> myFavorites = _db.Query<Favorite, RecipeFavoriteView, RecipeFavoriteView>(sql, (favorite, recipe) =>
    {
      recipe.FavoriteId = favorite.Id;
      recipe.CreatorId = favorite.AccountId;
      return recipe;
    }, new { accountId }).ToList();
    return myFavorites;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@recipeId, @accountId);
    SELECT * FROM favorites
    WHERE id = LAST_INSERT_ID();
    ;";
    int lastInsertId = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = lastInsertId;
    return favoriteData;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"
    SELECT * FROM 
    favorites
    WHERE id = @favoriteId
    ;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @favoriteId;";
    _db.Execute(sql, new { favoriteId });
  }
}