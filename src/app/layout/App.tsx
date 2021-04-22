import React, { useEffect } from "react";
import "./styles.css";
import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import ProductDashboard from "../../features/products/dashboard/ProductDashboard";
import LoadingComponent from "./LoadingComponent";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";

function App() {
	const { productStore } = useStore();

	useEffect(() => {
		productStore.loadProducts();
	}, [productStore]);

	if (productStore.loadingInitial)
		return <LoadingComponent content="Loading app" />;

	return (
		<div>
			<NavBar />
			<Container style={{ marginTop: "7em" }}>
				<ProductDashboard />
			</Container>
		</div>
	);
}

export default observer(App);
