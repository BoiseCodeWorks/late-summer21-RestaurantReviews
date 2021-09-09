import { logger } from '../utils/Logger'
import Pop from '../utils/Notifier'
import { SocketHandler } from '../utils/SocketHandler'

class SocketService extends SocketHandler {
  constructor() {
    super()
    this
      .on('error', this.onError)
  }

  onError(e) {
    logger.error('[SOCKET_ERROR]', e)
    Pop.toast(e.message, 'error')
  }
}

// export const socketService = new SocketService()
