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
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="col-6 p-3 recipe-instructions">
          <h3 class="recipe-title">Instructions</h3>
          <p>{{ activeRecipe.instructions }}</p>
        </div>
        <div class="col-6 p-3 recipe-ingredients">
          <h3 class="recipe-title">Ingredients</h3>

        </div>
        </section>
      </div>
    </section>
    
  </div>
      
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup(){
    const activeRecipe = computed(() => AppState.activeRecipe)
  return {
    activeRecipe
    }
  }
};
</script>


<style lang="scss" scoped>
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