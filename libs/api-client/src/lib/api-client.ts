import axios from "axios";
import { Eintrag, Pbi, Projekt } from "@dude/stunden-domain";
import { PbiDto } from "./dto/pbi-dto";

const axiosInstance = axios.create({ baseURL: "http://192.168.2.103:3333" });
//const axiosInstance = axios.create({ baseURL: "http://localhost:3333" });

const get = async <Out>(path: string): Promise<Out> => {
  const response = await axiosInstance.get<Out>(path);
  const r = response.data;
  return r;
};

const post = async <In, Out>(path: string, content: In): Promise<Out> => {
  const response = await axiosInstance.post<Out>(path, content);
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
    return await post<Projekt, Projekt>("/api/projekt", projekt);
  },
  async deleteProjekt(id: string): Promise<void> {
    return await del(`/api/projekt/${id}`);
  }
};

const pbis = {
  async getPbis(): Promise<Pbi[]> {
    return await get<Pbi[]>("/api/pbi");
  },
  async addPbi(pbi: PbiDto): Promise<Pbi> {
    return await post<PbiDto, Pbi>("/api/pbi", pbi);
  },
  async deletePbi(id: string): Promise<void> {
    return await del(`/api/pbi/${id}`);
  }
};

export const apiClient = {
  stunden,
  projekte,
  pbis
};


