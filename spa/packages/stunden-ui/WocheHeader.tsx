import { setDatum, useAppDispatch, useAppSelector } from "app-store";
import { getAsCalendarWeek, getAsYear, getTodayAsIso, parsedDate } from "werkzeug/index";
import { IconButton, Stack, Typography } from "@mui/material";
import TodayIcon from "@mui/icons-material/Today";

export function WocheHeader() {
  const { datum } = useAppSelector(state => state.eintrag);
  const dispatch = useAppDispatch();

  const changeToToday = () => {
    dispatch(setDatum(getTodayAsIso()));
  };

  const getTitel = () => {
    const date = parsedDate(datum);
    const jahr = getAsYear(date);
    const kalenderWoche = getAsCalendarWeek(date);
    return kalenderWoche + ". KW " + jahr;
  };

  return (
    <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
      <Typography variant="h3">{getTitel()}</Typography>
      <IconButton onClick={() => changeToToday()} sx={{ mt: 2 }}>
        <TodayIcon />
      </IconButton>
    </Stack>
  );
}

export default WocheHeader;
