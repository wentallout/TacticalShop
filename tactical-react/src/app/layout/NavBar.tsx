import { Container, Menu } from "semantic-ui-react";

import { NavLink } from "react-router-dom";

export default function NavBar() {
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
					<Menu.Item as={NavLink} to="/categories" exact name="Categories" />
<<<<<<< HEAD
=======
					<Menu.Item as={NavLink} to="/users" exact name="Users" />
>>>>>>> eea345c8ebc613772181eaab7d4d7aaa55fa8d55
				</Container>
			</Menu>
		</>
	);
}
