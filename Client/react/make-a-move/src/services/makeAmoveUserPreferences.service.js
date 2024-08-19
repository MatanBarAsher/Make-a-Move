import { dark } from "@mui/material/styles/createPalette";
import axios from "axios";

export const makeAmoveUserPreferencesServer = {
  ReadUserPreferencesByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/UserPreference/ReadUserPreferencesByEmail?email=${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching preferences", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  UpdateUserPreference: async (data) =>
    await axios
      .put(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/UserPreference`,
        data
      )
      .then((res) => res.status) //returning data
      .catch((error) => error.status),
};
