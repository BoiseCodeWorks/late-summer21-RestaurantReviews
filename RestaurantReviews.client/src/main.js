import 'bootstrap'
import 'jquery'
import 'popper.js'
import 'sweetalert2/dist/sweetalert2.min.css'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'

const root = createApp(App)
registerGlobalComponents(root)

root
  .use(router)
  .mount('#app')
