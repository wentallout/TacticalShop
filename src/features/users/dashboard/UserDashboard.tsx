import React, { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import UserList from "./UserList";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../../../app/layout/LoadingComponent";

export default observer(function UserDashboard() {
	const { userStore } = useStore();
	const { loadUsers, userRegistry } = userStore;

	useEffect(() => {
		if (userRegistry.size <= 1) loadUsers();
	}, [userRegistry.size, loadUsers]);

	if (userStore.loadingInitial)
		return <LoadingComponent content="Loading app" />;

	return (
		<>
			<Grid>
				<Grid.Column width="10">
					<UserList />
				</Grid.Column>
			</Grid>
		</>
	);
});
