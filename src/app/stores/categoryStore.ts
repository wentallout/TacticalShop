import { makeAutoObservable, runInAction } from "mobx";

import agent from "../api/agent";
import { Category } from "../models/category";

export default class CategoryStore {
	categoryRegistry = new Map<string, Category>();

	selectedCategory: Category | undefined = undefined;

	editMode = false;
	loading = false;
	loadingInitial = true;

	constructor() {
		makeAutoObservable(this);
	}

	get categoriesByDate() {
		return Array.from(this.categoryRegistry.values())
	}

	loadCategories = async () => {
		this.loadingInitial = true;
		try {
			const categories = await agent.Categories.list();
			categories.forEach((category) => {
				this.setCategory(category);
			});
			this.setLoadingInitial(false);
		} catch (error) {
			console.log(error);
			this.setLoadingInitial(false);
		}
	};

	loadCategory = async (categoryid: string) => {
		let category = this.getCategory(categoryid);
		if (category) {
			this.selectedCategory = category;
			return category;
		} else {
			this.loadingInitial = true;
			try {
				category = await agent.Categories.details(categoryid);
				this.setCategory(category);
				runInAction(() => {
					this.selectedCategory = category;
				});

				this.setLoadingInitial(false);
				return category;
			} catch (error) {
				console.log(error);
				this.setLoadingInitial(false);
			}
		}
	};

	private getCategory = (categoryid: string) => {
		return this.categoryRegistry.get(categoryid);
	};

	private setCategory = (category: Category) => {
		this.categoryRegistry.set(category.categoryId, category);
	};

	setLoadingInitial = (state: boolean) => {
		this.loadingInitial = state;
	};

	createCategory = async (category: Category) => {
		this.loading = true;

		try {
			await agent.Categories.create(category);
			runInAction(() => {
				this.categoryRegistry.set(category.categoryId, category);
				this.selectedCategory = category;
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

	updateCategory = async (category: Category) => {
		this.loading = true;

		try {
			await agent.Categories.update(category);
			runInAction(() => {
				this.categoryRegistry.set(category.categoryId, category);
				this.selectedCategory = category;
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

	deleteCategory = async (categoryid: string) => {
		this.loading = true;
		try {
			await agent.Categories.delete(categoryid);
			runInAction(() => {
				this.categoryRegistry.delete(categoryid);

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
