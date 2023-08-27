import { del, get, post } from "../index";
import { Eintrag } from "domain/stunden";

export const eintraege = {
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
