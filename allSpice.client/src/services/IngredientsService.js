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

}

export const ingredientsService = new IngredientsService()