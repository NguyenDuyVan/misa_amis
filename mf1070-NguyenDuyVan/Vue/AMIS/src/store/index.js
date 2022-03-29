const indexStore = {
  state() {
    return {
      isSaveAndAddEvent: false,
      isDataChanged: false,
      keyupWhere: null,
    };
  },
  mutations: {
    // Cập nhật state: isSaveAndAddEvent khi nhấn CẤT VÀ THÊM
    updateSaveEvent(state, bool) {
      state.isSaveAndAddEvent = bool;
    },

    // Cập nhật state: isDataChanged khi dữ liệu trong form có sự thay đổi
    updateDataState(state, bool) {
      state.isDataChanged = bool;
    },

    // Cập nhật state: keyupWhere khi có sự kiện Keyup
    updateWhereKeyup(state, where) {
      state.keyupWhere = where;
    },
  },
  actions: {
    // Gọi mutation: updateSaveEvent khi nhấn CẤT VÀ THÊM
    updateSaveEvent({ commit }, bool) {
      commit("updateSaveEvent", bool);
    },

    // Gọi mutation: updateDataState khi dữ liệu trong form có sự thay đổi
    updateDataState({ commit }, bool) {
      commit("updateDataState", bool);
    },

    // Gọi mutation: updateWhereKeyup khi có sự kiện Keyup
    updateWhereKeyup({commit}, where) {
      commit("updateWhereKeyup", where);
    },
  },
};

export default indexStore;
