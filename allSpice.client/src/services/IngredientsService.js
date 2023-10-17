import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{

  async getIngredients(recipeId){
    let res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log(res.data)
    AppState.ingredients = res.data?.map(i => new Ingredient(i))
    logger.log('This is the Appstate', AppState.ingredients)
  }
  async createIngredient(newIngredient) {
    let res = await api.post(`api/ingredients`, newIngredient)
    logger.log(res.data)
    AppState.ingredients.push(new Ingredient(res.data))
  }

  async deleteIngredient(ingredientId) {
    let res = await api.delete(`api/ingredients/${ingredientId}`)
    logger.log(res.data)
    let index = AppState.ingredients.findIndex(i => i.id == ingredientId)
    AppState.ingredients.splice(index, 1)
  }
}

export const ingredientsService = new IngredientsService()