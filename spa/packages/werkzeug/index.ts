export * from "./dateWerkzeug";

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

