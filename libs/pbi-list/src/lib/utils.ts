export const toBranch = (name: string): string => {


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
