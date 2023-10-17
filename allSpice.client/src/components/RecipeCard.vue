<template>
    <div @click="setActiveRecipe(recipe.id)" data-bs-toggle="modal" data-bs-target="#recipeModal" class="recipe-card selectable m-3 elevation-3">
      <img class="img-fluid recipe-image" :src="recipe.img">
      <div class="category">
        <p class="category-text m-1">{{ recipe.category }}</p>
        <div v-if="favorited == false" class="btn like-button"><i class="heart mdi mdi-heart-outline"></i></div>
        <div v-else class="btn like-button"><i class="heart mdi mdi-heart"></i></div>
      </div>
      <div class="title-panel w-100 p-0">
      <h3 class="recipe-title">{{ recipe.title }}</h3>
      
      </div>
    </div> 
    
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { logger } from "../utils/Logger";
import { Recipe } from "../models/Recipe";
import { FavoritedRecipe } from "../models/FavoritedRecipes";

export default {
  props: {recipe: { type: Recipe || FavoritedRecipe, required: true}},
  setup(props){
    let favorited = ref(false)
    const account = computed(() => AppState.account)
    watchEffect(() => {
      AppState.account
      checkFavorite()
    }
      
    )
    function checkFavorite() {
      let recipeInAppstate = AppState.favoriteRecipes.find(r => r.id == props.recipe.id)
      if (recipeInAppstate) {
        favorited.value = true;
        logger.log(favorited.value, "Prop bool swapped")
      }
    }
  return { 
    props,
    account,
    favorited,
    
    setActiveRecipe() {
      let activeRecipe = AppState.recipes.find(r => r.id == props.recipe.id)
      AppState.activeRecipe = activeRecipe;
      logger.log("setting the active recipe", AppState.activeRecipe)
    },
    // deleteRecipe() {
    //   logger.log("Deleting Recipe:", props.recipe.id)
    // },
    }
   }
  };
</script>

<style lang="scss" scoped>

.like-button {
  color: #ff8484;
}

.heart {
  font-size: 2rem;
}

p {
  margin: 0px;
}

h3 {
  margin: 0px;
}


.recipe-card {
  aspect-ratio: 1/1;
  min-height: 30vh;
  position: relative;
  border-radius: 1rem;
  transition: .5s;
}

.recipe-image {
  object-fit: cover;
  aspect-ratio: 1/1;
}

.category {
  position: absolute;
  background-color: #1b1b1b;
  border-radius: 20px;
  top: 1rem;
  left: 1rem;
}

.category-text {
  color: #ffffff;
  font-size: 1rem;
  padding: .2rem;
}

.title-panel {
  background-color: #4f4f4f;
  backdrop-filter: opacity(50%);
  color: #ffffff;
  position: absolute;
  bottom: 12px;
}


</style>