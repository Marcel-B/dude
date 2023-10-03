import { Box, Stack, Typography } from "@mui/material";
import QueryBuilderIcon from "@mui/icons-material/QueryBuilder";
import EuroIcon from "@mui/icons-material/Euro";
import { Fragment } from "react";

export interface IProps {
  stunden: number;
  stundensatz: number;
  title: string;
}

export function ZusammenfassungItem({ stunden, stundensatz, title }: IProps) {
  const money = (t: number) => {
    if (t && stundensatz) {
      return new Intl.NumberFormat("de-DE", { style: "currency", currency: "EUR" }).format(t * stundensatz);
    }
    return "0";
  };

  const hours = (t: number) => {
    if (t) {
      return new Intl.NumberFormat("de-DE", { style: "decimal" }).format(t);
    }
    return "0";
  };

  return (
    <Fragment>
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Box sx={{ display: "flex" }}>
          <QueryBuilderIcon sx={{ mt: .5, mr: .5, fontSize: "1rem" }} />
          <Typography variant="body1">{title}</Typography>
        </Box>
        <Typography variant="body1">{hours(stunden)} h</Typography>
      </Stack>
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Box sx={{ display: "flex" }}>
          <EuroIcon sx={{ mt: .5, mr: .5, fontSize: "1rem" }} />
          <Typography variant="body1">{title}</Typography>
        </Box>
        <Typography variant="body1">{money(stunden)}</Typography>
      </Stack>
    </Fragment>
  );
}

export default ZusammenfassungItem;
