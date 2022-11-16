<template>
  <div>
    <el-button
      icon="el-icon-circle-check"
      round
      :disabled="selectedIDs.length == 0"
      @click="handleChangeStatus(true)"
      >Статус активный</el-button
    >
    <el-button
      icon="el-icon-circle-close"
      round
      :disabled="selectedIDs.length == 0"
      @click="handleChangeStatus(false)"
      >Статус пассивный</el-button
    >
    <table>
      <tr>
        <th>
          <input
            type="checkbox"
            @change="handleSelectAll($event)"
            v-model="isSelectedAll"
          />
        </th>
        <th
          v-for="header in headers"
          :key="header.name"
          @click="handleSort(header.name)"
        >
          {{ header.title }}
        </th>
      </tr>
      <tr v-for="item in sortedData" :key="item.id">
        <td>
          <input
            type="checkbox"
            :checked="selectedIDs.indexOf(item.id) > -1"
            @change="handleChooseRow($event, item.id)"
          />
        </td>
        <td>{{ item.number }}</td>
        <td @click="handleOpenEdit(item)">{{ item.name }}</td>
        <td>{{ item.date.substring(0,10) }}</td>
        <td>{{ item.status ? "Активный" : "Пассивный" }}</td>
      </tr>
    </table>
    <EditDialog :open="isOpen" @onClose="handleClose" :data="editData" />
  </div>
</template>

<script>
import EditDialog from '../components/Dialog.vue';
export default {
  components:{EditDialog},
  data() {
    return {
      headers: [
        { title: "номер", name: "number" },
        { title: "название", name: "name" },
        { title: "дата", name: "date" },
        { title: "статус", name: "status" },
      ],
      data: [],
      isSelectedAll: false,
      selectedIDs: [],
      sortProp: null,
      sortDirect: true,
      isOpen:false,
      editData:null,
    };
  },
  methods: {
    reloadData() {
      this.$get("api/WeatherForecast/tabledata").then((resp) => {
        this.data = resp.data;
        this.selectedIDs = [];
        this.isSelectedAll = false;
      });
    },
    handleSort(prop) {
      if (this.sortProp == prop) {
        this.sortDirect = !this.sortDirect;
      } else {
        this.sortProp = prop;
        this.sortDirect = true;
      }
    },
    handleSelectAll(e) {
      const { checked } = e.target;
      if (checked) {
        this.selectedIDs = [];
        if (this.selectedIDs.length === 0) {
          this.data.map((row) => {
            this.selectedIDs.push(row.id);
          });
        }
      } else {
        this.selectedIDs = [];
      }
    },
    handleChooseRow(e, id) {
      this.isSelectedAll = false;
      const { checked } = e.target;
      if (!checked) {
        this.selectedIDs = this.selectedIDs.filter((x) => x != id);
      } else {
        this.selectedIDs.push(id);
      }
    },
    handleChangeStatus(val) {
      this.$put(`api/WeatherForecast/tabledata`, {
        status: val,
        ids: this.selectedIDs,
      }).then(() => {
        this.reloadData();
      });
    },
    handleOpenEdit(val){
      this.editData = val;
      this.isOpen = true;
    },
    handleClose(){
      this.isOpen = false;
      this.editData = null;
      this.reloadData();
    }
  },
  computed: {
    sortedData() {
      if (this.sortProp == null) return this.data;

      return this.data.sort((a, b) => {
        let mod = 1;
        if (!this.sortDirect) mod = -1;

        if (a[this.sortProp] < b[this.sortProp]) return -1 * mod;
        if (a[this.sortProp] > b[this.sortProp]) return 1 * mod;
        return 0;
      });
    },
  },
  mounted() {
    this.reloadData();
  },
};
</script>

<style scoped>
table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td,
th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
  cursor: pointer;
}

tr:nth-child(even) {
  background-color: #dddddd;
}
</style>