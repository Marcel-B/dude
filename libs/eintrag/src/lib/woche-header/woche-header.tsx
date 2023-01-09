import { setDatum, useAppDispatch, useAppSelector } from "@dude/store";
import { parsedDate } from "@dude/util";
import { format, startOfDay } from "date-fns";
import { IconButton, Stack, Typography } from "@mui/material";
import TodayIcon from "@mui/icons-material/Today";

export function WocheHeader() {
  const { datum } = useAppSelector(state => state.eintrag);
  const dispatch = useAppDispatch();

  const changeToToday = () => {
    const date = parsedDate(datum);
    const newDatum = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    dispatch(setDatum(startOfDay(new Date()).toISOString()));
  };

  const getTitel = () => {
    const date = parsedDate(datum);
    const jahr = format(date, "yyyy");
    const kalenderWoche = format(date, "w");
    return kalenderWoche + ". KW " + jahr;
  };

  return (
    <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
      <Typography variant="h3">{getTitel()}</Typography>
      <IconButton onClick={() => changeToToday()} sx={{mt: 2}}>
        <TodayIcon />
      </IconButton>
    </Stack>
  );
}

export default WocheHeader;
