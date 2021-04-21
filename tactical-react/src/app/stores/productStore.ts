import { makeAutoObservable } from "mobx";
import { Product } from "../models/product";

export default class ProductStore {
	products: Product[] = [];
	selectedProduct: Product = null;

	editMode = false;
	loading = false;
	loadingInitial = false;

	constructor() {
		makeAutoObservable(this);
	}

	loadProducts = () => {
		this.loadingInitial = true;
	};
}
