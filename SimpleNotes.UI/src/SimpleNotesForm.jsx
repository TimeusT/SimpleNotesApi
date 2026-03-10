// SimpleNotes Create Note Form
import axios from 'axios';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';

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
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema)
  });

  const postNote = (data) => {
    axios.post("https://localhost:7183/api/Note", {
        userId: data.userId,
        title: data.title,
        content: data.content})
      .then((response) => {console.log("User created:", response.data)})
      .catch((error) => {
        const problem = error.response;
        if (problem) {
          console.log("Response Data:", problem.data.title);
        } else {
          console.log("Else data:", problem?.title);
        }});
      };

  return(
    <form onSubmit={handleSubmit(postNote)}>
      <Box sx={{ flexGrow: 1 }}>
        <Grid container spacing={2}>
          <Grid size={12}>
            <TextField
              required
              label="User ID"
              variant="outlined"
              {...register("userId")}
              helperText={errors.userId && <span>{errors.userId?.message}</span>}
              error={errors.userId} />
          </Grid>
          <Grid size={12}>
            <TextField
              required
              label="Title"
              variant="outlined"
              {...register("title")}
              helperText={errors.title && <span>{errors.title.message}</span>}
              error={errors.title} />
          </Grid>
          <Grid size={12}>
            <TextField
              multiline
              label="Content"
              rows={4}
              {...register("content")}
              helperText={errors.content && <span>{errors.content.message}</span>}
              error={errors.content} />
          </Grid>
          <Grid>
            <button type='submit'>Submit</button>
          </Grid>
        </Grid>
      </Box>
    </form>
  );
}
