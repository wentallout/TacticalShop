import React, { Fragment } from "react";
import { Container, Menu, Button } from "semantic-ui-react";

interface Props {
	openForm: () => void;
}

export default function NavBar({ openForm }: Props) {
	return (
		<>
			<Menu inverted fixed="top">
				<Container>
					<Menu.Item header>
						<img
							style={{ marginRight: "10px" }}
							src="/assets/logo.png"
							alt="logo"
						/>
						TacticalShopAdmin
					</Menu.Item>

					<Menu.Item name="TacticalShop" />

					<Menu.Item>
						<Button
							onClick={openForm}
							positive
							content="Create Product"></Button>
					</Menu.Item>
				</Container>
			</Menu>
		</>
	);
}
