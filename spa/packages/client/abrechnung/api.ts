import { get } from "../index";

export const abrechnung = {
  async getByKalenderwoche(kalenderwoche: number, jahr: number, text: string): Promise<number> {
    if (!jahr) throw new Error("jahr is required");
    if (!text) throw new Error("text is required");
    if (!kalenderwoche) throw new Error("kalenderwoche is required");

    const params = new URLSearchParams();
    params.append("kalenderwoche", kalenderwoche.toString());
    params.append("jahr", jahr.toString());
    params.append("text", text);
    const result = await get<{ stunden: number }>(`/api/v1/abrechnung/by-kalenderwoche?${params.toString()}`);
    return result.stunden;
  },
  async getByMonat(monat: number, jahr: number, text: string): Promise<number> {
    if (!jahr) throw new Error("jahr is required");
    if (!text) throw new Error("text is required");
    if (!monat) throw new Error("monat is required");

    const params = new URLSearchParams();
    params.append("jahr", jahr.toString());
    params.append("monat", monat.toString());
    params.append("text", text);

    const result = await get<{ stunden: number }>(`/api/v1/abrechnung/by-monat?${params.toString()}`);
    return result.stunden;
  },
  async getByJahr(jahr: number, text: string): Promise<number> {
    if (!jahr) throw new Error("jahr is required");
    if (!text) throw new Error("text is required");

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
