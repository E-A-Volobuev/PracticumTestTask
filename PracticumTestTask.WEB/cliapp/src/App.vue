<template>
  <v-app>
    <v-app-bar
      app
    >
      <div class="d-flex align-center">
        <v-img
          alt="Simbirsoft Logo"
          class="shrink mr-2"
          contain
          :src="require('./assets/logo.png')"
          transition="scale-transition"
          width="60"
        />
        <v-img
          alt="Simbirsoft Name"
          class="shrink mr-2"
          contain
          :src="require('./assets/name.png')"
          transition="scale-transition"
          width="200"
        />
      </div>
    </v-app-bar>

    <v-main>
      <search/>
    </v-main>
  </v-app>
</template>

<script>
import Search from './components/search.vue';

export default {
  name: 'App',

  components:{
    Search
  },

  data: () => ({
    //
  }),
};
</script>
