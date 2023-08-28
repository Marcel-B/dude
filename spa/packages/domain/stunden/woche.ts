import { Tag } from "./tag";
import { Wochentag } from "./wochentag";

export class Woche {
  static readonly Montag = new Wochentag("Montag", Tag.Montag);
  static readonly Dienstag = new Wochentag("Dienstag", Tag.Dienstag);
  static readonly Mittwoch = new Wochentag("Mittwoch", Tag.Mittwoch);
  static readonly Donnerstag = new Wochentag("Donnerstag", Tag.Donnerstag);
  static readonly Freitag = new Wochentag("Freitag", Tag.Freitag);
  static readonly Samstag = new Wochentag("Samstag", Tag.Samstag);
}

