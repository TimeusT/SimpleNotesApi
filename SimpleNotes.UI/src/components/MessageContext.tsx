import { Alert, Snackbar, SnackbarCloseReason } from "@mui/material";
import { ReactNode, useState, createContext } from "react";

type MessageType = "success" | "info" | "warning" | "error";

interface Message {
  text: string;
  type: MessageType;
}

interface MessageContextProps {
  showMessage: (text: string, type?: MessageType) => void;
}

export const MessageContext = createContext<MessageContextProps | undefined>(undefined);

export const MessageProvider = ({ children }: { children: ReactNode }) => {
  const [message, setMessage] = useState<Message | null>(null);
  const [open, setOpen] = useState(false);

  const showMessage = (text: string, type?: MessageType) => {
    setMessage({text, type: type ?? "success"});
    setOpen(true);
  }

  const handleClose = (_: any, reason: string) => {
    if (reason == "clickaway"){
      return;
    }
    setOpen(false);
  }

  const handleAlertClose = () =>{
    setOpen(false);
  }

  return(
    <MessageContext.Provider value={{showMessage}}>
      {children}

      <Snackbar
        open={open}
        autoHideDuration={4 * 1000}
        onClose={handleClose}
        anchorOrigin={{vertical: "top", horizontal: "right"}}
      >
        {message ? (
          <Alert onClose={handleAlertClose} severity={message.type} variant="filled">
            {message.text}
          </Alert>
        ) : undefined}
      </Snackbar>
    </MessageContext.Provider>
  );
};
