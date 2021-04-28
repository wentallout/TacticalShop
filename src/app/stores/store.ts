import { createContext, useContext } from "react";
import ProductStore from "./productStore";
import CategoryStore from "./categoryStore";

interface Store {
	productStore: ProductStore;
	categoryStore: CategoryStore;
}

export const store: Store = {
	productStore: new ProductStore(),
	categoryStore: new CategoryStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
	return useContext(StoreContext);
}
