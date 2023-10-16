<template>
    <div @click="setActiveRecipe(recipe.id)" data-bs-toggle="modal" data-bs-target="#recipeModal" class="recipe-card selectable m-3 elevation-3">
      <img class="img-fluid recipe-image" :src="recipe.img">
      <div class="category">
        <p class="category-text m-1">{{ recipe.category }}</p>
        <button @click.stop="toggleFavorite(recipe.id)" v-if="favorited == false" class="btn like-button"><i class="heart mdi mdi-heart-outline"></i></button>
        <button @click.stop="toggleFavorite(recipe.id)" v-else class="btn like-button"><i class="heart mdi mdi-heart"></i></button>
      </div>
      <div class="title-panel w-100 p-0">
      <h3 class="recipe-title">{{ recipe.title }}</h3>
      <button @click.stop="deleteRecipe(recipe.id)" v-if="recipe.creatorId == account.id" class="btn delete-button"><i class="mdi mdi-delete"></i></button>
      <button  v-if="recipe.creatorId == account.id" class="p-2"><i class="mdi mdi-pen"></i><b>Edit Recipe</b></button>
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
      if(AppState.account)
      checkFavorite()
    })
    onMounted(() => checkFavorite())
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
    toggleFavorite() {
      favorited.value = !favorited.value;
      if(favorited.value == true)
      {
        logger.log("favoriting recipe..", props.recipe.id )
        // let button = document.getElementsByClassName('btn')
        // create favorite with recipeId
        // button.removeAttribute('disabled')
      } else {
        logger.log("unfavoriting recipe..", props.recipe.id)
          // let button = document.getElementsByClassName('btn')
        // delete favorite with recipeId
        //  button.removeAttribute('disabled')
      }
      },
    setActiveRecipe() {
      let activeRecipe = AppState.recipes.find(r => r.id == props.recipe.id)
      AppState.activeRecipe = activeRecipe;
      logger.log("setting the active recipe", AppState.activeRecipe)
    },
    deleteRecipe() {
      logger.log("Deleting Recipe:", props.recipe.id)
    },
    }
   }
  };
</script>

<style lang="scss" scoped>

p {
  margin: 0px;
}

h3 {
  margin: 0px;
}

.like-button {
  color: #ff8484;
}

.heart {
  font-size: 2rem;
}

.like-button:hover {
  transform: scale(1.1);
}
.recipe-card {
  aspect-ratio: 1/1;
  min-height: 30vh;
  position: relative;
  border-radius: 1rem;
  transition: .5s;
}

.recipe-card:hover {
  transform: scale(1.02);
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