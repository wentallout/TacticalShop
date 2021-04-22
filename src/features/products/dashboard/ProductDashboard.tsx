import React from "react";
import { Grid } from "semantic-ui-react";
import ProductList from "./ProductList";
import ProductDetails from "../details/ProductDetails";
import ProductForm from "../form/ProductForm";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

export default observer(function ProductDashboard() {
	const { productStore } = useStore();
	const { selectedProduct, editMode } = productStore;

	return (
		<Grid>
			<Grid.Column width="10">
				<ProductList />
			</Grid.Column>
			<Grid.Column width="6">
				{selectedProduct && !editMode && <ProductDetails />}
				{editMode && <ProductForm />}
			</Grid.Column>
		</Grid>
	);
});
