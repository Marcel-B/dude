import { Tag, Wochentag } from "@dude/stunden-domain";
import parseIso from "date-fns/parseISO";
import { format, lastDayOfWeek, startOfDay, subDays } from "date-fns";

export const toBranch = (name: string): string => {
  name = name.replace("#", "");
  //const regexSpace = /\b \b/g;
  const regexFalseChars = /[/:]/g;
  const regexDoubleSpace = /\s+/g;

  const strPerfect = name
    .replace(regexFalseChars, " ")
    .replace(regexDoubleSpace, "-")
    .trim();
  return strPerfect;
  // return strPerfect.replace(regexSpace, "-");
};

export const parsedDate = (date: string): Date => {
  return startOfDay(parseIso(date));
};

export const getDateByWochentag = (wochentag: Wochentag, datum: string): Date => {
  const date = parsedDate(datum);
  const sonntag = lastDayOfWeek(date, { weekStartsOn: 1 });
  return subDays(sonntag, wochentag.tag);
};

export const formatStunden = (stunden: number) => {
  const h = Math.floor(stunden);
  const m = Math.round((stunden - h) * 60);
  return `${h}h ${m}m`;
};

export const getFormattedDate = (wochentag: Wochentag, datum: string): string => {
  const date = getDateByWochentag(wochentag, datum);
  return format(date, "dd.MM.");
};

export const getSonntag = (datum: string): Date => {
  const date = parsedDate(datum);
  return lastDayOfWeek(date, { weekStartsOn: 1 });
};
export const getDateByTag = (datum: string, tag: Tag): Date => {
  return subDays(getSonntag(datum), tag);
};
export const getDateByTagISO = (datum: string, tag: Tag): string => {
  return subDays(getSonntag(datum), tag).toISOString();
};
export const sameDate = (date1: Date, date2: Date): boolean => {
  return format(date1, "yyyy-mm-dd") === format(date2, "yyyy-mm-dd");
};
