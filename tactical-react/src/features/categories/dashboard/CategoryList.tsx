import { observer } from "mobx-react-lite";
import React, { SyntheticEvent, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Item, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function CategoryList() {
	const { categoryStore } = useStore();
	const { deleteCategory, loading, categoriesByDate } = categoryStore;
	const [target, setTarget] = useState("");

	function handleCategoryDelete(
		e: SyntheticEvent<HTMLButtonElement>,
		Categoryid: string,
	) {
		setTarget(e.currentTarget.name);
		deleteCategory(Categoryid);
	}

	return (
		<Segment>
			<Item.Group divided>
				{categoriesByDate.map((category) => (
					<Item key={category.categoryId}>
						<Item.Content>
							<Item.Header>{category.categoryName}</Item.Header>
							{/* <Item.Meta>Last Updated: {category.updatedDate}</Item.Meta>
							<Item.Meta>Created Date: {category.createdDate}</Item.Meta> */}
							<Item.Description>
								<div>CategoryName: {category.categoryName}</div>
								<div>CategoryDescription: {category.categoryDescription}</div>
							</Item.Description>
						</Item.Content>

						<Item.Extra>
							<Button
								as={Link}
								to={`/categories/${category.categoryId}`}
								floated="right"
								content="View"
								color="blue"
							/>
							<Button
								name={category.categoryId}
								loading={loading && target === category.categoryId}
								onClick={(e) => handleCategoryDelete(e, category.categoryId)}
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
