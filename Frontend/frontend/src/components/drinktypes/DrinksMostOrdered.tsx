import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  CircularProgress,
  Container,
  IconButton,
  Tooltip,
} from "@mui/material";

import { useEffect, useState } from "react";
import { BACKEND_API_URL } from "../../constants";
import {DrinksReport} from "../../models/DrinksReport";

export const ShowDrinksMostOrdered = () => {
    const [loading, setLoading] = useState(true);
    const [drinks, setDrinks] = useState([]);

    useEffect(() => {
        fetch(`${BACKEND_API_URL}/Drinks/mostOrdered`)
            .then((response) => response.json())
            .then((data) => {
                setDrinks(data);
                setLoading(false);
            });
    }, []);
    
    return (
        <Container>
            <h1>Drinks most ordered</h1>
            {loading && <CircularProgress />}
            {!loading && drinks.length === 0 && <div>No drinks found!</div>}
            {!loading && drinks.length > 0 && (
                <TableContainer component={Paper}>
                    <Table sx={{minWidth:900}} aria-aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell>Times ordered</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {drinks.map((drink: DrinksReport, index) => (
                                <TableRow key={drink.id}>
                                    <TableCell component="th" scope="row">
                                        {index + 1}
                                    </TableCell>
                                    <TableCell align="right">{drink.name}</TableCell>
                                    <TableCell align="right">{drink.total}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Container>
    );
};
