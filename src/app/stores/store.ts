import { createContext, useContext } from "react";
import ProductStore from "./productStore";
import CategoryStore from "./categoryStore";
import PhotoStore from "./photoStore";
import UserStore from "./userStore";

interface Store {
	productStore: ProductStore;
	categoryStore: CategoryStore;
	photoStore: PhotoStore;
	userStore: UserStore;
}

export const store: Store = {
	productStore: new ProductStore(),
	categoryStore: new CategoryStore(),
	photoStore: new PhotoStore(),
	userStore: new UserStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
	return useContext(StoreContext);
}
