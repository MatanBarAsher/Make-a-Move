import axios from "axios";

export const makeAmoveFeedbackServer = {
  createFeedback: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/feedbacks`, data)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error create feedback", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  getFeedbacks: () =>
    axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/feedbacks`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching feedbacks:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  getFeedbacksByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/feedbacks/email/${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching feedbacks:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  createFeedbackContinue: (data) =>
    axios
      .post(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/secondFeedback`,
        data
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error create feedback", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  getContinueFeedbacksByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/SecondFeedback/email/${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching feedbacks:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),
};
