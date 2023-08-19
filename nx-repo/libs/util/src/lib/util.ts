import { Tag, Wochentag } from "@dude/stunden-domain";
import parseIso from "date-fns/parseISO";
import { addHours, addWeeks, format, lastDayOfWeek, startOfDay, subDays } from "date-fns";

export const toBranch = (name: string): string => {
  const regexFalseChars = /[/:#']/g;
  const regexDoubleSpace = /\s+/g;

  const strPerfect = name
    .replace(regexFalseChars, " ")
    .trim()
    .replace(regexDoubleSpace, "-")
    .trim();
  return strPerfect;
  // return strPerfect.replace(regexSpace, "-");
};

export const parsedDate = (date: string): Date => {
  return setCommonTime(parseIso(date));
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
  return format(getDateByWochentag(wochentag, datum), "dd.MM.");
};
export const getFormattedDateByDate = (date: string): string => {
  return date ?
    format(parsedDate(date), "dd.MM.") : "";
};

export const getSonntag = (datum: string): Date => {
  return setCommonTime(lastDayOfWeek(parsedDate(datum), { weekStartsOn: 1 }));
};
export const getDateByTag = (datum: string, tag: Tag): Date => {
  return subDays(getSonntag(datum), tag);
};
export const getDateByTagISO = (datum: string, tag: Tag): string => {
  return subDays(getSonntag(datum), tag).toISOString();
};

export const setCommonTime = (date: Date): Date => {
  return addHours(startOfDay(date), 12);
};

export const getDateTimeAsISO = (date: Date): string => {
  return date.toISOString();
};

export const addWeek = (date: Date): Date => {
  return addWeeks(date, 1);
};

export const subWeek = (date: Date): Date => {
  return addWeeks(date, -1);
};

export const changeWeek = (date: Date, change: number): Date => {
  return addWeeks(date, change);
};

export const sameDate = (date1: Date, date2: Date): boolean => {
  return format(date1, "yyyy-MM-dd") === format(date2, "yyyy-MM-dd");
};

export const getTodayAsISO = (): string => {
  return getDateTimeAsISO(setCommonTime(new Date()));
};

export const getAsYear = (date: Date): string => {
  return format(date, "yyyy");
};

export const getAsCalendarWeek = (date: Date): string => {
  return format(date, "ww");
};
