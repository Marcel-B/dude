import { get, post, del } from "../index";
import { Pbi } from "domain/pbi";
import { PbiDto } from "./models";

export const pbis = {
  async getPbis(): Promise<Pbi[]> {
    return await get<Pbi[]>("/api/v1/pbi");
  },
  async addPbi(pbi: PbiDto): Promise<Pbi> {
    return await post<PbiDto, Pbi>("/api/v1/pbi", pbi);
  },
  async deletePbi(id: number): Promise<void> {
    return await del(`/api/v1/pbi/${id}`);
  }
};