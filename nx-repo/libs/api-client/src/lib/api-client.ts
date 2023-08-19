import axios from "axios";
import { Eintrag, Pbi, Projekt } from "@dude/stunden-domain";
import { PbiDto } from "./dto/pbi-dto";

const axiosInstance = axios.create({ baseURL: "http://192.168.2.103:8054" });
//const axiosInstance = axios.create({ baseURL: "http://localhost:5263" });

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

const eintraege = {
  async getEintraege(): Promise<Eintrag[]> {
    return await get<Eintrag[]>("/api/eintrag");
  },
  async getEintrag(id: string): Promise<Eintrag> {
    return await get<Eintrag>(`/api/eintrag/${id}`);
  },
  async addEintrag(eintrag: Eintrag): Promise<Eintrag> {
    return await post<Eintrag, Eintrag>("/api/eintrag", eintrag);
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

const abrechnung = {
  async getByKalenderwoche(kalenderwoche: number, jahr: number, text: string): Promise<number> {
    if(!jahr) throw new Error("jahr is required");
    if(!text) throw new Error("text is required");
    if(!kalenderwoche) throw new Error("kalenderwoche is required");

    const params = new URLSearchParams();
    params.append("kalenderwoche", kalenderwoche.toString());
    params.append("jahr", jahr.toString());
    params.append("text", text);
    const result = await get<{ stunden: number }>(`/api/abrechnung/by-kalenderwoche?${params.toString()}`);
    return result.stunden;
  },
  async getByMonat(monat: number, jahr: number, text: string): Promise<number> {
    if(!jahr) throw new Error("jahr is required");
    if(!text) throw new Error("text is required");
    if(!monat) throw new Error("monat is required");

    const params = new URLSearchParams();
    params.append("jahr", jahr.toString());
    params.append("monat", monat.toString());
    params.append("text", text);

    const result = await get<{ stunden: number }>(`/api/abrechnung/by-monat?${params.toString()}`);
    return result.stunden;
  },
  async getByJahr(jahr: number, text: string): Promise<number> {
    if(!jahr) throw new Error("jahr is required");
    if(!text) throw new Error("text is required");

    const params = new URLSearchParams();
    params.append("jahr", jahr.toString());
    params.append("text", text);
    const result = await get<{ stunden: number }>(`/api/abrechnung/by-jahr?${params.toString()}`);
    return result.stunden;
  },
  async getProjekte(): Promise<string[]> {
    return await get<string[]>("/api/abrechnung/projekte");
  }
};

export const apiClient = {
  abrechnung,
  eintraege,
  projekte,
  pbis
};


