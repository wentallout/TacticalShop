import * as React from "react";
import { Route, Switch } from "react-router-dom";

import { Callback } from "../features/auth/callback";
import { LogoutCallback } from "../features/auth/logoutCallback";
import { Logout } from "../features/auth/logout";
import { SilentRenew } from "../features/auth/silentRenew";

export const Routes = (
	<Switch>
		{/* <Route exact={true} path="/signin-oidc" component={Callback} /> */}
		<Route exact={true} path="/logout" component={Logout} />
		<Route exact={true} path="/logout/callback" component={LogoutCallback} />

		<Route
			exact={true}
			path="/authentication/login-callback"
			component={Callback}
		/>

		<Route exact={true} path="/silentrenew" component={SilentRenew} />
	</Switch>
);
