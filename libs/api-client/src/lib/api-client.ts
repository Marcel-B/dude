import axios from "axios";
import { Eintrag, Projekt } from "@dude/stunden-domain";

//const axiosInstance = axios.create({ baseURL: "http://192.168.2.103:3333" });
const axiosInstance = axios.create({ baseURL: "http://localhost:3333" });

const get = async <T>(path: string): Promise<T> => {
  const response = await axiosInstance.get<T>(path);
  const r = response.data;
  return r;
};

const post = async <T>(path: string, content: T): Promise<T> => {
  const response = await axiosInstance.post<T>(path, content);
  const r = response.data;
  return r;
};

const del = async (path: string): Promise<void> => {
  await axiosInstance.delete(path);
};

const stunden = {
  async getEintraege(): Promise<Eintrag[]> {
    return await get<Eintrag[]>("/api/eintrag");
  },
  async getEintrag(id: string): Promise<Eintrag> {
    return await get<Eintrag>(`/api/eintrag/${id}`);
  },
  async deleteEintrag(id: string): Promise<void> {
    return await del(`/api/eintrag/${id}`);
  }
};

const projekte = {
  async getProjekte(): Promise<Projekt[]> {
    return await get<Projekt[]>("/api/projekt");
  },
  async addProjekt(projekt: Projekt): Promise<Projekt> {
    return await post<Projekt>("/api/projekt", projekt);
  }
};

export const apiClient = {
  stunden,
  projekte
};


