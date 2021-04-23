import { Container, Menu } from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";

export default function HomePage() {
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
			</Container>
		</>
	);
}
