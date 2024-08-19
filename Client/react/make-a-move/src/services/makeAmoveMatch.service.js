import axios from "axios";

export const makeAmoveMatchServer = {
  getAllMatches: () =>
    axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/matches`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching matches:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),
  createMatch: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/matches`, data)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error create match", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  getMatchesByEmail: (email) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/matches/ReadMatchesByEmail?inputEmail=${email}`
      )
      .then((res) => res.data)
      .catch((res) => {
        console.error("Error reading matchs", res);
        throw error; // Rethrow the error to be caught by the caller
      });
  },
};
