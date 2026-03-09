// SimpleNotes Create Note Form
import { useState } from 'react';
import axios from 'axios';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    userId: yup.number().typeError("Must be a number").positive("Must be a positive number").integer("Must be a int").required(),
    title: yup.string().required(),
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
      content: data.content
    }).then((response) => {
      console.log("User created:", response.data);
    }).catch((error) => {
      console.error("Error:", error); 
    });
  };
  
  return(
    <form onSubmit={handleSubmit(postNote)}>
      <input {...register("userId")} />
      {errors.userId && <span>{errors.userId.message}</span>}

      <input {...register("title")} />
      {errors.title && <span>{errors.title.message}</span>}

      <textarea {...register("content")} />
      {errors.content && <span>{errors.content.message}</span>}

      <button type='submit'>Submit</button>

    </form>
  );
}
