import React, { useEffect } from "react";
import { Grid, Button } from "semantic-ui-react";
import ProductList from "./ProductList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { NavLink } from "react-router-dom";

export default observer(function ProductDashboard() {
	const { productStore } = useStore();
	const { loadProducts, productRegistry } = productStore;

	useEffect(() => {
		if (productRegistry.size <= 1) loadProducts();
	}, [productRegistry.size, loadProducts]);

	if (productStore.loadingInitial)
		return <LoadingComponent content="Loading app" />;

	return (
		<>
			<Button
				as={NavLink}
				to="/createProduct"
				exact
				positive
				content="Create Product"></Button>
			<Button
				as={NavLink}
				to="/addphoto"
				exact
				positive
				content="Add product photos"></Button>

			<Grid>
				<Grid.Column width="10">
					<ProductList />
				</Grid.Column>
				<Grid.Column width="6">
					<h2>Product Filters (just a placeholder)</h2>
				</Grid.Column>
			</Grid>
		</>
	);
});
