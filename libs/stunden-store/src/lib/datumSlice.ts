import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface DatumState {
  datum: Date;
}

const initialState: DatumState = {
  datum: new Date()
};
export const datumSlice = createSlice({
  name: "datum",
  initialState,
  reducers: {
    setDatum: (state, action: PayloadAction<Date>) => {
      state.datum = action.payload;
    }
  }
});


export const { setDatum } = datumSlice.actions;
export const selectDatum = (state: DatumState) => state.datum;
