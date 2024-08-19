import axios from "axios";

export const makeAmoveAdminServer = {
  getAllAdmins: () =>
    axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/admin`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching admins:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),
  createAdmin: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/admin`, data)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error create admin:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  updateAdmin: (email, data) =>
    axios
      .put(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/admin/${email}`,
        data
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error update admin:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  loginAdmin: ({ phone, password }) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/admins/login`, {
        phone,
        password,
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error login:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
};
