import {
	Autocomplete,
	Button,
	Card,
	CardActions,
	CardContent,
	IconButton,
	TextField,
} from "@mui/material";
import { Container } from "@mui/system";
import { useCallback, useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { BACKEND_API_URL } from "../../constants";
import { DrinkType } from "../../models/DrinkType";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import axios, { AxiosError } from "axios";
import { Drink } from "../../models/Drink";
import { debounce } from "lodash";

export const DrinkTypeAdd = () => {
	const navigate = useNavigate();

	const [DrinkType, setDrinkType] = useState<DrinkType>({
		name: "",
        nrOfBrands: 0,
        stock: 0,
        profitMargin: 0,
	});

	const [Drinks, setDrinks] = useState<Drink[]>([]);

    const addDrinkType = async (event: { preventDefault: () => void }) => {
        event.preventDefault();
        try{
            await axios
            .post(`${BACKEND_API_URL}/DrinkTypes/`, DrinkType)
            .then(()=>{
                alert("DrinkType added successfully");
            })
            .catch((reason: AxiosError)=>{
                console.log(reason.message);
                alert("Error adding DrinkType");
            });
            navigate("/drinkTypes");
        }catch(error){
            console.log(error);
            alert("Error adding DrinkType");
        }
    };

    useEffect(() => {
        const fetchDrinks = async () => {
          try {
            const response = await fetch(`${BACKEND_API_URL}/Drinks/`);
            const data = await response.json();
            setDrinks(data);
          } catch (error) {
            console.log(error);
          }
        };
       fetchDrinks();
      }, []);

      return(
        <Container>
            <h1>Add DrinkType</h1>
            <Card sx={{ minWidth: 275 }}>
                <CardContent>
                    <IconButton component={Link} sx={{ mr: 3 }} to={`/DrinkTypes`}>
                            <ArrowBackIcon />
                        </IconButton>{" "}
                    <form onSubmit={addDrinkType}>
                        <TextField
                            required
                            id="name"
                            label="Name"
                            variant="outlined"
                            value={DrinkType.name}
                            onChange={(event) => setDrinkType({ ...DrinkType, name: event.target.value })}
                        />
                        <TextField
                            required
                            id="nrOfBrands"
                            label="NrOfBrands"
                            variant="outlined"
                            value={DrinkType.nrOfBrands}
                            onChange={(event) => setDrinkType({ ...DrinkType, nrOfBrands: parseInt(event.target.value) })}
                        />
                        <TextField
                            required
                            id="stock"
                            label="Stock"
                            variant="outlined"
                            value={DrinkType.stock}
                            onChange={(event) => setDrinkType({ ...DrinkType, stock: parseInt(event.target.value) })}
                        />
                        <TextField
                            required
                            id="profitMargin"
                            label="ProfitMargin"
                            variant="outlined"
                            value={DrinkType.profitMargin}
                            onChange={(event) => setDrinkType({ ...DrinkType, profitMargin: parseInt(event.target.value) })}
                        />
                        <Button type="submit" variant="contained">Add DrinkType</Button>
                    </form>
                </CardContent>
            </Card>
        </Container>

      )
};