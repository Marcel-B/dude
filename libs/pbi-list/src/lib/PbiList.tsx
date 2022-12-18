import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import DeleteIcon from "@mui/icons-material/Delete";
import React from "react";
import { Pbi } from "@dude/pbi-shared";
import { DataGrid, GridActionsCellItem, GridColumns, GridRowParams } from "@mui/x-data-grid";

export interface PbiListProps {
  pbis: Pbi[];
  deletePbi: (pbi: Pbi) => void;
  triggerSnackbar?: (message: string, severity: "success" | "error" | "info") => void;
}

export const PbiList = ({ pbis, deletePbi, triggerSnackbar }: PbiListProps) => {
  const cols: GridColumns = [
    { field: "id", headerName: "ID", width: 70 },
    { field: "name", headerName: "P.B.I.", width: 480 },
    { field: "description", headerName: "Beschreibung", editable: true, width: 300 },
    { field: "project", headerName: "Projekt", width: 240 },
    {
      field: "copy",
      type: "actions",
      width: 60,
      getActions: (params: GridRowParams<Pbi>) => [
        <GridActionsCellItem label="Copy" icon={<ContentCopyIcon color="info" />} onClick={() => {
          const forClipboard = `${params.row.name} (${params.row.description})`;
          if (triggerSnackbar) {
            triggerSnackbar(`P.B.I. '${forClipboard}' in die Zwischenablage kopiert`, "info");
          }
          navigator.clipboard
            .writeText(forClipboard)
            .then(() => {
              params.row.description = "";
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
