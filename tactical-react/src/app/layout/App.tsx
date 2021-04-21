import React, { useEffect, useState } from "react";
import "./styles.css";
import { Container, Button } from "semantic-ui-react";
import { Product } from "../models/product";
import NavBar from "./NavBar";
import ProductDashboard from "../../features/products/dashboard/ProductDashboard";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { observer } from "mobx-react-lite";

function App() {
	const [products, setProducts] = useState<Product[]>([]);
	const [selectedProduct, setSelectedProduct] = useState<Product | undefined>(
		undefined,
	);
	const [editMode, setEditMode] = useState(false);
	const [loading, setLoading] = useState(true);
	const [submitting, setSubmitting] = useState(false);

	useEffect(() => {
		agent.Products.list().then((response) => {
			setProducts(response);
			setLoading(false);
		});
	}, []);

	function handleFormOpen(productid?: string) {
		productid ? handleSelectProduct(productid) : handleCancelSelectedProduct();
		setEditMode(true);
	}

	function handleFormClose() {
		setEditMode(false);
	}

	function handleSelectProduct(productid: string) {
		setSelectedProduct(products.find((x) => x.productId === productid));
	}

	function handleCancelSelectedProduct() {
		setSelectedProduct(undefined);
	}

	function handleCreateOrEditProduct(product: Product) {
		setSubmitting(true);
		if (product.productId) {
			agent.Products.update(product).then(() => {
				setProducts([
					...products.filter((x) => x.productId !== product.productId),
					product,
				]);
				setSelectedProduct(product);
				setEditMode(false);
				setSubmitting(false);
			});
		} else {
			agent.Products.create(product).then(() => {
				setProducts([...products, product]);
				setSelectedProduct(product);
				setEditMode(false);
				setSubmitting(false);
			});
		}
	}
	function handleDeleteProduct(productid: string) {
		setSubmitting(true);
		agent.Products.delete(productid).then(() => {
			setProducts([...products.filter((x) => x.productId !== productid)]);
			setSubmitting(false);
		});
	}

	if (loading) return <LoadingComponent content="Loading app" />;

	return (
		<div>
			<NavBar openForm={handleFormOpen} />
			<Container style={{ marginTop: "7em" }}>
				<Button content="Do something bla bla" positive />
				<ProductDashboard
					products={products}
					selectedProduct={selectedProduct}
					selectProduct={handleSelectProduct}
					cancelSelectProduct={handleCancelSelectedProduct}
					editMode={editMode}
					openForm={handleFormOpen}
					closeForm={handleFormClose}
					createOrEdit={handleCreateOrEditProduct}
					deleteProduct={handleDeleteProduct}
					submitting={submitting}
				/>
			</Container>
		</div>
	);
}

export default observer(App);
