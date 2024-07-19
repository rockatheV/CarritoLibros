/** @format */

import { useEffect, useState } from 'react';
import { getAllUsers } from '../api/UsersApi';

const useUsers = () => {
	const [users, setUsers] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		fetchUsers();
	}, []);

	const fetchUsers = async () => {
		try {
			const data = await getAllUsers();
			console.log('users', data);
			setUsers(data.data);
		} catch (err) {
			setError(err);
		} finally {
			setLoading(false);
		}
	};

	return { users, loading, error };
};

export default useUsers;
