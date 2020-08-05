import Axios from 'axios';

const URL = '/models';

export default {
  getAllModels() {
    return Axios.get(URL);
  },
  getById(id) {
    return Axios.get(`${URL}/model/${id}`);
  },
  create(id, data) {
    return Axios.post(URL + "/" + id, data);
  },

  update(id, data) {
    return Axios.put(`${URL}/model/${id}`, data);
  },

  delete(id) {
    return Axios.delete(URL + '/model/' + id);
  }

}