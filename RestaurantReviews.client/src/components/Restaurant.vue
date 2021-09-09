<template>
  <div class="restaurant rounded elevation-1 bg-light my-3 p-3 text-left selectable">
    <router-link :to="{name: 'RestaurantDetails', params: {id: restaurant.id}}">
      <h3 class="m-0">
        {{ restaurant.name }} - {{ restaurant.location }}
      </h3>
    </router-link>
  </div>
</template>

<script>
export default {
  props: {
    restaurant: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>
a {
  text-decoration: none;
  color: inherit
}
</style>
