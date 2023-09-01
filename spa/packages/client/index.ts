import axios from "axios";
import { eintraege } from "./eintraege";
import { pbis } from "./pbi";
import { projekte } from "./projekt";
import { abrechnung } from "./abrechnung";
import { getAccessToken } from "auth";

const instance = axios.create({
  baseURL: "http://192.168.2.103:8067",
  timeout: 1000,
});

// Request-Interceptor
instance.interceptors.request.use(
  async (config) => {
    const token = await getAccessToken();
    console.info('==== Client token', token);
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export const get = async <Out>(path: string): Promise<Out> => {
  const response = await instance.get<Out>(path);
  const r = response.data;
  return r;
};

export const post = async <In, Out>(path: string, content: In): Promise<Out> => {
  const response = await instance.post<Out>(path, content);
  const r = response.data;
  return r;
};

export const del = async (path: string): Promise<void> => {
  await instance.delete(path);
};


const client = {
  async get<T>(url: string) {
    return instance.get<T>(url).then((res) => res.data)
  }
}

export const apiClient = {
  abrechnung,
  eintraege,
  projekte,
  pbis,
  client
};



