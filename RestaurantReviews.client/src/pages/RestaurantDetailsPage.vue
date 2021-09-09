<template>
  <div class="component container">
    <div class="row mt-5 bg-light elevation-1">
      <div class="col-md-8 d-flex p-3">
        <img src="https://thiscatdoesnotexist.com" alt="">
        <div class="d-flex flex-column justify-content-center">
          <h3 class="m-0 border-bottom border-primary text-break p-3">
            {{ restaurant.name }}
          </h3>
          <h3 class="m-0 p-3">
            {{ restaurant.location }}
          </h3>
        </div>
      </div>
      <div class="col-md-4 p-3 text-right d-flex flex-column justify-content-between">
        <p>Total Reviews: {{ reviews.length }}</p>
        <p>Average: {{ average }}</p>
        <button class="btn btn-success" data-toggle="modal" data-target="#review-modal">
          Add Review
        </button>
      </div>
    </div>
    <ReviewModal v-if="restaurant.id" :restaurant="restaurant" />
    <div class="row mt-5 bg-light elevation-1">
      <div class="col">
        {{ reviews }}
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { restaurantsService } from '../services/RestaurantsService'
import { reviewsService } from '../services/ReviewsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await restaurantsService.getById(route.params.id)
        await reviewsService.getByRestaurantId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })

    return {
      restaurant: computed(() => AppState.activeRestaurant),
      reviews: computed(() => AppState.reviews),
      average: computed(() => {
        let total = 0
        AppState.reviews.forEach(r => { total += r.rating })
        return total / AppState.reviews.length
      })
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  width: 200px
}
</style>
