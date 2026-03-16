import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
//import App from './App.jsx'
import CreateNote from './CreateNoteForm.jsx'
import CreateUser from './CreateUserForm.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <CreateNote />
    <CreateUser />
  </StrictMode>,
)
