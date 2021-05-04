import { makeAutoObservable, runInAction } from "mobx";

import agent from "../api/agent";
import { Product } from "../models/product";

export default class ProductStore {
	productRegistry = new Map<string, Product>();

	selectedProduct: Product | undefined = undefined;

	editMode = false;
	loading = false;
	loadingInitial = true;

	constructor() {
		makeAutoObservable(this);
	}

	get productsByDate() {
		return Array.from(this.productRegistry.values()).sort(
			(a, b) => Date.parse(a.updatedDate) - Date.parse(b.updatedDate),
		);
	}

	loadProducts = async () => {
		this.loadingInitial = true;
		try {
			const products = await agent.Products.list();
			products.forEach((product) => {
				this.setProduct(product);
			});
			this.setLoadingInitial(false);
		} catch (error) {
			console.log(error);
			this.setLoadingInitial(false);
		}
	};

	loadProduct = async (productid: string) => {
		let product = this.getProduct(productid);
		if (product) {
			this.selectedProduct = product;
			return product;
		} else {
			this.loadingInitial = true;
			try {
				product = await agent.Products.details(productid);
				this.setProduct(product);
				runInAction(() => {
					this.selectedProduct = product;
				});

				this.setLoadingInitial(false);
				return product;
			} catch (error) {
				console.log(error);
				this.setLoadingInitial(false);
			}
		}
	};

	private getProduct = (productid: string) => {
		return this.productRegistry.get(productid);
	};

	private setProduct = (product: Product) => {
		product.updatedDate = product.updatedDate.split("T")[0];
		this.productRegistry.set(product.productId, product);
	};

	setLoadingInitial = (state: boolean) => {
		this.loadingInitial = state;
	};

	createProduct = async (product: Product) => {
		this.loading = true;

		try {
			await agent.Products.create(product);
			runInAction(() => {
				this.productRegistry.set(product.productId, product);
				this.selectedProduct = product;
				this.editMode = false;
				this.loading = false;
			});
		} catch (error) {
			console.log(error);
			runInAction(() => {
				this.loading = false;
			});
		}
	};

	updateProduct = async (product: Product) => {
		this.loading = true;

		try {
			await agent.Products.update(product);
			runInAction(() => {
				this.productRegistry.set(product.productId, product);
				this.selectedProduct = product;
				this.editMode = false;
				this.loading = false;
			});
		} catch (error) {
			console.log(error);
			runInAction(() => {
				this.loading = false;
			});
		}
	};

	deleteProduct = async (productid: string) => {
		this.loading = true;
		try {
			await agent.Products.delete(productid);
			runInAction(() => {
				this.productRegistry.delete(productid);

				this.loading = false;
			});
		} catch (error) {
			console.log(error);
			runInAction(() => {
				this.loading = false;
			});
		}
	};
}
