import { del, get, post } from "../index";
import { Projekt } from "domain/projekt";

export const projekte = {
  async getProjekte(): Promise<Projekt[]> {
    return await get<Projekt[]>("/api/v1/projekt");
  },
  async addProjekt(projekt: Projekt): Promise<Projekt> {
    return await post<Projekt, Projekt>("/api/v1/projekt", projekt);
  },
  async deleteProjekt(id: number): Promise<void> {
    return await del(`/api/v1/projekt/${id}`);
  }
};
