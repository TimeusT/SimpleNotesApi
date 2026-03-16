// import
import axios from 'axios';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';

// schema
const schema = yup.object({
    id: yup.number().typeError(" Must be a number").positive(" Can only be a positive number"),
    firstName: yup.string().required(),
    lastName: yup.string().required(),
    age: yup.number().typeError(" Can only be a number").positive(" Can only be a positive number").required(),
    email: yup.string(),
    address: yup.string()
}).required();

// default function
export default function CreateUser() {
    // error handler
    const {
        register,
        handleSubmit,
        setError,
        formState: { errors },
    } = useForm({
        resolver: yupResolver(schema, { abortEarly: false }),
        mode: "onChange"
    });
    
    // object function
    const postUser = (data) => {
        axios.post("https://localhost:7138/api/User", {
            id: data.userId,
            firstName: data.firstName,
            lastName: data.lastName,
            age: data.age,
            email: data.email,
            address: data.address})
        .then((response) => {console.log("User created:", response.data)})
        .catch((error) => {
        if (error.response?.data?.errors) {
            const apiErrors = error.response.data.errors;

            Object.keys(apiErrors).forEach((key) => {
                setError(key, {
                type: "server",
                message: apiErrors[key][0],
                });
            });
        }});
    }

    // return grid
    return(
        <form onSubmit={handleSubmit(postUser)}>
            <h1>Create a User</h1>
            <Box sx={{ flexGrow: 1 }}>
                <Grid container spacing={2}>
                    <Grid size={12}>
                        <TextField
                        required
                        label="ID"
                        variant="outlined"
                        {...register("id")}
                        helperText={errors.id?.message}
                        error={!!errors.id} />
                    </Grid>
                    <Grid size={12}>
                        <TextField
                        required
                        label="First Name"
                        variant="outlined"
                        helperText={errors.firstName?.message}
                        error={!!errors.firstName} />
                    </Grid>
                    <Grid size={12}>
                        <TextField
                        required
                        label="Last Name"
                        variant="outlined"
                        helperText={errors.lastName?.message}
                        error={!!errors.lastName} />
                    </Grid>
                    <Grid size={12}>
                        <TextField
                        required
                        label="Age"
                        variant="outlined"
                        helperText={errors.age?.message}
                        error={!!errors.age} />
                    </Grid>
                    <Grid size={12}>
                        <TextField
                        required
                        label="Email"
                        variant="outlined"
                        helperText={errors.email?.message}
                        error={!!errors.email} />
                    </Grid>
                    <Grid size={12}>
                        <TextField
                        required
                        label="Address"
                        variant="outlined"
                        helperText={errors.address?.message}
                        error={!!errors.address} />
                    </Grid>
                    <Grid>
                        <button type='submit'>Submit</button>
                    </Grid>
                </Grid>
            </Box>
        </form>
    );
}