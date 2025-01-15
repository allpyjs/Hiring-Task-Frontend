import React, { useEffect, useState } from "react";
import { fetchTodos, createTodo, updateTodo, deleteTodo } from "../../services/api";
import TodoForm from "./TodoForm";

const TodoList = () => {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const { data } = await fetchTodos();
      setTodos(data);
    };
    fetchData();
  }, []);

  const handleCreate = async (todo) => {
    const { data } = await createTodo(todo);
    setTodos([...todos, data]);
  };

  const handleUpdate = async (id, updatedTodo) => {
    const { data } = await updateTodo(id, updatedTodo);
    setTodos(todos.map((todo) => (todo.id === id ? data : todo)));
  };

  const handleDelete = async (id) => {
    await deleteTodo(id);
    setTodos(todos.filter((todo) => todo.id !== id));
  };

  return (
    <div>
      <h2>Todos</h2>
      <TodoForm onSubmit={handleCreate} />
      <ul>
        {todos.map((todo) => (
          <li key={todo.id}>
            <h3>{todo.title}</h3>
            <p>{todo.description}</p>
            <button onClick={() => handleDelete(todo.id)}>Delete</button>
            <button onClick={() => handleUpdate(todo.id, { ...todo, status: !todo.status })}>
              {todo.status ? "Mark as Incomplete" : "Mark as Complete"}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TodoList;
