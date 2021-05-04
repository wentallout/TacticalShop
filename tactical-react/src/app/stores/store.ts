import { createContext, useContext } from "react";
import ProductStore from "./productStore";
import CategoryStore from "./categoryStore";
<<<<<<< HEAD
import PhotoStore from './photoStore';
=======
import PhotoStore from "./photoStore";
import UserStore from "./userStore";
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55

interface Store {
	productStore: ProductStore;
	categoryStore: CategoryStore;
	photoStore: PhotoStore;
<<<<<<< HEAD
=======
	userStore: UserStore;
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
}

export const store: Store = {
	productStore: new ProductStore(),
	categoryStore: new CategoryStore(),
	photoStore: new PhotoStore(),
<<<<<<< HEAD
=======
	userStore: new UserStore(),
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
};

export const StoreContext = createContext(store);

export function useStore() {
	return useContext(StoreContext);
}
