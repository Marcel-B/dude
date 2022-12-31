import styles from "./list.module.scss";
import { fetchProjekte, projekteSelectors, store, useAppDispatch, useAppSelector } from "@dude/store";
import { useEffect } from "react";
import { DataGrid, GridColumns } from "@mui/x-data-grid";

export const List = () => {
  const projekte = useAppSelector(projekteSelectors.selectAll);
  const dispatch = useAppDispatch();

  useEffect(() => {
    console.log("Now Dispatch fetchProjekte");
    dispatch(fetchProjekte());
  }, []);

  const cols: GridColumns = [
    { field: "id", headerName: "Projekt ID" },
    { field: "name", headerName: "Projekt" }
  ];

  return (
    <DataGrid
      rows={projekte}
      initialState={{
        columns: {
          columnVisibilityModel: {
            id: true
          }
        }
      }}
      autoHeight
      columns={cols}
      pageSize={10}
      rowsPerPageOptions={[10, 50, 110]}
      disableSelectionOnClick
      experimentalFeatures={{ newEditingApi: true }}
    />
  );
};

export default List;
