import { Card, Divider, Stack, Typography } from "@mui/material";
import QueryBuilderIcon from "@mui/icons-material/QueryBuilder";
import EuroIcon from "@mui/icons-material/Euro";

export function Zusammenfassung() {
  return (
    <Card sx={{ p: 2, width: 222, height: 170 }}>
      <Typography variant="h2">Abrechnung</Typography>
      <Divider />
      <Stack direction={"row"} sx={{ mt: 1, justifyContent: "space-between" }}>
        <Typography variant="body1">Woche <QueryBuilderIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">77h</Typography>
      </Stack>
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Typography variant="body1">Woche <EuroIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">1.551$</Typography>
      </Stack>
      <Divider />
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Typography variant="body1">Monat <QueryBuilderIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">222</Typography>
      </Stack>
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Typography variant="body1">Monat <EuroIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">1.551$</Typography>
      </Stack>
      <Divider />
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Typography variant="body1">Jahr <QueryBuilderIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">222</Typography>
      </Stack>
      <Stack direction={"row"} sx={{ justifyContent: "space-between" }}>
        <Typography variant="body1">Jahr <EuroIcon style={{ fontSize: "1rem" }} /></Typography>
        <Typography variant="body1">1.551$</Typography>
      </Stack>
    </Card>
  );
}

export default Zusammenfassung;
