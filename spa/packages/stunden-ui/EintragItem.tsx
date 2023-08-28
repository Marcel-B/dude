import { formatStunden } from "werkzeug/index";
import { Paper, Stack, Typography } from "@mui/material";
import EuroIcon from "@mui/icons-material/Euro";
import React from "react";

interface IProps {
  text: string;
  stunden: number;
  abrechenbar: boolean;
  style?: React.CSSProperties;
}

export function EintragItem({ text, stunden, style, abrechenbar }: IProps) {
  return (
    <Paper sx={{ p: 1, mb: .2 }} style={style}>
      <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
        <Typography variant="body1">{text}</Typography>
        <Stack direction={"row"}>
          {abrechenbar ?
            <EuroIcon sx={{ mt: .5, mr: .5, fontSize: "1rem" }} /> : null
          }
          <Typography variant="body1">{formatStunden(stunden)}</Typography>
        </Stack>
      </Stack>
    </Paper>
  );
}

export default EintragItem;
