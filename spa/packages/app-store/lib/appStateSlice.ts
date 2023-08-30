import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface AppState {
  toClipboard: string;
}

const initialState: AppState = {
  toClipboard: ""
};

export const appStateSlice = createSlice({
  name: "app-state",
  initialState,
  reducers: {
    setToClipboard: (state, action: PayloadAction<string>) => {
      state.toClipboard = action.payload;
    },
    resetToClipboard: (state) => {
      state.toClipboard = "";
    }
  }
});


export const { setToClipboard, resetToClipboard } = appStateSlice.actions;
