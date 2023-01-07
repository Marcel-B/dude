// import { Container, Divider, Grid, IconButton, Typography } from "@mui/material";
// import React from "react";
// import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
// import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
// import { addWeeks, lastDayOfWeek, parse, startOfDay, subDays } from "date-fns";
// import { eineWoche, Eintrag, Tag, Wochentag } from "@dude/stunden-domain";
// import { TagItem } from "./tag-item";
// import { EintragItem } from "./eintrag-item";
// import { RootState, setDatum, useAppDispatch, useAppSelector } from "@dude/store";
// import format from "date-fns/format";
//
// const getDaDay = (date: string, day: Day) => {
//   const d = parse(date, "dd.MM.yyyy", new Date());
//   const sonntag = lastDayOfWeek(d, { weekStartsOn: 1 });
//   const result = subDays(sonntag, day);
//   return format(result, "dd.MM.yyyy");
// };
//
// interface IProps {
//   titel: string;
// }
//
// export const WocheView = ({ titel }: IProps) => {
//   const dispatch = useAppDispatch();
//   const { datum } = useAppSelector((state: RootState) => state.datum);
//   const { eintraege } = useAppSelector((state: RootState) => state.eintrag);
//
//   const nextWeek = () => {
//     const asDate = parse(datum, "dd.MM.yyyy", startOfDay(new Date()));
//     const newDatum = addWeeks(asDate, 1);
//     dispatch(setDatum(format(newDatum, "dd.MM.yyyy")));
//   };
//
//   const getKalenderWoche = (date: string) => {
//     const d = parse(date, "dd.MM.yyyy", new Date());
//     const kalenderWoche = format(d, "w");
//     return kalenderWoche;
//   };
//
//   const getTitel = (date: string) => {
//     const d = parse(date, "dd.MM.yyyy", new Date());
//     const jahr = format(d, "yyyy");
//     return getKalenderWoche(date) + ". KW " + jahr;
//   };
//
//   const lastWeek = () => {
//     const asDate = parse(datum, "dd.MM.yyyy", startOfDay(new Date()));
//     const newDatum = addWeeks(asDate, -1);
//     dispatch(setDatum(format(newDatum, "dd.MM.yyyy")));
//   };
//   const getEintragForTag = (eintraege: Eintrag[], tag: Tag) => {
//     return eintraege.filter(e => e.datum === getDaDay(datum, tag));
//   };
//
//   return (
//     <Container sx={{ p: 2 }}>
//       <Typography variant="h3">{getTitel(datum)}</Typography>
//       <Divider sx={{ mb: 2 }} />
//       <Grid
//         container
//         alignItems={"flex-start"}
//         justifyContent={"space-between"}
//       >
//
//         <Grid item xs={0}>
//           <IconButton onClick={() => lastWeek()}>
//             <KeyboardArrowLeftIcon />
//           </IconButton>
//         </Grid>
//         {eineWoche.map((wochentag: Wochentag) => {
//           return (
//             <Grid item xs={2} key={wochentag.tag}>
//               <TagItem wochentag={wochentag} style={{ background: "#7ed6df" }} />
//               {eintraege && getEintragForTag(eintraege, wochentag.tag).map(e =>
//                 <EintragItem
//                   key={`${e.text}-${e.datum}-${e.stunden}`}
//                   text={e.text}
//                   style={{ background: "#dff9fb" }}
//                   stunden={e.stunden} />)}
//               <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
//               <EintragItem
//                 text="Gesamt"
//                 style={{ background: "#6ab04c" }}
//                 stunden={getEintragForTag(eintraege, wochentag.tag)
//                   .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></EintragItem>
//             </Grid>
//           );
//         })}
//         <Grid item xs={0}>
//           <IconButton onClick={() => nextWeek()}>
//             <KeyboardArrowRightIcon />
//           </IconButton>
//         </Grid>
//       </Grid>
//     </Container>
//   );
// };
