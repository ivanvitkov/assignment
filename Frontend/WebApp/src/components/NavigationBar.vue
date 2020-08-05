<template>
  <b-navbar toggleable="md" type="dark" variant="dark">
    <b-navbar-brand :to="{name: 'Home'}">Home</b-navbar-brand>
    <b-navbar-nav>
      <b-nav-item :to="{name: 'ManufacturerList'}">Cars</b-nav-item>
      <b-nav-item href="#">Taxi optimization</b-nav-item>
    </b-navbar-nav>
  </b-navbar>
</template>

