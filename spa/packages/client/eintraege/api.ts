import { del, get, post } from "../index";
import { Eintrag } from "domain/stunden";

export const eintraege = {
  async getEintraege(): Promise<Eintrag[]> {
    return await get<Eintrag[]>("/api/v1/eintrag");
  },
  async getEintrag(id: string): Promise<Eintrag> {
    return await get<Eintrag>(`/api/v1/eintrag/${id}`);
  },
  async addEintrag(eintrag: Eintrag): Promise<Eintrag> {
    return await post<Eintrag, Eintrag>("/api/v1/eintrag", eintrag);
  },
  async deleteEintrag(id: string): Promise<void> {
    return await del(`/api/v1/eintrag/${id}`);
  }
};
