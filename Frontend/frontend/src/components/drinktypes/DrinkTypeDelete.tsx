import {
    Container,
    Card,
    CardContent,
    IconButton,
    CardActions,
    Button,
  } from "@mui/material";
  import { Link, useNavigate, useParams } from "react-router-dom";
  import ArrowBackIcon from "@mui/icons-material/ArrowBack";
  import axios, { AxiosError } from "axios";
  import { BACKEND_API_URL } from "../../constants";
  
  export const DrinkTypeDelete = () => {
    const { DrinkTypeId } = useParams();
    const navigate = useNavigate();
  
    const handleDelete = async (event: { preventDefault: () => void }) => {
      event.preventDefault();
      await axios
        .delete(`${BACKEND_API_URL}/DrinkTypes/${DrinkTypeId}`)
        .then(() => {
          alert("DrinkType deleted successfully!");
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          alert("Failed to delete DrinkType!");
        });
      // go to DrinkTypes list
      navigate("/DrinkTypes");
    };
  
    const handleCancel = (event: { preventDefault: () => void }) => {
      event.preventDefault();
      // go to DrinkTypes list
      navigate("/DrinkTypes");
    };
  
    return (
      <Container>
        <Card>
          <CardContent>
            <IconButton component={Link} sx={{ mr: 3 }} to={`/DrinkTypes`}>
              <ArrowBackIcon />
            </IconButton>{" "}
            Are you sure you want to delete this DrinkType? This cannot be undone!
          </CardContent>
          <CardActions>
            <Button onClick={handleDelete}>Yes</Button>
            <Button onClick={handleCancel}>Cancel</Button>
          </CardActions>
        </Card>
      </Container>
    );
  };