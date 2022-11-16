<template>
  <el-dialog
    title="Редактирование"
    :visible.sync="open"
    width="30%"
    center
    @close="handleClose"
    @open="handleOpen"
  >
    <div><el-input placeholder="Please input" v-model="input"></el-input></div>
    <span slot="footer" class="dialog-footer">
      <el-button @click="handleClose">Закрыть</el-button>
      <el-button type="primary" @click="handleSave">Сохранить</el-button>
    </span>
  </el-dialog>
</template>

<script>
export default {
  props: ["open", "onClose", "data"],
  data() {
    return {
      input: "",
    };
  },
  methods: {
    handleClose() {
      this.$emit("onClose");
    },
    handleOpen() {
      this.input = this.data.name;
    },
    handleSave() {
      this.$put(`api/WeatherForecast/tabledataname?id=${this.data.id}`, {
        name: this.input,
      }).then(() => {
        this.$message("Данные успешно обновлены");
        this.$emit("onClose");
      });
    },
  },
};
</script>