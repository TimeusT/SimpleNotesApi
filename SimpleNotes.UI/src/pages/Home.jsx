import { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { useGetUserNotes } from "../hooks/useGetAllNotes";
import { useDeleteNote } from "../hooks/useDeleteNote";
import EditNote from "./EditNoteForm";

import {
  List,
  ListItem,
  ListItemText,
  IconButton,
  ListItemSecondaryAction,
  Modal,
  Box,
} from "@mui/material";


const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

const Home = () => {
  const { user, isAuthenticated } = useAuth0();
  const { data: notes } = useGetUserNotes();
  const deleteNoteMutation = useDeleteNote();
  const [open, setOpen] = useState(false);
  const [selectedNote, setSelectedNote] = useState(null);

  const onEdit = (note) => {
    setOpen(true);
    setSelectedNote(note);
  };

  const onDelete = (id) => {
    deleteNoteMutation.mutateAsync(id);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <>
      <h1>Home Page</h1>
      {isAuthenticated && (
        <div>
          <p>
            Welcome, <strong>{user.name}</strong>
          </p>
          <p>
            Your nickname is <strong>{user.nickname}</strong>
          </p>
        </div>
      )}

      {notes?.length && (
        <List>
          {notes.map((note) => (
            <ListItem key={note.id}>
              <ListItemText primary={note.title} secondary={note.content} />
              <ListItemSecondaryAction>
                <IconButton
                  edge="end"
                  aria-label="Edit"
                  onClick={() => onEdit(note)}
                >
                  edit
                </IconButton>

                <IconButton
                  edge="end"
                  aria-label="Delete"
                  onClick={() => onDelete(note.id)}
                >
                  delete
                </IconButton>
              </ListItemSecondaryAction>
            </ListItem>
          ))}
        </List>
      )}
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <EditNote note={selectedNote} onSuccess={handleClose} onCancel={handleClose} />
        </Box>
      </Modal>
    </>
  );
};

export default Home;
