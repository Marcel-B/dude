import axios from "axios";

const instance = axios.create({
  baseURL: "http://192.168.2.103:8067",
  timeout: 1000,
});

export const client = {
  get: <T>(url: string) => instance.get<T>(url).then((res) => res.data),
}

