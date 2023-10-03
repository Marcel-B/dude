import { Typography } from "@mui/material";
import { useAppSelector } from "app-store";
import { parsedDate } from "werkzeug/index";
import { format } from "date-fns";

export function Eintrag() {
  const {datum} = useAppSelector(state => state.eintrag);

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

export default Eintrag;
