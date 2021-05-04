import { observer } from "mobx-react-lite";
import { ChangeEvent, useEffect, useState } from "react";

import { Button, Segment, Form } from "semantic-ui-react";

import { useStore } from "../../../app/stores/store";
import { useHistory, useParams } from "react-router";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Link } from "react-router-dom";

export default observer(function CategoryForm() {
	const history = useHistory();
	const { categoryStore } = useStore();

	const {
		createCategory,
		updateCategory,
		loadCategory,
		loading,
		loadingInitial,
	} = categoryStore;
	const { categoryid } = useParams<{ categoryid: string }>();
	const [category, setCategory] = useState({
		categoryId: "",
		categoryName: "",
		categoryDescription: "",
	});

	useEffect(() => {
		if (categoryid)
			loadCategory(categoryid).then((category) => setCategory(category!));
	}, [categoryid, loadCategory]);

	function handleSubmit() {
		if (category.categoryId.length === 0) {
			createCategory(category).then(() =>
				history.push(`/categories/${category.categoryId}`),
			);
		} else {
			updateCategory(category).then(() =>
				history.push(`/categories/${category.categoryId}`),
			);
		}
	}

	function handleInputChange(
		event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
	) {
		const { name, value } = event.target;
		setCategory({ ...category, [name]: value });
	}

	if (loadingInitial) return <LoadingComponent content="Loading category..." />;
	return (
		<Segment clearing>
			<Form onSubmit={handleSubmit} autoComplete="off">
				<Form.Input
					placeholder="categoryName"
					value={category.categoryName}
					name="categoryName"
					onChange={handleInputChange}></Form.Input>

				<Form.TextArea
					placeholder="Description"
					value={category.categoryDescription}
					name="categoryDescription"
					onChange={handleInputChange}></Form.TextArea>

				<Button
					loading={loading}
					floated="right"
					positive
					type="submit"
					content="SUBMIT"
				/>
				<Button
					as={Link}
					to="/categories"
					floated="right"
					type="button"
					content="CANCEL"
				/>
			</Form>
		</Segment>
	);
});
