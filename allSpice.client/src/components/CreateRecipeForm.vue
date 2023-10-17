<template>
 <form @submit.prevent="createRecipe()" class="row">
  <div class="col-12 mt-3">
    <label>Title</label>
    <input v-model="recipeData.title" min="2" type="text">
  </div>
  <div class="col-12 mt-3">
    <label>Image Link</label>
    <input v-model="recipeData.img" min="2" type="text">
  </div>
  <div class="col-12 mt-3">
    <label>Instructions</label>
    <input v-model="recipeData.instructions" min="2" type="text">
  </div>
  <div class="col-12 mt-3">
     <label>Category</label>
    <select v-model="recipeData.category" required>
      <option disabled selected value="">Select an Option</option>
      <option value="Italian">Italian</option>
      <option value="Specialty Coffee">Specialty Coffee</option>
      <option value="Mexican">Mexican</option>
      <option value="Soup">Soup</option>
      <option value="Cheese">Cheese</option>
    </select>
  </div>
  <div class="col-6">
    <button type="submit">Create Recipe</button>
  </div>
 </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop";
import { recipesService } from "../services/RecipesService";

export default {
  setup(){
    const recipeData = ref({})

  return { recipeData,
      async createRecipe() {
        try {
          await recipesService.createRecipe(recipeData.value)
        } catch (error) {
          Pop.error(error)
        }
      }
      }
  }
};
</script>


<style lang="scss" scoped>

</style>