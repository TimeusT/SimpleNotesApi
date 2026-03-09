// SimpleNotes Create Note Form
import { useState } from 'react';
import axios from 'axios';

export default function CreateNote() {
  const [userId, setUserId] = useState("");
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  const handleSubmit = (s) => {
    s.preventDefault();
    axios.post("https://localhost:7183/api/Note", {
      userId: userId,
      title: title,
      content: content
    }).then((response) => {
      console.log("User created:", response.data);
    }).catch((error) => {
      console.error("Error:", error); 
    });
  };
  
  return(
    <form onSubmit={handleSubmit}>
      <div>
        <input
          value={userId}
          onChange={(c) => setUserId(c.target.value)}
          placeholder="User ID"/>
      </div>
      <div className="inputField">
        <input
          value={title}
          onChange={(c) => setTitle(c.target.value)}
          placeholder="Title"/>
      </div>
      <div className="inputField">
        <textarea
          value={content}
          onChange={(c) => setContent(c.target.value)}
          placeholder="Content"/>
      </div>
      <div>
        <button type="submit">Submit</button>
      </div>
    </form>
  );
}
