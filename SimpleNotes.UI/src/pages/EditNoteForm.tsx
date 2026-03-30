import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import { useEditNote } from "../hooks/useEditNote";
import { Alert, Button } from "@mui/material";
import { Note } from "../types/Note";
import { useMessage } from "../hooks/useMessage";

const schema = yup
  .object({
    id: yup.number().required("ID is required"),
    title: yup.string().trim().required("Title is required"),
    content: yup.string(),
    userId: yup.number().required("User ID is required"),
  })
  .required();

interface EditNoteProps {
  note: Note;
  onSuccess: (updatedNote: Note) => void;
  onCancel: () => void;
}

export default function EditNote({ note, onSuccess, onCancel }: EditNoteProps) {
  const editNoteMutation = useEditNote();
  const { showMessage } = useMessage();

  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm({
    defaultValues: {
      id: note.id,
      title: note.title,
      content: note.content,
      userId: note.userId,
    },
    resolver: yupResolver(schema, { abortEarly: false }),
    mode: "onChange",
  });

  const editNote = async (data: any) => {
    try {
      const note = await editNoteMutation.mutateAsync(data);
      onSuccess(note);
      showMessage("Note Updated!");
    } catch (error: any) {
      if (error.response?.data?.errors) {
        showMessage("Something went wrong.", "error");
        const apiErrors = error.response.data.errors;
        Object.keys(apiErrors).forEach((key: any) => {
          setError(key, {
            type: "server",
            message: apiErrors[key][0],
          });
        });
      }
    }
  };

  return (
    <form onSubmit={handleSubmit(editNote)}>
      <h1>Edit Note</h1>
      {Object.keys(errors).length > 0 && (
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
            <Button type="submit">Save</Button>
            <Button type="button" onClick={onCancel}>
              Cancel
            </Button>
          </Grid>
        </Grid>
      </Box>
    </form>
  );
}
