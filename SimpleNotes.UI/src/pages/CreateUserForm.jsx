import axios from 'axios';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useMutation } from '@tanstack/react-query';

const schema = yup
  .object({
    firstName: yup.string().trim().required(" Valid First Name is required."),
    lastName: yup.string().trim().required(" Valid Last Name is required."),
    age: yup.number().typeError(" Must be valid age.").positive(" Must be a positive number.").required(" Valid Age is required."),
    email: yup.string().trim().required(" Valid Email is required.")
  })
  .required()

  export default function CreateUser() {
    const {
      register,
      handleSubmit,
      setError,
      reset,
      formState: { errors }
    } = useForm({
      resolver: yupResolver(schema),
      mode: "onChange"
    });

    const mutation = useMutation({
      mutationFn: (data) => axios.post("https://localhost:7183/api/User", data),
      onError: (error) => {
        if (error.response?.data?.errors) {
          const errorResponse = error.response.data.errors;
          Object.keys(errorResponse).forEach((key) => {
            setError(key, {
              type: "server",
              message: errorResponse[key][0]});
          })
        }
      },
      onSuccess: (data) => {
        console.log("Post successful:", data);
        reset();
      }
    });

    const submitUser = (data) => mutation.mutate(data);

    return(
      <form onSubmit={handleSubmit(submitUser)}>
        <h1>Create a User</h1>
        <Box sx={{flexGrow: 1}}>
          <Grid container spacing={2}>
            <Grid size={12}>
              <TextField
                required
                label="First Name"
                variant="outlined"
                {...register("firstName")}
                error={!!errors.firstName}
                helperText={errors.firstName?.message} />
            </Grid>
            <Grid size={12}>
              <TextField
                required
                label="Last Name"
                variant="outlined"
                {...register("lastName")}
                error={!!errors.lastName}
                helperText={errors.lastName?.message} />
            </Grid>
            <Grid size={12}>
              <TextField
                required
                label="Age"
                variant="outlined"
                {...register("age")}
                error={!!errors.age}
                helperText={errors.age?.message} />
            </Grid>
            <Grid size={12}>
              <TextField
                required
                label="Email"
                variant="outlined"
                {...register("email")}
                error={!!errors.email}
                helperText={errors.email?.message} />
            </Grid>
            <Grid>
              <Button variant="contained" type="submit">Submit</Button>
            </Grid>
          </Grid>
        </Box>
      </form>
    );
  }