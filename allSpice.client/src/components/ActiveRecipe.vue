<template>
  <div v-if="activeRecipe" class="active-recipe">
    <section class="row m-0 p-0">
      <div class="col-5 d-flex p-0 recipe-image-wrapper">
        <img class="img-fluid recipe-image" :src="activeRecipe.img">
      </div>
      <div class="col-7 p-0">
        <section class="row">
        <div class="col-12 recipe-info d-flex">
          <h3 class="recipe-title">{{ activeRecipe.title }}</h3>
          <p class="recipe-category mx-3"><b>{{ activeRecipe.category }} </b></p>
          <div v-if="account.id">
          <button @click.stop="toggleFavorite(activeRecipe.id)" v-if="favorited == false" class="btn like-button">
            <i class="heart mdi mdi-heart-outline"></i>
          </button>
          <button @click.stop="toggleFavorite(activeRecipe.id)" v-else class="btn like-button">
            <i class="heart mdi mdi-heart"></i>
          </button>
          </div>
        </div>
        <div class="col-6 p-3 recipe-instructions">
          <h3 class="recipe-title">Instructions</h3>
          <p>{{ activeRecipe.instructions }}</p>
        </div>
        <div class="col-6 p-3 recipe-ingredients">
          <h3 class="recipe-title">Ingredients</h3>
          <div v-for="ingredient in ingredients" :key="ingredient.id" class="ingredient">
            <IngredientCard :ingredient="ingredient"/>
          </div>
          <div v-if="activeRecipe && activeRecipe.creatorId == account.id">
            <form @submit.prevent="createIngredient" class="">
              <label>Ingredient Name</label>
              <input v-model="ingredientData.name" class="form-control" type="text">
              <label>Ingredient Quantity</label>
              <input v-model="ingredientData.quantity" class="form-control" type="text">
              <button type="submit" class="btn btn-success p-2 m-2">
                <i class="mdi mdi-plus"></i>
              </button>
            </form>
          </div>
        </div>
        </section>
      </div>
    </section>
  </div>
      
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { Recipe } from "../models/Recipe";
import { FavoritedRecipe } from "../models/FavoritedRecipes";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { ingredientsService } from "../services/IngredientsService"
import { recipesService } from "../services/RecipesService";
import IngredientCard from "./IngredientCard.vue";
export default {
    setup() {
        let favorited = ref(false);
        let ingredientData = ref({})
        const activeRecipe = computed(() => AppState.activeRecipe);
        const account = computed(() => AppState.account);
        const ingredients = computed(() => AppState.ingredients);
        async function getIngredients() {
            if (activeRecipe.value) {
                try {
                    logger.log(activeRecipe.value.id);
                    await ingredientsService.getIngredients(activeRecipe.value.id);
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        }
        watchEffect(() => {
            AppState.activeRecipe;
            getIngredients();
        });
        watchEffect(() => {
            if (AppState.activeRecipe) {
                checkFavorite();
            }
        });
        function checkFavorite() {
            let recipeInFavorites = AppState.favoriteRecipes.find(r => r.id == AppState.activeRecipe.id);
            if (recipeInFavorites) {
                favorited.value = true;
                logger.log(favorited.value, "Prop bool swapped");
            }
            else {
                favorited.value = false;
                logger.log(favorited.value, "recipe not favorited");
            }
        }
        return {
            activeRecipe,
            favorited,
            account,
            ingredients,
            ingredientData,
            async toggleFavorite() {
                favorited.value = !favorited.value;
                if (favorited.value == true) {
                    logger.log("favoriting recipe..", activeRecipe.value.id);
                    await recipesService.createFavorite(activeRecipe.value.id);
                    // let button = document.getElementsByClassName('btn')
                    // create favorite with recipeId
                    // button.removeAttribute('disabled')
                }
                else {
                    logger.log("unfavoriting recipe..", activeRecipe.value.id);
                    await recipesService.deleteFavorite(activeRecipe.value.id);
                    // let button = document.getElementsByClassName('btn')
                    // delete favorite with recipeId
                    //  button.removeAttribute('disabled')
                }
            },
          
            async createIngredient() {
              ingredientData.value.recipeId = AppState.activeRecipe.id
              logger.log("creating Ingredient..", ingredientData.value)
              ingredientsService.createIngredient(ingredientData.value)
              ingredientData.value = {}
            }
        };
    },
    components: { IngredientCard }
};
</script>


<style lang="scss" scoped>

.like-button {
  color: #ff8484;
}

.heart {
  font-size: 2rem;
}

.like-button:hover {
  transform: scale(1.1);
}

.recipe-title {
  margin: 0px;
  color: rgb(89, 213, 89);
  padding: .5rem;
}

.recipe-image-wrapper {
  height: 70vh;
}

.recipe-image {
  object-fit: cover !important;
  object-position: center;
}

.recipe-category {
  margin: auto;
  background-color: #acacac;
  color: #e2e2e2;
  border-radius: 24px;
  padding: 12px;
}

.recipe-info {
  margin-top: 1rem;
}

.btn-close {
  position: absolute;
  right: 0px;
  margin: .2rem;
}
</style>