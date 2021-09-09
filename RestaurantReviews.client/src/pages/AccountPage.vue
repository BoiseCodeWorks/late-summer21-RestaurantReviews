<template>
  <div class="about text-center container">
    <div class="row">
      <div class="col d-flex my-5">
        <img class="rounded" :src="account.picture" alt="" />
        <div class="p-3">
          <p contenteditable @blur="saveChanges('name')">
            {{ account.name }}
          </p>
          <p contenteditable @blur="saveChanges('email')">
            {{ account.email }}
          </p>
        </div>
      </div>
    </div>

    <p>{{ myReviews }}</p>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { accountService } from '../services/AccountService'
export default {
  name: 'Account',
  setup() {
    onMounted(async() => {
      try {
        await accountService.getReviews()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      myReviews: computed(() => AppState.myReviews),
      saveChanges(field) {
        this.account[field] = event.target.textContent
        console.log(this.account)
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
