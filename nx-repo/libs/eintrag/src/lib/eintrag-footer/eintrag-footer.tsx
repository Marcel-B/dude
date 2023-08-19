import { Paper, Stack, Typography } from "@mui/material";
import { formatStunden } from "@dude/util";

interface IProps {
  stunden: number;
  style?: React.CSSProperties;
}

export function EintragFooter({ style, stunden }: IProps) {
  return (
    <Paper sx={{ p: 1, mb: .2, background: "#6ab04c" }} style={style}>
      <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
        <Typography variant="body1">Gesamt</Typography>
        <Typography variant="body1">{formatStunden(stunden)}</Typography>
      </Stack>
    </Paper>
  );
}

export default EintragFooter;
