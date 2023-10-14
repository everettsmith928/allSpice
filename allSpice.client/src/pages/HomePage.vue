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
    <section class="row m-3 g-3 elevation-3 recipe-list">
      <RecipeCard/>
      <RecipeCard/>
      <RecipeCard/>
    </section>
  
  <!-- <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="home-card p-5 bg-white rounded elevation-3">
      <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo"
        class="rounded-circle">
      <h1 class="my-5 bg-dark text-white p-3 rounded text-center">
        Vue 3 Starter
      </h1>
    </div>
  </div> -->
</template>

<script>
import { computed, ref } from "vue";
import RecipeCard from "../components/RecipeCard.vue";
import { AppState} from "../AppState.js";
export default {
    setup() {
    const filter = ref('')

        return {
          filter,
          account: computed(() => AppState.account),
          recipes: computed(() => {
          if (!filter.value) {
          return AppState.recipes
        } else if(filter.value == 'my') {
          return AppState.filter.filter(recipe => recipe.creatorId == this.account.id)
        } else if (filter.value == 'favorites') {
          // Get the recipes by the Account
          return AppState.recipes
        } else {
          return AppState.favoriteRecipes
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
  margin-bottom: 2rem;
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
</style>
