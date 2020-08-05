<template>
  <b-container id="app">
    <b-row>
      <b-col>
        <NavigationBar />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <router-view />
      </b-col>
    </b-row>
  </b-container>
</template>


<script>
import NavigationBar from "./components/NavigationBar";
export default {
  name: "App",
  components: { NavigationBar },
};
</script>

