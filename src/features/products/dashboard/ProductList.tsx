import React, { SyntheticEvent, useState } from "react";
import { Button, Item, Segment } from "semantic-ui-react";
import { Product } from "../../../app/models/product";

interface Props {
	products: Product[];
	selectProduct: (productid: string) => void;
	deleteProduct: (productid: string) => void;
	submitting: boolean;
}

export default function ProductList({
	products,
	selectProduct,
	deleteProduct,
	submitting,
}: Props) {
	const [target, setTarget] = useState('');

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
				{products.map((product) => (
					<Item key={product.productId}>
						<Item.Content>
							<Item.Header>
								{product.productName} [{product.categoryName}]
							</Item.Header>
							<Item.Meta>Last Updated:{product.updatedDate}</Item.Meta>

							<Item.Description>
								<div>{product.brandName}</div>

								<div>{product.productPrice}</div>
							</Item.Description>
						</Item.Content>

						<Item.Extra>
							<Button
								onClick={() => selectProduct(product.productId)}
								floated="right"
								content="View"
								color="blue"
							/>
							<Button
								name={product.productId}
								loading={submitting && target === product.productId}
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
}
