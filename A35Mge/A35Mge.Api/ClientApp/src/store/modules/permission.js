import { generatorDynamicRouter } from '@/router/generator-routers'
const permission = {
  state: {
    routers: [],
    addRouters: []
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
      state.routers = routers
    }
  },
  actions: {
    GenerateRoutes ({ commit }) {
      return new Promise(async resolve => {
        var rs = await generatorDynamicRouter()
        console.log('route', rs)
        commit('SET_ROUTERS', rs)
        resolve()
      })
    }
  }
}

export default permission
