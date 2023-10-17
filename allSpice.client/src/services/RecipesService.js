import { AppState } from "../AppState"
import { api } from "./AxiosService"
import { logger } from "../utils/Logger"
import {Recipe} from "../models/Recipe.js"
import { FavoritedRecipe } from "../models/FavoritedRecipes"
import { Favorite } from "../models/Favorite"


class RecipesService {
  async getRecipes() {
    let res = await api.get("api/recipes")
    logger.log(res.data)
    AppState.recipes = res.data.map(r => r = new Recipe(r));
    logger.log(AppState.recipes)
  }

  async getRecipeById(recipeId) {
    let res = await api.get(`api/recipes/${recipeId}`)
    logger.log(res.data)

  }

  async createRecipe(recipeData) {
    let res = await api.post(`api/recipes`, recipeData)
    logger.log(res.data)
    AppState.recipes.push(new Recipe(res.data))
    logger.log(AppState.recipes)
  }

  async deleteRecipe(recipeId) {
    let res = await api.delete(`api/recipes/${recipeId}`)
    logger.log(res.data)
    let index = AppState.recipes.findIndex(r => r.id == recipeId)
    AppState.recipes.splice(index, 1)

  }

  searchRecipes(search) {
    if(search){
      const foundRecipes = AppState.recipes.filter(r => r.category == search)
      return foundRecipes;
    } else {
      recipesService.getRecipes()
      return AppState.recipes
    }
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

  async createFavorite(recipeId){
    let res = await api.post("api/favorites", {recipeId: recipeId})
    logger.log(res.data)
    this.getFavoriteRecipes()
    // let favorite = new Favorite(res.data)
    // let newFavorite = this.getRecipeById(favorite.recipeId)
    // let favoritedRecipe = new FavoritedRecipe(newFavorite)
    // AppState.favoriteRecipes.push(favoritedRecipe);
    logger.log(AppState.favoriteRecipes);
  }

  async deleteFavorite(recipeId){
    debugger
    let recipeToDelete = AppState.favoriteRecipes.find(r => r.id == recipeId)
    let favoriteId = recipeToDelete.favoriteId
    let res = await api.delete(`api/favorites/${favoriteId}`)
    logger.log(res.data)
    let recipeIndex = AppState.favoriteRecipes.findIndex(r => r.id == recipeId)
    AppState.favoriteRecipes.splice(recipeIndex, 1);
    logger.log('AfterSplice', AppState.favoriteRecipes)
  
  }

  async getFavorites(){
    let res = await api.get(`/account/favorites`)
    logger.log('Account Favorites', res.data)
  }
}

export const recipesService = new RecipesService();