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
import React from "react";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { DrinkType } from "../../models/DrinkType";
import { BACKEND_API_URL } from "../../constants";
import ReadMoreIcon from "@mui/icons-material/ReadMore";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import AddIcon from "@mui/icons-material/Add";

export const AlldrinkTypes = () => {
	const [loading, setLoading] = useState(false);
	const [drinkTypes, setdrinkTypes] = useState<DrinkType[]>([]);
	const [sorting, setSorting] = useState({
		key: "name",
		ascending: true,
	});

	function applySorting(key: string, ascending: boolean) {
		setSorting({ key: key, ascending: ascending });
	  }

	  useEffect(() => {
		if (drinkTypes.length === 0) {
		  return;
		}
	
		const currentDrinkTypes = [...drinkTypes];
	
		const sortedCurrentDrinkTypes = currentDrinkTypes.sort((a, b) => {
			return a[sorting.key].localeCompare(b[sorting.key]);										
		});
	
		setdrinkTypes(
		  sorting.ascending ? sortedCurrentDrinkTypes : sortedCurrentDrinkTypes.reverse()
		);
	  }, [sorting]);

	useEffect(() => {
		setLoading(true);
		fetch(`${BACKEND_API_URL}/DrinkTypes`)
			.then((response) => response.json())
			.then((data) => {
				setdrinkTypes(data);
				setLoading(false);
			});
	}, []);
	console.log(drinkTypes)

	return (
		<Container>
			<h1>All drinkTypes</h1>

			{loading && <CircularProgress />}
			{!loading && drinkTypes.length === 0 && <p>No drinkTypes found</p>}
			{!loading && (
				<IconButton component={Link} sx={{ mr: 3 }} to={`/drinkTypes/add`}>
					<Tooltip title="Add a new drinkType" arrow>
						<AddIcon color="primary" />
					</Tooltip>
				</IconButton>
			)}
			{!loading && drinkTypes.length > 0 && (
				<TableContainer component={Paper}>
					<Table sx={{ minWidth: 650 }} aria-label="simple table">
						<TableHead>
							<TableRow>
								<TableCell>#</TableCell>
								<TableCell
									align="right"
									style={{ cursor: "pointer" }}
									onClick={() => applySorting("name", !sorting.ascending)}>
									Name
								</TableCell>
								<TableCell align="right">Stock</TableCell>
								<TableCell align="center">Operations</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{drinkTypes.map((drinkType, index) => (
								<TableRow key={drinkType.id}>
									<TableCell component="th" scope="row">
										{index + 1}
									</TableCell>
									<TableCell component="th" scope="row">
										<Link to={`/drinkTypes/${drinkType.id}/details`} title="View drinkType details">
											{drinkType.name}
										</Link>
									</TableCell>
									<TableCell align="right">{drinkType.stock}</TableCell>
									<TableCell align="right">
										<IconButton
											component={Link}
											sx={{ mr: 3 }}
											to={`/drinkTypes/${drinkType.id}/details`}>
											<Tooltip title="View drinkType details" arrow>
												<ReadMoreIcon color="primary" />
											</Tooltip>
										</IconButton>

										<IconButton component={Link} sx={{ mr: 3 }} to={`/drinkTypes/${drinkType.id}/edit`}>
											<EditIcon />
										</IconButton>

										<IconButton component={Link} sx={{ mr: 3 }} to={`/drinkTypes/${drinkType.id}/delete`}>
											<DeleteForeverIcon sx={{ color: "red" }} />
										</IconButton>
									</TableCell>
								</TableRow>
							))}
						</TableBody>
					</Table>
				</TableContainer>
			)}
		</Container>
	);
};
