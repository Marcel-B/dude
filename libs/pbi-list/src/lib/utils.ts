export const toBranch = (name: string): string => {


  const regexSpace = /\b \b/g;
  const regexDash = /\//g;

  //const regex = / /g;
  //const regex = /\\s{2,}/;
  const strPerfect = name.replace(regexDash, "").replace(/\s+/g, " ").trim();
  return strPerfect.replace(regexSpace, "-");
};
