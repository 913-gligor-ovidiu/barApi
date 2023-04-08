import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";

export default function App() {
  const [drinks, setDrinks] = useState([]);
  const [newDrink, setNewDrink] = useState({
    name: "",
    price: "",
    quantity: "",
    abv: "",
    drinkType: "",
  });
  const [filterPrice, setFilterPrice] = useState("");
  const [editDrink, setEditDrink] = useState(null);

  useEffect(() => {
    getDrinks();
  }, []);

  const getDrinks = () => {
    axios
      .get("https://barapi.azurewebsites.net/api/Drinks")
      .then((response) => setDrinks(response.data))
      .catch((error) => console.log(error));
  };
  console.log(drinks);
  const handleAddDrinkChange = (event) => {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    setNewDrink((prevDrink) => ({ ...prevDrink, [name]: value }));
  };

  const handleAddDrinkSubmit = (event) => {
    event.preventDefault();

    if (editDrink) {
      axios
        .put(`https://barapi.azurewebsites.net/api/Drinks/${editDrink.id}`, newDrink)
        .then(() => {
          setEditDrink(null);
          setNewDrink({
            name: "",
            price: "",
            quantity: "",
            abv: "",
            drinkType: "",
          });
          getDrinks();
        })
        .catch((error) => console.log(error));
    } else {
      axios
        .post("https://barapi.azurewebsites.net/api/Drinks", newDrink)
        .then(() => {
          setNewDrink({
            name: "",
            price: "",
            quantity: "",
            abv: "",
            drinkType: "",
          });
          getDrinks();
        })
        .catch((error) => console.log(error));
    }
  };

  const handleDeleteDrink = (id) => {
    axios
      .delete(`https://barapi.azurewebsites.net/api/Drinks/${id}`)
      .then(() => getDrinks())
      .catch((error) => console.log(error));
  };

  const handleEditDrink = (drink) => {
    setEditDrink(drink);
    setNewDrink(drink);
  };

  const handleFilterDrinksSubmit = (event) => {
    event.preventDefault();

    axios
      .get(`https://barapi.azurewebsites.net/api/Drinks/filter/${filterPrice}`)
      .then((response) => setDrinks(response.data))
      .catch((error) => console.log(error));
  };

  return (
    <div>
      {/* Add Drink Form */}
      <h2>{editDrink ? "Edit Drink" : "Add Drink"}</h2>
      <form onSubmit={handleAddDrinkSubmit}>
        <TextField
          label="Name"
          name="name"
          required
          onChange={handleAddDrinkChange}
          value={newDrink.name}
        />
        <TextField
          label="Price"
          name="price"
          type="number"
          required
          onChange={handleAddDrinkChange}
          value={newDrink.price}
        />
        <TextField
          label="Quantity"
          name="quantity"
          type="number"
          required
          onChange={handleAddDrinkChange}
          value={newDrink.quantity}
        />
        <TextField
          label="ABV"
          name="abv"
          type="number"
          required
          onChange={handleAddDrinkChange}
          value={newDrink.abv}
        />
        <TextField
          label="Drink Type"
          name="drinkType"
          required
          onChange={handleAddDrinkChange}
          value={newDrink.drinkType}
        />
          <Button variant="contained" color="primary" type="submit">
          {editDrink ? "Save" : "Add"}
          </Button>
          </form>
          {/* Filter Drinks Form */}
  <h2>Filter Drinks</h2>
  <form onSubmit={handleFilterDrinksSubmit}>
    <TextField
      label="Price"
      name="price"
      type="number"
      onChange={(event) => setFilterPrice(event.target.value)}
      value={filterPrice}
    />
    <Button variant="contained" color="primary" type="submit">
      Filter
    </Button>
  </form>

  {/* Drinks Table */}
  <h2>Drinks</h2>
  <TableContainer component={Paper}>
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Name</TableCell>
          <TableCell>Price</TableCell>
          <TableCell>Quantity</TableCell>
          <TableCell>ABV</TableCell>
          <TableCell>Drink Type</TableCell>
          <TableCell></TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {drinks.map((drink) => (
          <TableRow key={drink.id}>
            <TableCell>{drink.name}</TableCell>
            <TableCell>{drink.price}</TableCell>
            <TableCell>{drink.quantity}</TableCell>
            <TableCell>{drink.abv}</TableCell>
            <TableCell>{drink.drinkType}</TableCell>
            <TableCell>
              <Button
                variant="contained"
                color="primary"
                onClick={() => handleEditDrink(drink)}
              >
                Edit
              </Button>
            </TableCell>
            <TableCell>
              <Button
                variant="contained"
                color="secondary"
                onClick={() => handleDeleteDrink(drink.id)}
              >
                Delete
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer>
</div>
);
}