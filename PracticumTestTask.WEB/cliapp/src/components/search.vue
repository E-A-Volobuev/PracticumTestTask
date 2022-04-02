<template>
  <v-container>
    <v-row style="height: 100px"></v-row>
    <v-row justify="center">
      <v-col cols="7">
        <v-card elevation="8">
          <v-card-title> Парсинг произвольных HTML страниц </v-card-title>
          <v-card-subtitle>
            Приложение, которое позволяет парсить HTML страницы и выдавать
            статистику по количеству произвольных слов
          </v-card-subtitle>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col md="9">
                  <v-text-field dense outlined v-model="text">
                    <template slot="label">
                      <v-icon>mdi-magnify</v-icon>Ведите URL, например
                      https://www.simbirsoft.com/
                    </template>
                  </v-text-field>
                </v-col>
                <v-col md="3">
                  <v-btn
                    elevation="2"
                    color="primary"
                    :loading="loading"
                    @click="fetchGetStatistics(text)"
                    ><v-icon large>mdi-cloud-print-outline</v-icon
                    >&nbsp;&nbsp;парсинг</v-btn
                  ></v-col
                >
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-btn
              color="blue-grey"
              class="ma-2 white--text"
              @click="historyShow = !historyShow"
            >
              История запросов
              <v-icon right dark large> mdi-clipboard-text-clock </v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
    <v-row justify="center" v-if="historyShow">
      <v-col cols="4">
        <v-card elevation="8">
          <v-card-title> История запросов </v-card-title>
          <v-list dense>
            <v-list-item v-for="(item, i) in historyResults" :key="i">
              <v-list-item-content>
                <v-list-item-title v-text="item.url"></v-list-item-title>
                <v-list-item-subtitle v-text="item.time"></v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action>
                <v-btn
                  icon
                  :loading="item.loadingResults"
                  @click="fetchGetResults(item)"
                >
                  <v-icon color="primary" x-large>mdi-location-enter</v-icon>
                </v-btn>
              </v-list-item-action>
              <v-list-item-action>
                <v-btn
                  icon
                   :loading="item.loadingDelete"
                  @click="fetchDelete(item,1)"
                >
                  <v-icon color="error" x-large>mdi-delete-circle</v-icon>
                </v-btn>
              </v-list-item-action>
            </v-list-item>
          </v-list>
        </v-card>
      </v-col>
    </v-row>
    <v-row justify="center" v-if="resultShow">
      <v-col cols="4">
        <v-card elevation="8">
          <v-card-title> Статистика повторений слов </v-card-title>
          <v-simple-table>
            <template v-slot:default>
              <thead>
                <tr>
                  <th class="text-left">Значение</th>
                  <th class="text-left">Количество повторений</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="item in results" :key="item.id">
                  <td>{{ item.value }}</td>
                  <td>{{ item.count }}</td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="resultShow = false">
              закрыть
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import SiteUrl from "../settings/site-url.settings";

export default {
  name: "search-word",

  data: () => ({
    text: "",
    loading: false,
    resultShow: false,
    historyShow: false,
    results: [],
    historyResults: [],
  }),
  created() {
    this.fetchHistoryCreated();
  },
  methods: {
    async fetchGetStatistics(url) {
      this.loading = true;
      const r = await fetch(SiteUrl.getStatistics() + url, {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });

      if (r.ok === true) {
        await this.fetchGetHistoryRequest();
      } else {
        alert("ошибка парсинга html-страницы");
      }
    },
    async fetchGetHistoryRequest() {
      const responce = await fetch(SiteUrl.getHistoryRequest(), {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });

      if (responce.ok === true) {
        this.historyShow = true;
        let r = await responce.json();

        this.historyResults = r.map((item) => {
          item.loadingResults = false;
          item.loadingDelete = false;
          return item;
        });

        this.loading = false;
      } else {
        alert("ошибка получения истории запросов");
      }
    },
    async fetchHistoryCreated() {
      const responce = await fetch(SiteUrl.getHistoryRequest(), {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });
      if (responce.ok === true) {
        let r = await responce.json();

        this.historyResults = r.map((item) => {
          item.loadingResults = false;
          item.loadingDelete = false;
          return item;
        });

      } else {
        alert("ошибка получения истории запросов");
      }
    },
    async fetchGetResults(item) {
      item.loadingResults = true;
      const responce = await fetch(SiteUrl.getResults() + item.id, {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });

      if (responce.ok === true) {
        this.resultShow = true;
        let r = await responce.json();
        this.results = r;
      } else {
        alert("ошибка получения информации о запросе");
      }

      item.loadingResults = false;
    },
    async fetchDelete(item,num) {
      item.loadingDelete = true;
      this.historyResults.splice(item, num)
      const responce = await fetch(SiteUrl.delete() + item.id, {
        method: "DELETE",
        headers: {
          Accept: "application/json",
        },
      });

      if (responce.ok === true) {
        this.resultShow = false;
        item.loadingDelete = false;
      } else {
        alert("ошибка удаления");
      }
    },
  },
};
</script>