/* /src/utils/authConst.js */

export const IDENTITY_CONFIG = {
	authority: "https://localhost:44341",
	client_id: "react",
	redirect_uri: "http://localhost:3000/authentication/login-callback",
	post_logout_redirect_uri:
		"http://localhost:3000/authentication/logout-callback",
	login: "http://localhost:44341/login",
	loadUserInfo: true,
	responseType: "code", //(string, default: 'id_token'): The type of response desired from the OIDC provider.
	grantType: "code",
	webAuthResponseType: "code",
	scope: "openid tacticalshop.api", //(string, default: 'openid'): The scope being requested from the OIDC provider.
	automaticSilentRenew: true,
	includeIdTokenInSilentRenew: true,
};

export const METADATA_OIDC = {
	issuer: "https://identityserver",
	jwks_uri: "https://localhost:44341/.well-known/openid-configuration/jwks",
	authorization_endpoint: "https://localhost:44341/connect/authorize",
	token_endpoint: "https://localhost:44341/connect/token",
	userinfo_endpoint: "https://localhost:44341/connect/userinfo",
	end_session_endpoint: "https://localhost:44341/connect/endsession",
	check_session_iframe: "https://localhost:44341/connect/checksession",
	revocation_endpoint: "https://localhost:44341/connect/revocation",
	introspection_endpoint: "https://localhost:44341/connect/introspect",
};
