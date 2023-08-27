import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Pbi } from "domain/pbi";
import { apiClient } from "client";
import { RootState } from "./store";
import { PbiDto } from "client/pbi";

const pbiAdapter = createEntityAdapter<Pbi>({
  selectId: (pbi) => pbi.id,
  sortComparer: (a, b) => a.name.localeCompare(b.name)
});

export const fetchPbis = createAsyncThunk("pbi/fetchPbis", async () => {
  const response = await apiClient.pbis.getPbis();
  return response;
});

export const addPbi = createAsyncThunk("pbi/addPbi", async (pbi: PbiDto) => {
  const response = await apiClient.pbis.addPbi(pbi);
  return response;
});

export const deletePbi = createAsyncThunk("pbi/deletePbi", async (id: string) => {
  await apiClient.pbis.deletePbi(id);
  return id;
});

export const pbiSlice = createSlice({
  name: "pbi",
  initialState: pbiAdapter.getInitialState({
    openCopyDialog: false,
    pbi: "",
    copyDialogType: ""
  }),
  reducers: {
    setOpenCopyDialog: (state, action: PayloadAction<boolean>) => {
      state.openCopyDialog = action.payload;
    },
    setPbi: (state, action: PayloadAction<string>) => {
      state.pbi = action.payload;
    },
    setCopyDialogType: (state, action: PayloadAction<string>) => {
      state.copyDialogType = action.payload;
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchPbis.fulfilled, (state, action: PayloadAction<Pbi[]>) => {
      pbiAdapter.setAll(state, action.payload);
    });
    builder.addCase(addPbi.fulfilled, (state, action: PayloadAction<Pbi>) => {
      pbiAdapter.addOne(state, action.payload);
    });
    builder.addCase(deletePbi.fulfilled, (state, action: PayloadAction<string>) => {
      pbiAdapter.removeOne(state, action.payload);
    });
  }
});

// Can create a set of memoized selectors based on the location of this entity state
export const pbiSelectors = pbiAdapter.getSelectors(
  (state: RootState) => state.pbi
);
export const {setOpenCopyDialog, setPbi, setCopyDialogType} = pbiSlice.actions;
