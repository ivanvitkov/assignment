<template>
  <div>
    <b-row>
      <b-col md="12">
        <b-button class="btn-success mt-2" v-b-modal.modal-prevent-closing>Open Modal</b-button>
        <b-modal
          id="modal-prevent-closing"
          ref="modal"
          title="Submit Your Name"
          @show="resetModal"
          @hidden="resetModal"
          @ok="handleOk"
        >
          <form ref="form" @submit.stop.prevent="handleSubmit">
            <b-form-group
              :state="isValid"
              label="Name"
              label-for="name-input"
              invalid-feedback="Car name is required"
            >
              <b-form-input
                id="name-input"
                v-model="newModel.name"
                :state="isValid"
                required
              ></b-form-input>
            </b-form-group>
            <b-form-group
              :state="isValid"
              label="Color"
              label-for="color-input"
              invalid-feedback="Car color is required"
            >
              <b-form-input
                id="color-input"
                v-model="newModel.color"
                :state="isValid"
                required
              ></b-form-input>
            </b-form-group>
            <b-form-group
              :state="isValid"
              label="Production year"
              label-for="year-input"
              invalid-feedback="Year is required"
            >
              <b-form-input
                id="year-input"
                v-model.number="newModel.year"
                :state="isValid"
                type="number"
              ></b-form-input>
            </b-form-group>
          </form>
        </b-modal>


        <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Color</th>
                 <th>Year</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="model in models" :key="model.id">
                <td>{{ model.id }}</td>
                <td>{{ model.name }}</td>
                <td> {{model.color}} </td>
                <td> {{model.year}} </td>
                <td class="float-right">
                  <b-button>Edit</b-button>
                  <b-button class="btn-danger" v-on:click="deleteModel(model.id)">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    </b-row>
  </div>
</template>



<script>
import ManufacturerService from "../api/manufacturer.service";
import ModelsService from "../api/models.service";
export default {
  data: () => {
    return {
      models: [],
      modalShow: false,
      isValid: null,
      newModel: { name:'', color:'', year: 0},
    };
  },
  watch: {},
  created() {
    this.fetchModels();
  },
  methods: {
    checkFormValidity() {
      const valid = this.$refs.form.checkValidity();
      this.isValid = valid;
      console.log(parseInt(this.newModel.year));
     
      return valid;
    },
    resetModal() {
      this.newModel = { name:'', color:'', year: 0 },
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
      this.addModel();
      // Hide the modal manually
      this.$nextTick(() => {
        this.$bvModal.hide("modal-prevent-closing");
      });
    },
    addModel(){
      ModelsService.create(this.$route.params.id, this.newModel).then(response => {
        console.log("New model created");
        this.fetchModels();

      }).catch(error => {
        console.log(this.newModel);
        console.log(this.$route.params.id);
        console.log(error);
      })
    },
    fetchModels() {
      ManufacturerService.getModels(this.$route.params.id)
        .then((response) => {
          console.log(response.data.carModels);
          this.models = response.data.carModels;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    deleteModel(id) {
      ModelsService.delete(id)
        .then((response) => {
          console.log(`Model with ID: ${id} deleted`);
          this.fetchModels();
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>