import { configureStore } from "@reduxjs/toolkit";
import { datumSlice } from "./datumSlice";
import { eintragSlice } from "./eintragSlice";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { projektSlice } from "./projektSlice";
import { pbiSlice } from "./pbiSlice";

export const store = configureStore({
  reducer: {
    datum: datumSlice.reducer,
    eintrag: eintragSlice.reducer,
    projekt: projektSlice.reducer,
    pbi: pbiSlice.reducer
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
