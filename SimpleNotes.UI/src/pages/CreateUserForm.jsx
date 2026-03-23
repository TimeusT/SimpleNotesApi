// import
import axios from "axios";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import { useAuth0 } from "@auth0/auth0-react";
import { useCreateUser } from "../hooks/useUserByEmail";

// schema
const schema = yup
  .object({
    firstName: yup.string().required(),
    lastName: yup.string().required(),
    age: yup
      .number()
      .typeError(" Can only be a number")
      .positive(" Can only be a positive number")
      .required(),
    email: yup.string(),
    address: yup.string(),
  })
  .required();

// default function
export default function CreateUser() {
  const { user } = useAuth0();
  const createUserMutation = useCreateUser();

  // error handler
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema, { abortEarly: false }),
    mode: "onChange",
  });

  // object function
  const postUser = (data) => {
    createUserMutation
      .mutateAsync({
        firstName: data.firstName,
        lastName: data.lastName,
        age: data.age,
        email: data.email,
        address: data.address,
      })
      .then((user) => {
        console.log("User created:", user);
      })
      .catch((error) => {
        if (error.response?.data?.errors) {
          const apiErrors = error.response.data.errors;

          Object.keys(apiErrors).forEach((key) => {
            setError(key, {
              type: "server",
              message: apiErrors[key][0],
            });
          });
        }
      });
  };

  // return grid
  return (
    <form onSubmit={handleSubmit(postUser)}>
      <h1>Create a User</h1>
      <Box sx={{ flexGrow: 1 }}>
        <Grid container spacing={2}>
          <Grid size={12}>
            <TextField
              required
              label="First Name"
              variant="outlined"
              {...register("firstName")}
              helperText={errors.firstName?.message}
              error={!!errors.firstName}
            />
          </Grid>
          <Grid size={12}>
            <TextField
              required
              label="Last Name"
              variant="outlined"
              {...register("lastName")}
              helperText={errors.lastName?.message}
              error={!!errors.lastName}
            />
          </Grid>
          <Grid size={12}>
            <TextField
              required
              label="Age"
              variant="outlined"
              {...register("age")}
              helperText={errors.age?.message}
              error={!!errors.age}
            />
          </Grid>
          <Grid size={12}>
            <TextField
              slotProps={{ input: { readonly: true } }}
              value={user.email || ""}
              label="Email"
              variant="outlined"
              {...register("email")}
              helperText={errors.email?.message}
              error={!!errors.email}
            />
          </Grid>
          <Grid size={12}>
            <TextField
              required
              label="Address"
              variant="outlined"
              {...register("address")}
              helperText={errors.address?.message}
              error={!!errors.address}
            />
          </Grid>
          <Grid>
            <button type="submit">Submit</button>
          </Grid>
        </Grid>
      </Box>
    </form>
  );
}
