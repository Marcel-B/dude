import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { apiClient } from "client";

export interface AbrechnungState {
  kalenderwoche: number;
  monat: number;
  jahr: number;
  projekte: string[];
}

const initialState: AbrechnungState = {
  kalenderwoche: 0,
  monat: 0,
  jahr: 0,
  projekte: []
};

export const fetchAbrechnungKalenderwoche = createAsyncThunk("abrechnung/fetchAbrechnungKalenderwoche", async (payload: { kalenderwoche: number, jahr: number, text: string }) => {
  const result = await apiClient.abrechnung.getByKalenderwoche(payload.kalenderwoche, payload.jahr, payload.text);
  return result;
});
export const fetchAbrechnungMonat = createAsyncThunk("abrechnung/fetchAbrechnungMonat", async (payload: { monat: number, jahr: number, text: string }) => {
  const result = await apiClient.abrechnung.getByMonat(payload.monat, payload.jahr, payload.text);
  return result;
});

export const fetchAbrechnungJahr = createAsyncThunk("abrechnung/fetchAbrechnungJahr", async (payload: { jahr: number, text: string }) => {
  const result = await apiClient.abrechnung.getByJahr(payload.jahr, payload.text);
  return result;
});

export const fetchProjektnamen = createAsyncThunk("abrechung/fetchProjekte", async () => {
  const response = await apiClient.abrechnung.getProjekte();
  return response;
});

export const abrechnungSlice = createSlice({
  name: "abrechnung",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder.addCase(fetchAbrechnungKalenderwoche.fulfilled, (state, action: PayloadAction<number>) => {
      state.kalenderwoche = action.payload;
    });
    builder.addCase(fetchAbrechnungMonat.fulfilled, (state, action: PayloadAction<number>) => {
      state.monat = action.payload;
    });
    builder.addCase(fetchAbrechnungJahr.fulfilled, (state, action: PayloadAction<number>) => {
      state.jahr = action.payload;
    });
    builder.addCase(fetchProjektnamen.fulfilled, (state, action: PayloadAction<string[]>) => {
      state.projekte = action.payload;
    });
  }
});
