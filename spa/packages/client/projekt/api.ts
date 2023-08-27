import { del, get, post } from "../index";
import { Projekt } from "domain/projekt";

export const projekte = {
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
