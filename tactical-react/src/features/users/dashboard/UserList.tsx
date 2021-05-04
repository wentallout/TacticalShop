import { observer } from "mobx-react-lite";

import { Item, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function UserList() {
	const { userStore } = useStore();
	const { usersByDate } = userStore;

	return (
		<Segment>
			<Item.Group divided>
				{usersByDate.map((user) => (
					<Item key={user.id}>
						<Item.Content>
							<Item.Header>{user.userName}</Item.Header>

							<Item.Description>
								<div>Id: {user.id}</div>
								<div>Email: {user.email}</div>
								<div>PhoneNumber: {user.phoneNumber}</div>
								<div>FullName: {user.fullName}</div>
							</Item.Description>
						</Item.Content>

						{/* <Item.Extra>
							<Button
								as={Link}
								to={`/users/${user.id}`}
								floated="right"
								content="View"
								color="blue"
							/>
							<Button
								name={user.id}
								loading={loading && target === user.id}
								onClick={(e) => handleUserDelete(e, user.id)}
								floated="right"
								content="Delete"
								color="red"
							/>
						</Item.Extra> */}
					</Item>
				))}
			</Item.Group>
		</Segment>
	);
});
