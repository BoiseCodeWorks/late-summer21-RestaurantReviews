import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
class RestaurantsService {
  async getAll() {
    const res = await api.get('api/restaurants')
    logger.log(res.data)
    AppState.restaurants = res.data
  }

  async getById(id) {
    // NOTE not required but skips a network call if not needed
    const found = AppState.restaurants.find(r => r.id.toString() === id)
    if (found) {
      AppState.activeRestaurant = found
      return
    }
    const res = await api.get(`api/restaurants/${id}`)
    logger.log(res.data)
    AppState.activeRestaurant = res.data
  }
}

export const restaurantsService = new RestaurantsService()
