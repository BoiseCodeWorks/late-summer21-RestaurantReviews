<template>
  <div class="review-modal">
    <!-- Modal -->
    <div class="modal"
         id="review-modal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Review For {{ restaurant.name }}
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="submitReview">
              <div class="form-group">
                <label for="">Title</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="title"
                       placeholder="Title..."
                       v-model="state.newReview.title"
                >
              </div>
              <div class="form-group">
                <label for="body">Body</label>
                <input type="text"
                       class="form-control"
                       aria-describedby="body"
                       placeholder="Body..."
                       v-model="state.newReview.body"
                >
              </div>
              <div class="form-group">
                {{ state.newReview.rating }}
                <label for="range" class="form-label">Rating</label>
                <input type="range"
                       class="form-range"
                       min="0"
                       max="5"
                       id="range"
                       v-model="state.newReview.rating"
                >
              </div>
              <div class="form-group">
                <label for="published" class="form-label">Published</label>
                <input class="form-checkbox" type="checkbox" v-model="state.newReview.published">
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="submit" class="btn btn-primary">
                  Save
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { reviewsService } from '../services/ReviewsService'
import $ from 'jquery'
import Pop from '../utils/Notifier'

export default {
  props: {
    restaurant: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      newReview: {
        restaurantId: props.restaurant.id
      }
    })
    return {
      state,
      async submitReview() {
        try {
          await reviewsService.create(state.newReview)
          state.newReview = { restaurantId: props.restaurant.id }
          $('#review-modal').modal('hide')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
