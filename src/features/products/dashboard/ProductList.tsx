import { observer } from "mobx-react-lite";
import React, { SyntheticEvent, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Item, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function ProductList() {
	const { productStore } = useStore();
	const { deleteProduct, productsByDate, loading } = productStore;
	const [target, setTarget] = useState("");

	function handleProductDelete(
		e: SyntheticEvent<HTMLButtonElement>,
		productid: string,
	) {
		setTarget(e.currentTarget.name);
		deleteProduct(productid);
	}

	return (
		<Segment>
			<Item.Group divided>
				{productsByDate.map((product) => (
					<Item key={product.productId}>
						<Item.Content>
							<Item.Header>
								{product.productName} ({product.productQuantity} left)
							</Item.Header>
							<Item.Meta>Last Updated: {product.updatedDate}</Item.Meta>
							<Item.Meta>Created Date: {product.createdDate}</Item.Meta>
							<Item.Description>
								<div>CATEGORY: {product.categoryName}</div>
								<div>BRAND: {product.brandName}</div>
								<div>PRICE: ${product.productPrice}</div>
							</Item.Description>
						</Item.Content>

						<Item.Extra>
							<Button
								as={Link}
								to={`/products/${product.productId}`}
								floated="right"
								content="View"
								color="blue"
							/>
							<Button
								name={product.productId}
								loading={loading && target === product.productId}
								onClick={(e) => handleProductDelete(e, product.productId)}
								floated="right"
								content="Delete"
								color="red"
							/>
						</Item.Extra>
					</Item>
				))}
			</Item.Group>
		</Segment>
	);
});
