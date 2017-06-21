<template>
  <autocomplete-vue
      url="/bgg.json"
      requestType="get"
      property="name"
      :required="true"
      :threshold="4"
  ></autocomplete-vue>
</template>




<script>
  import AutocompleteVue from 'autocomplete-vue';

  export default {
    components: { AutocompleteVue },
    data() {
      return {
      }
    }
  };
</script>
