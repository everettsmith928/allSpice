

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

  internal List<RecipeFavoriteView> GetFavoritesByAccount(string accountId)
  {
    List<RecipeFavoriteView> favorites = _repo.GetFavoritesByAccount(accountId);
    return favorites;
  }
}