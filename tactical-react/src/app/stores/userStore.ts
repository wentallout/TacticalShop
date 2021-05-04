import { makeAutoObservable, runInAction } from "mobx";

import agent from "../api/agent";
import { User } from "../models/user";

export default class UserStore {
	userRegistry = new Map<string, User>();

	selectedUser: User | undefined = undefined;

	editMode = false;
	loading = false;
	loadingInitial = true;

	constructor() {
		makeAutoObservable(this);
	}

	get usersByDate() {
		return Array.from(this.userRegistry.values());
	}

	loadUsers = async () => {
		this.loadingInitial = true;
		try {
			const users = await agent.Users.list();
			users.forEach((user) => {
				this.setUser(user);
			});
			this.setLoadingInitial(false);
		} catch (error) {
			console.log(error);
			this.setLoadingInitial(false);
		}
	};

	loadUser = async (id: string) => {
		let user = this.getUser(id);
		if (user) {
			this.selectedUser = user;
			return user;
		} else {
			this.loadingInitial = true;
			try {
				user = await agent.Users.details(id);
				this.setUser(user);
				runInAction(() => {
					this.selectedUser = user;
				});

				this.setLoadingInitial(false);
				return user;
			} catch (error) {
				console.log(error);
				this.setLoadingInitial(false);
			}
		}
	};

	private getUser = (id: string) => {
		return this.userRegistry.get(id);
	};

	private setUser = (user: User) => {
		this.userRegistry.set(user.id, user);
	};

	setLoadingInitial = (state: boolean) => {
		this.loadingInitial = state;
	};

	createUser = async (user: User) => {
		this.loading = true;

		try {
			await agent.Users.create(user);
			runInAction(() => {
				this.userRegistry.set(user.id, user);
				this.selectedUser = user;
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

	updateUser = async (user: User) => {
		this.loading = true;

		try {
			await agent.Users.update(user);
			runInAction(() => {
				this.userRegistry.set(user.id, user);
				this.selectedUser = user;
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

	deleteUser = async (id: string) => {
		this.loading = true;
		try {
			await agent.Users.delete(id);
			runInAction(() => {
				this.userRegistry.delete(id);

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
