import { AppState } from "../AppState"
import { api } from "./AxiosService"
import { logger } from "../utils/Logger"
import {Recipe} from "../models/Recipe.js"
import { FavoritedRecipe } from "../models/FavoritedRecipes"


class RecipesService {
  async getRecipes() {
    let res = await api.get("api/recipes")
    logger.log(res.data)
    AppState.recipes = res.data.map(r => r = new Recipe(r));
    logger.log(AppState.recipes)
  }

  async getFavoriteRecipes() {
    if(AppState.account.id){
      let res = await api.get("account/favorites")
      logger.log(res.data)
      AppState.favoriteRecipes = res.data.map(r => r = new FavoritedRecipe(r))
      logger.log(AppState.favoriteRecipes)
    } else {
      logger.log("not logged in")
    }
  }
}

export const recipesService = new RecipesService();