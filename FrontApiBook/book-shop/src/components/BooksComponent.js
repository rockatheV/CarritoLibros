/** @format */

// src/components/BooksList.js
import React, { useState } from 'react';
import useBooks from '../hooks/HookBooks';
import { Button, Modal, Table } from 'flowbite-react';
import '../app/globals.css';
import SvgEdit2 from './../IconosSVG/EditSVG';
import DeleteSVG from './../IconosSVG/DeleteSVG';

const BooksList = () => {
	const {
		books,
		book,
		loading,
		error,
		createBook,
		editBooks,
		fetchBooksByid,
		DeleteBooks,
	} = useBooks();
	const [isModalOpen, setIsModalOpen] = useState(false);
	const [isEditing, setIsEditing] = useState(false);
	const [formData, setFormData] = useState({
		id: 0,
		title: '',
		author: '',
		price: '',
		publicationDate: '',
	});

	const getByid = async (id) => {
		const fetchedBook = await fetchBooksByid(id);
		if (fetchedBook) {
			openEditModal(fetchedBook);
		}
	};
	const openModal = () => {
		setIsEditing(false); // Estamos creando un libro
		setFormData({
			id: 0,
			title: '',
			author: '',
			price: '',
			publicationDate: '',
		});
		setIsModalOpen(true);
	};

	const openEditModal = (fetchedBook) => {
		setIsEditing(true);
		setFormData({
			id: fetchedBook.id,
			title: fetchedBook.title,
			author: fetchedBook.author,
			publicationDate: fetchedBook.publicationDate.split('T')[0],
			price: fetchedBook.price,
		});
		setIsModalOpen(true);
	};
	const closeModal = () => {
		setIsModalOpen(false);
	};

	const handleInputChange = (e) => {
		const { name, value } = e.target;
		setFormData({
			...formData,
			[name]: value,
		});
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		if (isEditing) {
			editBooks(formData.id, formData); // Llama a la función editBooks con los datos del formulario
		} else {
			createBook(formData); // Llama a la función createBook con los datos del formulario
		}
		closeModal(); // Cierra la modal después de crear o editar el libro
	};

	const handleDeleteBooks = async (id) => {
		const confirmDelete = window.confirm(
			'¿Estás seguro de que quieres eliminar este libro?',
		);
		if (confirmDelete) {
			await DeleteBooks(id);
		}
	};

	if (loading) return <p>Loading...</p>;
	if (error) return <p>Error loading Books.</p>;

	return (
		<div>
			<div className='flex justify-between items-center mb-5'>
				<div>
					<h1 className='text-5xl font-extrabold dark:text-white'>
						<small className='ms-2 font-semibold text-gray-500 dark:text-gray-400'>
							Lista de Libros
						</small>
					</h1>
				</div>
				<div>
					<Button color='success' onClick={openModal}>
						Crear Libro
					</Button>
				</div>
			</div>

			<div>
				<Table>
					<Table.Head>
						<Table.HeadCell>Title</Table.HeadCell>
						<Table.HeadCell>Author</Table.HeadCell>
						<Table.HeadCell>Publication Date</Table.HeadCell>
						<Table.HeadCell>Price</Table.HeadCell>
						<Table.HeadCell>Acciones</Table.HeadCell>
						<Table.HeadCell>
							<span className='sr-only'>Editar</span>
						</Table.HeadCell>
					</Table.Head>
					<Table.Body className='divide-y'>
						{books.map((book) => (
							<Table.Row
								key={book.id}
								className='bg-white dark:border-gray-700 dark:bg-gray-800'
							>
								<Table.Cell className='whitespace-nowrap font-medium text-gray-900 dark:text-white'>
									{book.title}
								</Table.Cell>
								<Table.Cell>{book.author}</Table.Cell>
								<Table.Cell>
									{new Date(book.publicationDate).toLocaleDateString()}
								</Table.Cell>
								<Table.Cell>{`$${book.price.toFixed(2)}`}</Table.Cell>
								<Table.Cell>
									<div className='flex flex-row items-center space-x-4'>
										<a
											onClick={() => getByid(book.id)}
											className='font-medium text-cyan-600 hover:underline dark:text-cyan-500'
										>
											<SvgEdit2 width={20} height={20} stroke='black' />
										</a>

										<a
											onClick={() => handleDeleteBooks(book.id)}
											className='font-medium text-cyan-600 hover:underline dark:text-cyan-500'
										>
											<DeleteSVG width={20} height={20} stroke='black' />
										</a>
									</div>
								</Table.Cell>
							</Table.Row>
						))}
					</Table.Body>
				</Table>
			</div>

			<Modal show={isModalOpen} onClose={closeModal}>
				<Modal.Header>Crear Nuevo Libro</Modal.Header>
				<Modal.Body>
					<form onSubmit={handleSubmit} className='space-y-6'>
						<div>
							<label className='block text-sm font-medium text-gray-700'>
								Título
							</label>
							<input
								type='text'
								name='title'
								value={formData.title}
								onChange={handleInputChange}
								required
								className='mt-1 block w-full border border-gray-300 rounded-md shadow-sm'
							/>
						</div>
						<div>
							<label className='block text-sm font-medium text-gray-700'>
								Autor
							</label>
							<input
								type='text'
								name='author'
								value={formData.author}
								onChange={handleInputChange}
								required
								className='mt-1 block w-full border border-gray-300 rounded-md shadow-sm'
							/>
						</div>
						<div>
							<label className='block text-sm font-medium text-gray-700'>
								Fecha de Publicación
							</label>
							<input
								type='date'
								name='publicationDate'
								value={formData.publicationDate}
								onChange={handleInputChange}
								required
								className='mt-1 block w-full border border-gray-300 rounded-md shadow-sm'
							/>
						</div>
						<div>
							<label className='block text-sm font-medium text-gray-700'>
								Precio
							</label>
							<input
								type='number'
								name='price'
								value={formData.price}
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

export default BooksList;
