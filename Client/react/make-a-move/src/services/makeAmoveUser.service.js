import { Password } from "@mui/icons-material";
import axios from "axios";

export const makeAmoveUserServer = {
  getAllUsers: () =>
    axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching users:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  createUser: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users`, {
        email: data.email,
        firstName: data.firstName,
        lastName: data.lastName,
        password: data.password,
        gender: data.gender,
        image: [],
        height: data.height,
        birthday: data.birthday,
        phoneNumber: data.phoneNumber,
        isActive: true,
        city: data.city,
        preferencesIds: [""],
        currentPlace: 0,
        persoalText: data.description,
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.log(data);
        console.error("Error create user:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  updateUser: (data) =>
    axios
      .put(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users/update`, data)
      .then((res) => res.status) //returning data
      .catch((error) => {
        console.error("Error update user:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  login: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/Users/Login`, {
        email: data.email,
        firstName: "string",
        lastName: "string",
        password: data.password,
        gender: 0,
        image: ["string"],
        height: 0,
        birthday: "2024-05-17T08:24:11.516Z",
        phoneNumber: "string",
        isActive: data.isActive,
        city: "string",
        personalInterestsIds: ["string"],
        preferencesIds: ["string"],
        currentPlace: 0,
        persoalText: "s",
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error on login:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  setPreferences: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/UserPreference`, {
        email: data.email,
        preferenceGender: data.preferedGender,
        minAge: `${data.ageRange[0]}`,
        maxAge: `${data.ageRange[1]}`,
        minHeight: `${data.heightRange[0]}`,
        maxHeight: `${data.heightRange[1]}`,
        maxDistance: `${data.maxDistance}`,
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error on setting Preferences:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  setPreferences: (data) =>
    axios
      .post(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/UserPreference`, {
        email: data.email,
        preferenceGender: data.preferedGender,
        minAge: `${data.ageRange[0]}`,
        maxAge: `${data.ageRange[1]}`,
        minHeight: `${data.heightRange[0]}`,
        maxHeight: `${data.heightRange[1]}`,
        maxDistance: `${data.maxDistance}`,
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error on setting Preferences:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),
  updatePreferences: (data) =>
    axios
      .put(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/UserPreference`, {
        email: data.email,
        preferenceGender: data.preferedGender,
        minAge: `${data.ageRange[0]}`,
        maxAge: `${data.ageRange[1]}`,
        minHeight: `${data.heightRange[0]}`,
        maxHeight: `${data.heightRange[1]}`,
        maxDistance: `${data.maxDistance}`,
      })
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error on updating Preferences:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  setLocationValue: async (place, user) => {
    try {
      const res = await axios.post(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/UpdatePlace/${user}`,
        {
          name: place.name,
          address: place.address,
          typeOfPlace: "",
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      return res;
    } catch (error) {
      console.error("Error on setting current place:", error);
      throw error; // Rethrow the error to be caught by the caller
    }
  },

  /**
   * check if the given key exist for the given value
   * @param  data key and value object @example {key:'userName', value:'yael'}
   * @returns boolean
   */
  checkExist: (key, value) =>
    axios
      .post(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/checkExistingUserByKeyAndValue/${key}`,
        value
      )
      .then((res) => res.key) //returning data
      .catch((error) => {
        console.error("Error:", error);
        throw error; // Rethrow the error to be caught by the caller
      }),

  changeImages: async ({ currentEmail, formData }) => {
    try {
      console.log(currentEmail);
      const response = await axios.post(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/changeImages/${currentEmail}`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
            "Current-Email": currentEmail, // Assuming your server needs this header
          },
        }
      );
      return response.data;
    } catch (error) {
      console.error("Error uploading images:", error);
      return null;
    }
  },
  getAllUsersEmails: () =>
    axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users/getEmails`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching users:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  AddImage: async ({ formData }) => {
    try {
      const response = await axios.post(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users/AddImages`,
        formData,
        { headers: { "Content-Type": "multipart/form-data" } }
      );
      return response.data;
    } catch (error) {
      console.error("Error uploading image:", error);
      return null;
    }
  },

  getUserByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/GetUserByEmail/${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching user:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  GetUserNoPasswordByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/GetUserDetailsNoPasswordByEmail/${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching user:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  GetImagesByEmail: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/getImagesByEmail/${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching user images:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  getImageByID: async (imageId) =>
    await axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/GetImage?imageId=${imageId}`,
        {
          responseType: "blob",
        }
      )
      .then((res) => URL.createObjectURL(res.data))
      .catch((error) => {
        console.error("Error fetching image:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),

  readUsersByPreference: (email) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/ReadUsersByPreference?userEmail=${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching users:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },

  GetPersonalInterests: () => {
    return axios
      .get(`${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/PersonalInterests`)
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching PersonalInterests:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },

  postPersonalInterests: (email, selections) => {
    return axios
      .post(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/addpersonalinterests/${email}`,
        selections
      )
      .then((res) => res)
      .catch((res) => res);
  },

  GetPersonalInterestsByEmail: (email) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/interests/${email}`
      )
      .then((res) => res.data)
      .catch((res) => res.data);
  },

  UpdatePersonalInterestsByEmail: (email, selections) => {
    return axios
      .put(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/UpdatePersonalInterestsByEmail/${email}`,
        selections
      )
      .then((res) => res.data)
      .catch((res) => res.data);
  },

  getMyLikesByEmail: (email) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/getMylikes?email=${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching likes:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },

  getEmailsThatLikesMe: (email) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/getWhoLikesMe?email=${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching likes:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },

  handleNewLike: (userA, userB, currentPlace) => {
    return axios
      .post(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/Users/AddLike/?userEmail=${userA}&likedUserEmail=${userB}&currentPlace=${currentPlace}`
      )
      .then((res) => res.data)
      .catch((res) => res.data);
  },

  getMatchScoreByEmails: (email1, email2) => {
    return axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/GetMatchPercentage?email1=${email1}&email2=${email2}`
      )
      .then((res) => res.data)
      .catch((error) => {
        console.error("Error fetching score:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },

  getUserFriendsByEmail: (email) => {
    return axios
      .get(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/users/friends/${email}`
      )
      .then((res) => res.data)
      .catch((error) => {
        console.error("Error fetching score:", error);
        throw error; // Rethrow the error to be caught by the caller}
      });
  },
  getAnalysis: (email) =>
    axios
      .get(
        `${
          import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL
        }/users/getAnalysis?email=${email}`
      )
      .then((res) => res.data) //returning data
      .catch((error) => {
        console.error("Error fetching user analysis:", error);
        throw error; // Rethrow the error to be caught by the caller}
      }),
};
