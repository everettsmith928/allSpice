<template>
  <div class="col-4 m-0 recipe-card text-center">
    <div class="category">
      <p class="category-text m-1">Sample</p>
    </div>
      <p>Recipe Card</p>
      <section class="row p-0 m-0 title-panel">
        <div class="col-12 p-0">
        <h3>Sample</h3>
        <p>Description Lorem ipsum dolor sit amet.</p>
        </div>
      </section>
  </div> 
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup(){
  return {  }
  }
};
</script>

<style lang="scss" scoped>

p {
  margin: 0px;
}
.recipe-card {
  height: 200px;
  position: relative;
}

.category {
  position: absolute;
  background-color: #4f4f4f;
  border-radius: 20px;
  top: 1rem;
  left:1rem;
}

.category-text {
  color: #ffffff;
  font-size: 1rem;
  padding: .2rem;
}

.title-panel {
  background-color: #4f4f4f;
  color: #ffffff;
  position: absolute;
  bottom: 0px;
}
</style>