import { get, post, del } from "../index";
import { Pbi } from "domain/pbi/index";
import { PbiDto } from "./models";

export const pbis = {
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