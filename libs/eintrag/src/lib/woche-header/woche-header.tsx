import { useAppSelector } from "@dude/store";
import { parsedDate } from "@dude/util";
import { format } from "date-fns";
import { Typography } from "@mui/material";

export function WocheHeader() {
  const { datum } = useAppSelector(state => state.eintrag);

  const getTitel = () => {
    const date = parsedDate(datum);
    const jahr = format(date, "yyyy");
    const kalenderWoche = format(date, "w");
    return kalenderWoche + ". KW " + jahr;
  };

  return (
    <Typography variant="h3">{getTitel()}</Typography>
  );
}

export default WocheHeader;
