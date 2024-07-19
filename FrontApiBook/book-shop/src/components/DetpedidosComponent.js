/** @format */

import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import useDetPedidos from '../hooks/HookDetPedidos';
import { getAllBooks } from '../api/BooksApi';
import { Table, Button, Modal } from 'flowbite-react';
import '../app/globals.css';
import SvgEdit2 from '../IconosSVG/EditSVG';

const DetPedidosList = () => {
	const router = useRouter();
	const { orderId } = router.query;
	const {
		detpedidos,
		detpedido,
		loading,
		error,
		createOrEditOrderDetail,
		UpdateAmountDetail,
		GetDetailsById,
	} = useDetPedidos(orderId);
	const [isModalOpen, setIsModalOpen] = useState(false);
	const [isEditing, setIsEditing] = useState(false);
	const [formData, setFormData] = useState({
		id: 0,
		idOrder: orderId,
		idBook: 0,
		amount: 0.0,
	});
	const [books, setBooks] = useState([]);
	const [selectedBook, setSelectedBook] = useState(''); // Inicializar como cadena vacía

	const fillSelectedBook = async () => {
		try {
			const booksData = await getAllBooks();
			console.log('Librossss', booksData);
			if (booksData.succes) {
				const allBooks = booksData.data;
				setBooks(allBooks);
			} else {
				console.error('Error fetching books:', booksData.Message);
			}
		} catch (error) {
			console.error('Error fetching books:', error);
		}
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		if (isEditing) {
			console.log(formData.id + '' + formData.amount);
			UpdateAmountDetail(formData.id, formData.amount);
		} else {
			console.log('Objeto antes de crear', formData);
			createOrEditOrderDetail(formData);
			setFormData({
				id: 0,
				idOrder: orderId,
				idBook: 0,
				amount: 0.0,
			});
		}
		closeModal(); // Cierra la modal después de crear o editar el libro
	};

	const openModal = () => {
		setIsEditing(false);
		setSelectedBook('');
		fillSelectedBook();
		setFormData({
			id: 0,
			idOrder: orderId,
			idBook: 0,
			amount: 0.0,
		});
		setIsModalOpen(true);
	};

	const getByid = async (id) => {
		try {
			const res = await GetDetailsById(id);
			console.log(res);
			if (res) {
				openEditModal(res);
			} else {
				console.error('Error: No data returned from GetDetailsById');
			}
		} catch (error) {
			console.error('Error fetching books:', error);
		}
	};

	const openEditModal = (fetchedBook) => {
		setIsEditing(true);
		fillSelectedBook();
		setSelectedBook(fetchedBook.idBook); // Ajustar el valor del libro seleccionado

		setFormData({
			id: fetchedBook.id,
			idOrder: orderId,
			idBook: fetchedBook.idBook, // También asegurar que el ID del libro está en el formData
			amount: fetchedBook.amount,
		});
		setIsModalOpen(true);
	};

	const closeModal = () => {
		setIsModalOpen(false);
	};

	const handleChange = (event) => {
		const selectedBookId = event.target.value;
		setSelectedBook(selectedBookId);
		setFormData({
			...formData,
			idBook: selectedBookId,
		});
	};

	const handleInputChange = (e) => {
		const { name, value } = e.target;
		setFormData({
			...formData,
			[name]: value,
		});
	};

	if (loading) return <p>Loading...</p>;
	if (error) return <p>Error loading pedidos.</p>;

	return (
		<div>
			<div className='flex justify-between items-center mb-5'>
				<div>
					<h1 className='text-5xl font-extrabold dark:text-white'>
						<small className='ms-2 font-semibold text-gray-500 dark:text-gray-400'>
							Lineas de Pedido
						</small>
					</h1>
				</div>
				<div>
					<Button color='success' onClick={openModal}>
						Crear Detalle
					</Button>
				</div>
			</div>

			<Table>
				<Table.Head>
					<Table.HeadCell>Titulo</Table.HeadCell>
					<Table.HeadCell>FechaPedido</Table.HeadCell>
					<Table.HeadCell>Autor</Table.HeadCell>
					<Table.HeadCell>Cantidad</Table.HeadCell>
					<Table.HeadCell>Precio Libro</Table.HeadCell>
					<Table.HeadCell>Total</Table.HeadCell>
					<Table.HeadCell>Acciones</Table.HeadCell>
				</Table.Head>
				<Table.Body className='divide-y'>
					{detpedidos.map((detOrder) => (
						<Table.Row
							key={detOrder.id}
							className='bg-white dark:border-gray-700 dark:bg-gray-800'
						>
							<Table.Cell>{detOrder.book}</Table.Cell>
							<Table.Cell className='whitespace-nowrap font-medium text-gray-900 dark:text-white'>
								{detOrder.order}
							</Table.Cell>
							<Table.Cell>{detOrder.bookAuthor}</Table.Cell>
							<Table.Cell>{detOrder.amount}</Table.Cell>
							<Table.Cell>{detOrder.bookPrice}</Table.Cell>
							<Table.Cell>{detOrder.price}</Table.Cell>
							<Table.Cell>
								<a
									onClick={() => getByid(detOrder.id)}
									className='font-medium text-cyan-600 hover:underline dark:text-cyan-500'
								>
									<SvgEdit2 width={20} height={20} stroke='black' />
								</a>
							</Table.Cell>
						</Table.Row>
					))}
				</Table.Body>
			</Table>

			<Modal show={isModalOpen} onClose={closeModal}>
				<Modal.Header>Detalle pedido</Modal.Header>
				<Modal.Body>
					<form onSubmit={handleSubmit} className='space-y-6'>
						<div>
							<label htmlFor='book-picker'>Seleccione un libro:</label>
							<select
								id='book-picker'
								value={selectedBook || ''} // Asegurarse de que el valor no sea null
								onChange={handleChange}
								disabled={isEditing}
							>
								<option value=''>Seleccione un libro</option>
								{books.map((book) => (
									<option key={book.id} value={book.id}>
										{book.title}
									</option>
								))}
							</select>
						</div>

						<div>
							<label className='block text-sm font-medium text-gray-700'>
								Cantidad
							</label>
							<input
								type='number'
								name='amount'
								value={formData.amount}
								onChange={handleInputChange}
								required
								className='mt-1 block w-full border border-gray-300 rounded-md shadow-sm'
							/>
						</div>

						<div className='flex justify-end space-x-2'>
							<Button color='success' type='submit'>
								{isEditing ? 'Editar' : 'Crear'}
							</Button>
							<Button color='gray' onClick={closeModal}>
								Cancelar
							</Button>
						</div>
					</form>
				</Modal.Body>
			</Modal>
		</div>
	);
};

export default DetPedidosList;
