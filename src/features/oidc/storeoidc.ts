import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import authReducer from "./auth-slice";

export const storeoidc = configureStore({
	reducer: {
		auth: authReducer,
	},
});

export type RootState = ReturnType<typeof storeoidc.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
	ReturnType,
	RootState,
	unknown,
	Action<string>
>;
