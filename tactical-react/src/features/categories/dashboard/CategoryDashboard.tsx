import React, { useEffect } from "react";
import { Grid, Button } from "semantic-ui-react";
import CategoryList from "./CategoryList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { NavLink } from "react-router-dom";

export default observer(function CategoryDashboard() {
	const { categoryStore } = useStore();
	const { loadCategories, categoryRegistry } = categoryStore;

	useEffect(() => {
		if (categoryRegistry.size <= 1) loadCategories();
	}, [categoryRegistry.size, loadCategories]);

	if (categoryStore.loadingInitial)
		return <LoadingComponent content="Loading app" />;

	return (
		<>
			<Button
				as={NavLink}
				to="/createCategory"
				exact
				positive
				content="Create Category"></Button>
			<Grid>
				<Grid.Column width="10">
					<CategoryList />
				</Grid.Column>
				<Grid.Column width="6">
					<h2>Category Filters (just a placeholder)</h2>
				</Grid.Column>
			</Grid>
		</>
	);
});
