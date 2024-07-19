/** @format */

import axios from 'axios';
const url_api = 'https://localhost:7120/api/User/';

export const getAllUsers = async () => {
	try {
		const response = await axios.get(url_api + 'GetAllUsers');
		return response.data;
	} catch (error) {
		throw error;
	}
};


