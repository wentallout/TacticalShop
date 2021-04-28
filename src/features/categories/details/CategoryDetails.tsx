import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Card, Button } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";

import { useStore } from "../../../app/stores/store";

export default observer(function CategoryDetails() {
	const { categoryStore } = useStore();
	const {
		selectedCategory: category,
		loadCategory,
		loadingInitial,
	} = categoryStore;
	const { categoryid } = useParams<{ categoryid: string }>();

	useEffect(() => {
		if (categoryid) loadCategory(categoryid);
	}, [categoryid, loadCategory]);

	if (loadingInitial || !category) return <LoadingComponent />;

	return (
		<Card fluid>
			<Card.Content>
				<Card.Header>{category.categoryName}</Card.Header>

				<Card.Description>{category.categoryDescription}</Card.Description>
			</Card.Content>
			<Card.Content extra>
				<Button.Group widths="2">
					<Button
						as={Link}
						to={`/manage/category/${category.categoryId}`}
						basic
						color="blue"
						content="EDIT"
					/>

					<Button
						as={Link}
						to="/categories"
						basic
						color="grey"
						content="CANCEL"
					/>
				</Button.Group>
			</Card.Content>
		</Card>
	);
});
