<template>
  <div>
    <b-row>
      <b-col md="2">
        <b-button v-b-modal.modal-prevent-closing class=" mt-2 btn btn-success">Add new</b-button>
        <b-modal
          id="modal-prevent-closing"
          ref="modal"
          title="Add new manufacturer"
          @show="resetModal"
          @hidden="resetModal"
          @ok="handleOk"
        >
          <form ref="form" @submit.stop.prevent="handleSubmit">
            <b-form-group
              :state="isValid"
              label="Name"
              label-for="name-input"
              invalid-feedback="Manufacturer name is required"
            >
              <b-form-input
                id="name-input"
                v-model="newManufacturer.name"
                :state="isValid"
                required
              ></b-form-input>
            </b-form-group>
           
          </form>
        </b-modal>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col md="12">
        <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>Id</th>
                <th>Name</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="manufacturer in manufacturers" :key="manufacturer.id">
                <td>{{ manufacturer.id }}</td>
                <td>
                  <router-link
                    :to="{name: 'ModelList', params: {id: manufacturer.id}}"
                    activeClass="active"
                  >
                    <a>{{ manufacturer.name }}</a>
                  </router-link>
                </td>
                <td class="float-right">
                  <b-button>Edit</b-button>
                  <b-button class="btn btn-danger" v-on:click="deleteManufacturer(manufacturer.id)">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    </b-row>
    <div></div>
  </div>
</template>


<script>
import ManufacturerService from "../api/manufacturer.service";
export default {
  name: "Manufacturers",

  props: ["manufacturer"],
  data: () => {
    return {
      manufacturers: [],
      modalShow: false,
      isValid: null,
      newManufacturer: {name:''},
    };
  },

  created() {
    this.fetchManufacturers();
  },
  methods: {
     
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.isValid = valid;
      return valid;
    },
    resetModal() {
      this.newManufacturer.name = '',
      this.isValid = null;
    },
    handleOk(bvModalEvt) {
      // Prevent modal from closing
      bvModalEvt.preventDefault();
      // Trigger submit handler
      this.handleSubmit();
    },
    handleSubmit() {
      // Exit when the form isn't valid
      if (!this.checkFormValidity()) {
        return;
      }
      // Push the name to submitted names
      this.addManufacturer();
      // Hide the modal manually
      this.$nextTick(() => {
        this.$bvModal.hide("modal-prevent-closing");
      });
    },

    addManufacturer() {
      ManufacturerService.create(this.newManufacturer)
        .then((response) => {
          console.log("new manufacturer added");
         this.fetchManufacturers();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    fetchManufacturers() {
      ManufacturerService.getAll()
        .then((response) => {
          console.log(response);
          this.manufacturers = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteManufacturer(id) {
      ManufacturerService.delete(id)
        .then((response) => {
          console.log("deleted");
          this.fetchManufacturers();
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>