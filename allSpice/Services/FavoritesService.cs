


namespace allSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;

  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repo.CreateFavorite(favoriteData);
    return favorite;
  }

  internal string DeleteFavorite(int favoriteId, string accountId)
  {
    Favorite favorite = this.GetFavoriteById(favoriteId);
    if (favorite.AccountId != accountId) throw new Exception("Unauthorized");
    _repo.DeleteFavorite(favoriteId);
    string message = "Deleted the favorite";
    return message;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repo.GetFavoriteById(favoriteId);
    if (favorite == null) throw new Exception("Could not find favorite");
    return favorite;
  }

  internal List<RecipeFavoriteView> GetFavoritesByAccount(string accountId)
  {
    List<RecipeFavoriteView> favorites = _repo.GetFavoritesByAccount(accountId);
    return favorites;
  }
}