import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import DeleteIcon from "@mui/icons-material/Delete";
import React from "react";
import { Pbi } from "@dude/stunden-domain";
import { DataGrid, GridActionsCellItem, GridColumns, GridRowParams } from "@mui/x-data-grid";
import AltRouteIcon from "@mui/icons-material/AltRoute";
import { toBranch } from "./utils";

export interface PbiListProps {
  pbis: Pbi[];
  deletePbi: (pbi: Pbi) => void;
  triggerSnackbar?: (message: string, severity: "success" | "error" | "info") => void;
}

export const PbiList = ({ pbis, deletePbi, triggerSnackbar }: PbiListProps) => {
  const cols: GridColumns = [
    { field: "id", headerName: "ID", width: 10 },
    { field: "name", headerName: "P.B.I.", width: 430 },
    { field: "description", headerName: "Beschreibung", editable: true, width: 300 },
    { field: "project", headerName: "Projekt", width: 240 },
    {
      field: "copy",
      type: "actions",
      width: 120,
      getActions: (params: GridRowParams<Pbi>) => [
        <GridActionsCellItem label="Copy" icon={<ContentCopyIcon color="info" />} onClick={() => {
          const forClipboard = `${params.row.name} (${params.row.beschreibung})`;
          if (triggerSnackbar) {
            triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
          }
          navigator.clipboard
            .writeText(forClipboard)
            .then(() => {
              params.row.beschreibung = "";
            });
        }}
        />,
        <GridActionsCellItem label="Branch" icon={<AltRouteIcon color="primary" />} onClick={() => {
          const forClipboard = toBranch(params.row.name);
          if (triggerSnackbar) {
            triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
          }
          navigator.clipboard
            .writeText(forClipboard)
            .then(() => {
              params.row.beschreibung = "";
            });
        }}
        />,
        <GridActionsCellItem label="Löschen" showInMenu icon={<DeleteIcon color="warning" />} onClick={() => {
          fetch(`http://localhost:3333/api/pbi/${params.row.id}`, {
            method: "DELETE"
          }).then(() => {
              deletePbi(params.row);
            }
          );
        }}
        />
      ]
    }];

  return (
    <DataGrid
      rows={pbis}
      initialState={{
        columns: {
          columnVisibilityModel: {
            id: false
          }
        }
      }
      }
      autoHeight
      columns={cols}
      pageSize={10}
      rowsPerPageOptions={[10, 50, 110]}
      disableSelectionOnClick
      experimentalFeatures={{ newEditingApi: true }}
    />
  );
};
