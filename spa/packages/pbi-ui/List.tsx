import React, { useEffect } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import {
  deletePbi,
  fetchPbis,
  fetchProjekte,
  pbiSelectors,
  projekteSelectors,
  setCopyDialogType,
  setOpenCopyDialog,
  setPbi,
  useAppDispatch,
  useAppSelector
} from "app-store";
import { toBranch } from "werkzeug";
import { PbiForGrid } from "./pbiForGrid";

export interface IProps {
  triggerSnackbar?: (message: string, severity: "success" | "error" | "info") => void;
}

export const List = ({triggerSnackbar}: IProps) => {
  const pbis = useAppSelector(pbiSelectors.selectAll);
  const projekte = useAppSelector(projekteSelectors.selectAll);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchPbis());
    dispatch(fetchProjekte());
  }, []);

  const cols: GridColDef[] = [
    {field: "id", headerName: "ID", width: 10},
    {field: "name", headerName: "P.B.I.", width: 430},
    {field: "beschreibung", headerName: "Beschreibung", editable: true, width: 300},
    {field: "projektName", headerName: "Projekt", width: 240},
    // {
    //   field: "copy",
    //   type: "actions",
    //   width: 120,
    //   getActions: (params: GridRowParams<PbiForGrid>) => [
    //     <GridActionsCellItem
    //       label="Copy"
    //       icon={<ContentCopyIcon color="info"/>}
    //       onClick={() => {
    //         const postfix = params.row.beschreibung ? `(${params.row.beschreibung.trim()})` : "";
    //         const forClipboard = `${params.row.name} ${postfix}`;
    //         dispatch(setPbi(forClipboard));
    //         dispatch(setCopyDialogType("PBI"));
    //         dispatch(setOpenCopyDialog(true));
    //         if (triggerSnackbar) {
    //           triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
    //         }
    //         params.row.beschreibung = "";
    //       }}
    //     />,
    //     <GridActionsCellItem
    //       label="Branch"
    //       icon={<AltRouteIcon color="primary"/>}
    //       onClick={() => {
    //         const forClipboard = toBranch(params.row.name);
    //         dispatch(setPbi(forClipboard));
    //         dispatch(setCopyDialogType("Branch"));
    //         dispatch(setOpenCopyDialog(true));
    //         if (triggerSnackbar) {
    //           triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
    //         }
    //         params.row.beschreibung = "";
    //       }}
    //     />,
    //     <GridActionsCellItem
    //       label="Löschen"
    //       showInMenu
    //       icon={<DeleteIcon color="warning"/>}
    //       onClick={() => {
    //         dispatch(deletePbi(params.row.id.toString()));
    //       }}
    // {
    //   field: "copy",
    //   type: "actions",
    //   width: 120,
    //   getActions: (params: GridRowParams<PbiForGrid>) => [
    //     <GridActionsCellItem
    //       label="Copy"
    //       icon={<ContentCopyIcon color="info"/>}
    //       onClick={() => {
    //         const postfix = params.row.beschreibung ? `(${params.row.beschreibung.trim()})` : "";
    //         const forClipboard = `${params.row.name} ${postfix}`;
    //         dispatch(setPbi(forClipboard));
    //         dispatch(setCopyDialogType("PBI"));
    //         dispatch(setOpenCopyDialog(true));
    //         if (triggerSnackbar) {
    //           triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
    //         }
    //         params.row.beschreibung = "";
    //       }}
    //     />,
    //     <GridActionsCellItem
    //       label="Branch"
    //       icon={<AltRouteIcon color="primary"/>}
    //       onClick={() => {
    //         const forClipboard = toBranch(params.row.name);
    //         dispatch(setPbi(forClipboard));
    //         dispatch(setCopyDialogType("Branch"));
    //         dispatch(setOpenCopyDialog(true));
    //         if (triggerSnackbar) {
    //           triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
    //         }
    //         params.row.beschreibung = "";
    //       }}
    //     />,
    //     <GridActionsCellItem
    //       label="Löschen"
    //       showInMenu
    //       icon={<DeleteIcon color="warning"/>}
    //       onClick={() => {
    //         dispatch(deletePbi(params.row.id.toString()));
    //       }}
    //     />
    //   ]
    // }
  ];

  return (
    <DataGrid
      rows={pbis.map<PbiForGrid>(pbi => ({
        ...pbi,
        beschreibung: "",
        projektName: projekte.find(p => p.id === pbi.projektId)?.name ?? ""
      }))}
      initialState={{
        columns: {
          columnVisibilityModel: {
            id: false
          }
        }
      }}
      autoHeight
      columns={cols}
      pageSizeOptions={[10]}
      disableRowSelectionOnClick
    />
  );
};

export default List;
