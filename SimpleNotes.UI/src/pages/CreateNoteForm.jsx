// SimpleNotes Create Note Form
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import { useCreateNote } from "../hooks/useCreateNote";
import useUserByEmail from "../hooks/useUserByEmail";
import { Alert } from "@mui/material";
import { useEffect } from "react";
import { useState } from "react";

const schema = yup
  .object({
    title: yup.string().trim().required("Title is required"),
    content: yup.string(),
    userId: yup.number().required("User ID is required"),
  })
  .required();

export default function CreateNote() {
  const { data: user } = useUserByEmail();
  const createNoteMutation = useCreateNote();
  const [severityState, setSeverityState] = useState(null);

  const {
    register,
    handleSubmit,
    setError,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema, { abortEarly: false }),
    mode: "onChange",
  });

  useEffect(() => {
    if (user?.id) {
      reset({
        userId: user.id,
      });
    }
  }, [user]);

  const postNote = async (data) => {
    try {
      const note = await createNoteMutation.mutateAsync(data);
      console.log("Note create:", note);
      setSeverityState("success");
      reset();
    } catch (error) {
      setSeverityState("error");
      if (error.response?.data?.errors) {
        const apiErrors = error.response.data.errors;

        Object.keys(apiErrors).forEach((key) => {
          setError(key, {
            type: "server",
            message: apiErrors[key][0],
          });
        });
      }
    }
  };

  return (
    <form onSubmit={handleSubmit(postNote)}>
      <h1>Create a Note</h1>
      {Object.keys(errors).length > 0 && severityState === "error" && (
        <Box sx={{ my: 2 }}>
          <Alert severity="error">
            <ul>
              {Object.entries(errors).map(([field, error]) => (
                <li key={field}>{error.message}</li>
              ))}
            </ul>
          </Alert>
        </Box>
      )}
      {severityState === "success" && (
        <Box sx={{ my: 2 }}>
          <Alert severity="success">
            Note Created Successfully!
          </Alert>
        </Box>
      )}
      <Box sx={{ flexGrow: 1 }}>
        <Grid container spacing={2}>
          <Grid size={12}>
            <TextField
              required
              label="Title"
              variant="outlined"
              {...register("title")}
              helperText={errors.title?.message}
              error={!!errors.title}
            />
          </Grid>
          <Grid size={12}>
            <TextField
              multiline
              label="Content"
              rows={4}
              {...register("content")}
              helperText={errors.content?.message}
              error={!!errors.content}
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
