import { Pbi } from "domain/pbi";

export interface PbiForGrid extends Pbi {
  beschreibung: string;
  projektName: string;
}
