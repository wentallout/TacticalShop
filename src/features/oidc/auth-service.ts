import {
	User,
	UserManager,
	UserManagerSettings,
	WebStorageStateStore,
} from "oidc-client";

import * as Config from "../../config.js";

const oidcSettings: UserManagerSettings = {
	authority: `${Config.API_URL}`,
	client_id: "react",
	redirect_uri: `${Config.REACT_URL}/authentication/login-callback`,
	post_logout_redirect_uri: `${Config.REACT_URL}/authentication/logout-callback`,
	response_type: "code",
	scope: "tacticalshop.api openid profile",
	automaticSilentRenew: true,
	includeIdTokenInSilentRenew: true,
	userStore: new WebStorageStateStore({ store: window.sessionStorage }),
};

class AuthService {
	public userManager: UserManager;

	constructor() {
		this.userManager = new UserManager(oidcSettings);
	}

	public getUserAsync(): Promise<User | null> {
		return this.userManager.getUser();
	}

	public loginAsync(): Promise<void> {
		return this.userManager.signinRedirect();
	}

	public completeLoginAsync(url: string): Promise<User> {
		return this.userManager.signinCallback(url);
	}

	public renewTokenAsync(): Promise<User> {
		return this.userManager.signinSilent();
	}

	public logoutAsync(): Promise<void> {
		return this.userManager.signoutRedirect();
	}

	public async completeLogoutAsync(url: string): Promise<void> {
		await this.userManager.signoutCallback(url);
	}
}

export default new AuthService();
