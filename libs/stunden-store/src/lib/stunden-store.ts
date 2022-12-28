import { configureStore } from "@reduxjs/toolkit";
import { datumSlice } from "./datumSlice";

export const stundenStore = configureStore({
  reducer: {
    datum: datumSlice.reducer
  }
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof stundenStore.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof stundenStore.dispatch
