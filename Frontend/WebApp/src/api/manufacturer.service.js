import Axios from 'axios';

const URL = '/manufacturers';

export default {
  getAll() {
    return Axios.get(URL);
  },
  get(id) {
    return Axios.get(`${URL}/${id}`);
  },
  getModels(id) {
    return Axios.get(`${URL}/${id}/models`);
  },
  create(data) {
    return Axios.post(URL, data);
  },

  update(id, data) {
    return Axios.put(`${URL}/${id}`, data);
  },

  delete(id) {
    return Axios.delete(URL + "/" + id);
  }

}