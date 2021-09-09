<template>
  <div class="home flex-grow-1 container">
    <div class="row">
      <div class="col text-center">
        <h2>Welcome to Restaurant Reviewer</h2>
      </div>
    </div>
    <div class="row">
      <div v-if="loading" class="col text-center">
        <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
      </div>
      <div v-else class="col">
        <Restaurant v-for="r in restaurants" :key="r.id" :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { restaurantsService } from '../services/RestaurantsService'
import { AppState } from '../AppState'

export default {
  setup() {
    const loading = ref(true)
    onMounted(async() => {
      try {
        await restaurantsService.getAll()
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      loading,
      restaurants: computed(() => AppState.restaurants)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
