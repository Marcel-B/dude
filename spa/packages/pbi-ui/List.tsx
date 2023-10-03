import React, { useEffect } from "react";
import { DataGrid, GridActionsCellItem, GridColDef, GridRowParams } from "@mui/x-data-grid";
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
import { Delete, CopyAllOutlined } from "@mui/icons-material";

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
      {field: "name", headerName: "P.B.I.", flex: 2},
      {field: "beschreibung", headerName: "Beschreibung", flex: 3, editable: true, width: 300},
      {field: "projektName", headerName: "Projekt", width: 240},
      {
        field: "delete",
        type: "actions",
        width: 120,
        getActions: (params: GridRowParams<PbiForGrid>) => [
          <GridActionsCellItem
            label="Copy"
            icon={<Delete color="warning"/>}
            onClick={() => {
              dispatch(deletePbi(params.row.id));
            }}
          />,
          <GridActionsCellItem
            label="Branch"
            icon={<CopyAllOutlined color="primary"/>}
            onClick={() => {
              const forClipboard = toBranch(params.row.name);
              dispatch(setPbi(forClipboard));
              dispatch(setCopyDialogType("Branch"));
              dispatch(setOpenCopyDialog(true));
              if (triggerSnackbar) {
                triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
              }
              params.row.beschreibung = "";
            }}
          />
        ]
      }
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
              id: true
            }
          }
        }}
        autoHeight
        columns={cols}
        pageSizeOptions={[10]}
        disableRowSelectionOnClick
      />
    );
  }
;

export default List;
