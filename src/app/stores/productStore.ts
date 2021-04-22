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
		try {
			const products = await agent.Products.list();
			products.forEach((product) => {
				product.updatedDate = product.updatedDate.split("T")[0];
				this.productRegistry.set(product.productId, product);
			});
			this.setLoadingInitial(false);
		} catch (error) {
			console.log(error);
			this.setLoadingInitial(false);
		}
	};

	setLoadingInitial = (state: boolean) => {
		this.loadingInitial = state;
	};

	selectProduct = (productid: string) => {
		this.selectedProduct = this.productRegistry.get(productid);
	};

	cancelSelectedProduct = () => {
		this.selectedProduct = undefined;
	};

	openForm = (productid?: string) => {
		productid ? this.selectProduct(productid) : this.cancelSelectedProduct();
		this.editMode = true;
	};

	closeForm = () => {
		this.editMode = false;
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
				if (this.selectedProduct?.productId === productid)
					this.cancelSelectedProduct();
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
