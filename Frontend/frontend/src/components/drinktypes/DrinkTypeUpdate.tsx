import {
    Button,
    Card,
    CardActions,
    CardContent,
    CircularProgress,
    Container,
    IconButton,
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
  } from "@mui/material";
  import { useEffect, useState } from "react";
  import { Link, useNavigate, useParams } from "react-router-dom";
  import ArrowBackIcon from "@mui/icons-material/ArrowBack";
  import axios, { AxiosError } from "axios";
  import { BACKEND_API_URL} from "../../constants";
  import { DrinkType } from "../../models/DrinkType"; 
  
  export const DrinkTypeUpdate = () => {
    const { DrinkTypeId } = useParams<{ DrinkTypeId: string }>();
    const navigate = useNavigate();
  
    const [loading, setLoading] = useState(false);
    const [drinkType, setDrinkType] = useState<DrinkType>({
        name: "",
        nrOfBrands: 0,
        stock: 0,
        profitMargin: 0,
    });
  
    useEffect(() => {
      const fetchDrinkType = async () => {
        const response = await fetch(
          `${BACKEND_API_URL}/DrinkTypes/${DrinkTypeId}/`
        );
        const drinkType = await response.json();
        setDrinkType({
            id: drinkType.id,
            name: drinkType.name,
            nrOfBrands: drinkType.nrOfBrands,
            stock: drinkType.stock,
            profitMargin: drinkType.profitMargin,
        });
        setLoading(false);
      };
      fetchDrinkType();
    }, [DrinkTypeId]);
  
    const handleUpdate = async (event: { preventDefault: () => void }) => {
      event.preventDefault();
      try {
        await axios
          .put(`${BACKEND_API_URL}/DrinkTypes/${DrinkTypeId}/`, drinkType)
          .then(() => {
            alert("DrinkType updated successfully!");
          })
          .catch((reason: AxiosError) => {
            console.log(reason.message);
            alert("Failed to update DrinkType!");
          });
        navigate("/DrinkTypes");
      } catch (error) {
        console.log(error);
        alert("Failed to update DrinkType!");
      }
    };
  
    const handleCancel = (event: { preventDefault: () => void }) => {
      event.preventDefault();
      navigate("/DrinkTypes");
    };
  
    return (
        <Container>
            <Card>
                <CardContent>
                    <h1>Update DrinkType</h1>
                    <form onSubmit={handleUpdate}>
                        <TextField
                            label="Name"
                            variant="outlined"
                            value={drinkType.name}
                            onChange={(event) =>
                                setDrinkType({
                                    ...drinkType,
                                    name: event.target.value,
                                })
                            }
                        />
                        <TextField
                            label="NrOfBrands"
                            variant="outlined"
                            value={drinkType.nrOfBrands}
                            onChange={(event) =>
                                setDrinkType({
                                    ...drinkType,
                                    nrOfBrands: parseInt(event.target.value) ,
                                })
                            }
                        />
                        <TextField
                            label="Stock"
                            variant="outlined"
                            value={drinkType.stock}
                            onChange={(event) =>
                                setDrinkType({
                                    ...drinkType,
                                    stock: parseInt(event.target.value) ,
                                })
                            }
                        />
                        <TextField
                            label="ProfitMargin"
                            variant="outlined"
                            value={drinkType.profitMargin}
                            onChange={(event) =>
                                setDrinkType({
                                    ...drinkType,
                                    profitMargin: parseInt(event.target.value) ,
                                })
                            }
                        />
                        </form>
                </CardContent>
                <CardActions>
                    <CardActions sx={{ justifyContent: "center" }}>
                        <Button type="submit" onClick={handleUpdate} variant="contained">
                            Update
                        </Button>
                        <Button onClick={handleCancel} variant="contained">
                            Cancel
                        </Button>
                    </CardActions>
                </CardActions>
            </Card>
    </Container>            
    );
  };