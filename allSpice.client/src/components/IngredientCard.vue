<template>
  <div class="ingredient d-flex">
    <p>🫥 {{ ingredient.name }} {{ ingredient.quantity }}</p>
    <button v-if="account.id" @click="deleteIngredient" class="btn btn-danger">Remove</button>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Ingredient } from "../models/Ingredient";
import Pop from "../utils/Pop";
import { ingredientsService } from "../services/IngredientsService";
export default {
  props: {ingredient: {type: Ingredient, required: true}},
  setup(props){
    const account = computed(() => AppState.account)
  return { 
    account,
    async deleteIngredient() {
      try {
        await ingredientsService.deleteIngredient(props.ingredient.id)
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