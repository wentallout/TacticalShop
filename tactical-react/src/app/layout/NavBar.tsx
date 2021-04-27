import { Container, Menu, Button } from "semantic-ui-react";

import { NavLink } from "react-router-dom";

export default function NavBar()
{


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

					<Menu.Item>
						<Button
							as={NavLink}
							to="/createProduct"
							exact
							positive
							content="Create Product"></Button>
					</Menu.Item>
				</Container>
			</Menu>
		</>
	);
}
