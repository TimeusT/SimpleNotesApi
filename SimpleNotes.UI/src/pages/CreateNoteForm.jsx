// SimpleNotes Create Note Form
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
    userId: yup.number().typeError(" Must be a number").positive(" Must be a positive number").integer(" Must be a int").required(),
    title: yup.string().trim().required(" A proper Title is required"),
    content: yup.string()
  })
  .required()

export default function CreateNote() {
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
    mutationFn: (data) => axios.post("https://localhost:7183/api/Note", data),
    onError: (error) => {
      if (error.response?.data?.errors) {
        const errorResponse = error.response.data.errors;
        Object.keys(errorResponse).forEach((key) => {
          setError(key, {
            type: "server",
            message: errorResponse[key][0] });
        });
      }
    },
    onSuccess: (data) => {
      console.log("Post successful:", data);
      reset();
    }
  });

  const submitNote = (data) => mutation.mutate(data);

  return(
    <form onSubmit={handleSubmit(submitNote)}>
      <h1>Create a Note</h1>
      <Box sx={{ flexGrow: 1 }}>
        <Grid container spacing={2}>
          <Grid size={12}>
            <TextField
              required
              label="User ID"
              variant="outlined"
              {...register("userId")}
              error={!!errors.userId}
              helperText={errors.userId?.message} />
          </Grid>
          <Grid size={12}>
            <TextField
              required
              label="Title"
              variant="outlined"
              {...register("title")}
              error={!!errors.title}
              helperText={errors.title?.message} />
          </Grid>
          <Grid size={12}>
            <TextField
              multiline
              label="Content"
              rows={4}
              {...register("content")}
              error={!!errors.content}
              helperText={errors.content?.message} />
          </Grid>
          <Grid>
            <Button variant="contained" type='submit'>Submit</Button>
          </Grid>
        </Grid>
      </Box>
    </form>
  );
}
