import React, { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import ProductList from "./ProductList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";

export default observer(function ProductDashboard() {
	const { productStore } = useStore();
	const { loadProducts, productRegistry } = productStore;

	useEffect(() => {
		if (productRegistry.size <= 1) loadProducts();
	}, [productRegistry.size, loadProducts]);

	if (productStore.loadingInitial)
		return <LoadingComponent content="Loading app" />;

	return (
		<Grid>
			<Grid.Column width="10">
				<ProductList />
			</Grid.Column>
			<Grid.Column width="6">
				<h2>Product Filters (just a placeholder)</h2>
			</Grid.Column>
		</Grid>
	);
});
