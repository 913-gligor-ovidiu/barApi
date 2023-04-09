import { useState } from "react";
import CssBaseline from "@mui/material/CssBaseline";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import * as React from "react";
import { AppBar, Toolbar, IconButton, Typography, Button } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppHome } from "./components/AppHome";
import { AppMenu } from "./components/AppMenu";
import { AlldrinkTypes } from "./components/drinktypes/AllDrinkTypes";
import { DrinkTypeDetails } from "./components/drinktypes/DrinkTypeDetails";
import { DrinkTypeUpdate } from "./components/drinktypes/DrinkTypeUpdate";
import { DrinkTypeDelete } from "./components/drinktypes/DrinkTypeDelete";
import { DrinkTypeAdd } from "./components/drinktypes/DrinkTypeAdd";
import { ShowDrinksMostOrdered } from "./components/drinktypes/DrinksMostOrdered";


function App() {
  return (
    <React.Fragment>
      <Router>
        <AppMenu />

        <Routes>
          <Route path="/" element={<AppHome />} />
          <Route path="/DrinkTypes" element={<AlldrinkTypes />} />
          <Route path="/DrinksReport" element={<ShowDrinksMostOrdered />} />

          <Route
            path="/DrinkTypes/:DrinkTypeId/details"
            element={<DrinkTypeDetails />}
          />
          <Route
            path="/DrinkTypes/:DrinkTypeId/edit"
            element={<DrinkTypeUpdate />}
          />
          <Route
            path="/DrinkTypes/:DrinkTypeId/delete"
            element={<DrinkTypeDelete />}
          />
          <Route path="/DrinkTypes/add" element={<DrinkTypeAdd />} />
        </Routes>
      </Router>
    </React.Fragment>
  );
}

export default App;