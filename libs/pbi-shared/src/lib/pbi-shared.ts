export function pbiShared(): string {
  return "pbi-shared";
}

export interface Pbi {
  id: number;
  name: string;
  project: string;
  description?: string;
}

export default Pbi;
