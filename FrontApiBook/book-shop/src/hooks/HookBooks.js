// src/hooks/useUsers.js
import { useEffect, useState } from 'react';
import { getAllBooks } from '../api/BooksApi';
import { PostBooks } from '../api/BooksApi';
import { getBooksById } from '../api/BooksApi';
import { EditBooks } from '../api/BooksApi';
import { DeleteBooksA } from '../api/BooksApi';

const useBooks = () => {
  const [books, setBooks] = useState([]);
  const [book, setBook] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getAllBooks();
      console.log("Librooos",data);
      setBooks(data.data);
    } catch (err) {
      setError(err);
    } finally {
      setLoading(false);
    }
  };
  const fetchBooksByid = async (id) => {
    try {
      setLoading(true);
      const data = await getBooksById(id);
      setBook(data.data);
      return data.data; // Devuelve el libro obtenido
    } catch (err) {
      setError(err);
    } finally {
      setLoading(false);
    }
  };

  const createBook= async (book) => {
    try{
        const data = await PostBooks(book);
        fetchUsers();

    }
    catch (err) {

    }
    finally {
        setLoading(false);
      }
  };

  const editBooks=async (id,book) => {
    try{
        const data = await EditBooks(id,book);
        fetchUsers();

    }
    catch (err) {

    }
    finally {
        setLoading(false);
      }
  }

  const DeleteBooks=async (id) => {
    try{
        const data = await DeleteBooksA(id);
        fetchUsers();

    }
    catch (err) {

    }
    finally {
        setLoading(false);
      }
  }

  return { books,book, loading, error,createBook,editBooks,fetchBooksByid,DeleteBooks };
};

export default useBooks;