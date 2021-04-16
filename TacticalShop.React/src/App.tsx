import React, { useEffect, useState } from "react";

import "./App.css";
import axios from "axios";
import { Header, List, ListItem } from "semantic-ui-react";

function App() {
	const [products, setProducts] = useState([]);

	useEffect(() => {
		axios.get("https://localhost:44341/api/products").then((response) => {
			console.log(response);
			setProducts(response.data);
		});
	}, []);

	return (
		<div>
			<Header as="h2" icon="users" content="TacticalShopAdmin" />

			<List>
				{products.map((product: any) => (
					<List.Item key={product.productId}>{product.productName}</List.Item>
				))}
			</List>
		</div>
	);
}

export default App;
