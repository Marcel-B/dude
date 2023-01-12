import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Eintrag } from "@dude/stunden-domain";
import { apiClient } from "@dude/api-client";
import { RootState } from "./store";
import { getTodayAsISO } from "@dude/util";


const eintragAdapter = createEntityAdapter<Eintrag>({
  selectId: (eintrag) => eintrag.id,
  sortComparer: (a, b) => a.datum.localeCompare(b.datum)
});

export const fetchEintraege = createAsyncThunk("eintrag/fetchEintraege", async () => {
  const response = await apiClient.eintraege.getEintraege();
  return response;
});

export const addEintrag = createAsyncThunk("eintrag/addEintrag", async (eintrag: Eintrag) => {
  const response = await apiClient.eintraege.addEintrag(eintrag);
  return response;
});

export const eintragSlice = createSlice({
  name: "eintrag",
  initialState: eintragAdapter.getInitialState({
    datum: getTodayAsISO(),
    loading: false,
    openCreate: false,
    selectedDatum: ""
  }),
  reducers: {
    setDatum: (state, action: PayloadAction<string>) => {
      state.datum = action.payload;
    },
    setOpenCreate: (state, action: PayloadAction<{ openCreate: boolean, selectedDatum: string }>) => {
      state.openCreate = action.payload.openCreate;
      state.selectedDatum = action.payload.selectedDatum;
    },
    reset: (state) => {
      state.datum = getTodayAsISO();
      state.openCreate = false;
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchEintraege.fulfilled, (state, action: PayloadAction<Eintrag[]>) => {
      eintragAdapter.setAll(state, action.payload);
    });
    builder.addCase(addEintrag.fulfilled, (state, action: PayloadAction<Eintrag>) => {
      eintragAdapter.addOne(state, action.payload);
    });
  }
});

export const eintragSelectors = eintragAdapter.getSelectors(
  (state: RootState) => state.eintrag
);

export const { setDatum, reset, setOpenCreate } = eintragSlice.actions;
