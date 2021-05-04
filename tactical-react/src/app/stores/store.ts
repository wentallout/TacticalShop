import { createContext, useContext } from "react";
import ProductStore from "./productStore";
import CategoryStore from "./categoryStore";
import PhotoStore from './photoStore';

interface Store {
	productStore: ProductStore;
	categoryStore: CategoryStore;
	photoStore: PhotoStore;
}

export const store: Store = {
	productStore: new ProductStore(),
	categoryStore: new CategoryStore(),
	photoStore: new PhotoStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
	return useContext(StoreContext);
}
