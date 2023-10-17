<template>
    <section class="row m-3">
      <div class="col-12 d-flex justify-content-center banner elevation-5">
      <div class="row banner-filter elevation-3">
        <div @click="filter = ''" class="col-4 selectable d-flex align-items-center justify-content-center text-center">
          <div>
          <p class="banner-filter-text m-0">All Recipes</p>
          </div>
        </div>
        <div @click="filter = 'my'" class="col-4 selectable d-flex align-items-center justify-content-center text-center">
          <div>
          <p class="banner-filter-text m-0">My Recipes</p>
          </div>
        </div>
        <div @click="filter = 'favorites'" class="col-4 selectable d-flex align-items-center justify-content-center text-center">
          <div>
          <p class="banner-filter-text m-0">Favorite Recipes</p>
          </div>
        </div>
      </div>
      </div>
    </section>
    <section v-if="filter == '' || filter == 'my'" class="row g-2 recipe-list">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4 p-0 m-0 text-center">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
    <section v-if="filter == 'favorites'" class="row g-2 recipe-list">
      <div v-for="recipe in favoriteRecipes" :key="recipe.id" class="col-4 p-0 m-0 text-center">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
    <!-- THIS IS THE EDIT MODAL -->
      <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
          <div class="modal-content">
            <div class="modal-body">
              <ActiveRecipe/>
            </div>
            <div class="modal-footer">
              <button @click.stop="deleteRecipe(activeRecipe.id)" v-if="activeRecipe && activeRecipe.creatorId == account.id" class="btn delete-button">
              <i class="mdi mdi-delete"></i>
            </button>
            <button  v-if=" activeRecipe && activeRecipe.creatorId == account.id" class="p-2">
              <i class="mdi mdi-pen"></i><b>Edit Recipe</b>
            </button>
            </div>
          </div>
        </div>
      </div>
 
</template>

<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import { AppState} from "../AppState.js";
import {recipesService} from "../services/RecipesService"
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";

export default {
    setup() {
    const account = computed(() => AppState.account)
    const activeRecipe = computed(() => AppState.activeRecipe)
    const filter = ref('')
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getFavorites() {
      try {
        await recipesService.getFavorites();
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getFavoriteRecipes() {
      try {
        await recipesService.getFavoriteRecipes();
      } catch (error) {
        Pop.error(error)
      }
    }
    onMounted(() => {
      getRecipes();
    })

    watchEffect(() => {
      if (AppState.account)
      getFavoriteRecipes()
    })
        return {
          getFavoriteRecipes,
          activeRecipe,
          account,
          filter,
          favoriteRecipes: computed(() => AppState.favoriteRecipes),
          recipes: computed(() => {
          if(filter.value == 'my' && AppState.account) {
            logger.log(filter.value)
            logger.log(AppState.account)
            return AppState.recipes.filter(r => r.creatorId == AppState.account.id)
          } else if (filter.value == 'favorites' && AppState.account) {
            logger.log(filter.value)
            return AppState.favoriteRecipes
          } else {
            logger.log(filter.value)
            return AppState.recipes
          }
          })
        };
    },
    components: { RecipeCard }
}
</script>

<style scoped lang="scss">



p {
  margin: 0px;
}
.banner {
  min-height: 30vh;
  background-image: url('https://images.unsplash.com/photo-1504674900247-0877df9cc836?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1770&q=80');
  background-size: cover;
  background-position: center;
  position: relative;
  margin-bottom: 10vh;
}

.banner-filter {
  position: relative;
  max-height: 5vh;
  width: 40%;
  background-color: #ffffff;
  bottom: -28vh;
}

.banner-filter-text {
  color: rgb(139, 206, 139);
  background-color: #ffffff;
}

.recipe-list {
  margin: 1rem;
}

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.modal-body {
  padding: 0px;
  min-height: 70vh;
}
</style>
