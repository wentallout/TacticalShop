import { Container, Menu } from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";
import { selectIsAuthenticated, selectUser } from "../oidc/auth-slice";
import { useSelector } from "react-redux";
import LoginMenu from "../oidc/LoginMenu";

export default function HomePage() {
	const isAuthenticated = useSelector(selectIsAuthenticated);
	const userName = useSelector(selectUser)?.name;
	return (
		<>
			<Menu inverted fixed="top">
				<Container>
					<Menu.Item as={NavLink} to="/" exact header>
						<img
							style={{ marginRight: "10px" }}
							src="/assets/logo.png"
							alt="logo"
						/>
						TacticalShopAdmin
					</Menu.Item>

					<Menu.Item as={NavLink} to="/products" exact name="Products" />
				</Container>
			</Menu>

			<Container style={{ marginTop: "7em" }}>
				<h1>Welcome to TacticalShop Admin Page!!!</h1>

				<h3>
					Go to <Link to="/products">Products</Link>{" "}
				</h3>

				<h3>
					<LoginMenu isAuthenticated={isAuthenticated} userName={userName} />
				</h3>
			</Container>
		</>
	);
}
