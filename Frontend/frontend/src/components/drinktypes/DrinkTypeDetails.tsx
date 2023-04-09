import { Card, CardActions, CardContent, IconButton } from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { BACKEND_API_URL} from "../../constants";
import {DrinkType} from "../../models/DrinkType";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";

export const DrinkTypeDetails = () => {
  const { DrinkTypeId } = useParams();
  const [DrinkType, setDrinkType] = useState<DrinkType>();

  useEffect(() => {
    const fetchDrinkType = async () => {
      // TODO: use axios instead of fetch
      // TODO: handle errors
      // TODO: handle loading state
      const response = await fetch(
        `${BACKEND_API_URL}/DrinkTypes/${DrinkTypeId}`
      );
      const DrinkType = await response.json();
      setDrinkType(DrinkType);
    };
    fetchDrinkType();
  }, [DrinkTypeId]);

  return (
    <Container>
        <Card>
            <CardContent>
                <IconButton component={Link} sx={{ mr: 3 }} to={`/DrinkTypes`}>
                    <ArrowBackIcon />
                </IconButton>
                <h1>{DrinkType?.name}</h1>
                <p>Number of brands: {DrinkType?.nrOfBrands}</p>
                <p>Stock: {DrinkType?.stock}</p>
                <p>Profit margin: {DrinkType?.profitMargin}</p>
                <p>Drinks:</p>
                <ul>
                    {DrinkType?.drinks?.map((drink) => (
                        <li key={drink.id}>{drink.name}</li>
                    ))}
                </ul>
            </CardContent>
            <CardActions>
                <IconButton component={Link} sx={{ mr: 3 }} to={`/DrinkTypes/${DrinkTypeId}/edit`}>
                    <EditIcon />
                </IconButton>
                <IconButton component={Link} sx={{ mr: 3 }} to={`/DrinkTypes/${DrinkTypeId}/delete`}>
                    <DeleteForeverIcon sx={{ color: "red" }} />
                </IconButton>
            </CardActions>
        </Card>
    </Container>

);
};