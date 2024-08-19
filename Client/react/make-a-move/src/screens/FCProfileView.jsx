import { React, useEffect, useState } from "react";
import FCHamburger from "../components/FCHamburger";
import FCMatchScore from "../components/FCMatchScore";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import FavoriteIcon from "@mui/icons-material/Favorite";
import background from "../assets/images/Matan.jpg";
import { makeAmoveMatchServer, makeAmoveUserServer } from "../services";
import { useAsync } from "../hooks";
import FCBackArrow from "../components/FCBackArrow";
import { useRecoilValue } from "recoil";
import { myDetailsState } from "../recoil/selectors";
import { Percent } from "@mui/icons-material";
import axios from "axios";
import FCMatchModal from "./FCMatchModal";
import { AlertDialog } from "../components";
import { keyframes } from "@emotion/react";

export default function FCProfileView(userToShow) {
  const [cityMap, setCityMap] = useState({});
  const [tempCity, setTempCity] = useState("");
  const [personalInterstsOptions, setPersonalInterstsOptions] = useState([]);
  const [selectedInterests, setSelectedInterests] = useState([]);
  const [selectedInterestsIndexes, setSelectedInterestsIndexes] = useState([]);
  const [likedUsersEmails, setLikedUsersEmails] = useState([]);
  const [emailsThatLikesMe, setEmailsThatLikesMe] = useState([]);
  const [currentImage, setCurrentImage] = useState("");
  const [matchDetails, setMatchDetails] = useState(null);

  localStorage.setItem("origin", JSON.stringify("map"));
  const currentEmail = JSON.parse(localStorage.getItem("current-email"));
  // const myDetails = useRecoilValue(myDetailsState);
  const myDetails = userToShow.userToShow;
  const {
    email,
    firstName,
    lastName,
    age,
    height,
    image,
    city,
    persoalText,
    percentage,
  } = myDetails;

  const nextImage = () => {
    const currentIndex = image.indexOf(currentImage);
    if (currentIndex < image.length - 1) {
      setCurrentImage(image[currentIndex + 1]);
    } else {
      setCurrentImage(myDetails.image[0]);
    }
  };

  useEffect(() => {
    // Fetch cities
    fetchCities();

    // Fetch personal interests
    makeAmoveUserServer.GetPersonalInterests().then((res) => {
      setPersonalInterstsOptions(res);

      // After personalInterstsOptions is set, update selectedInterests
      setSelectedInterests(getInterestDescByCodes(selectedInterestsIndexes));
    });

    // Set the initial image
    setCurrentImage(myDetails.image[0]);

    // Get likes for the current user
    getLikesByEmail();
  }, [myDetails]);

  // This effect updates selectedInterests whenever selectedInterestsIndexes or personalInterstsOptions change
  useEffect(() => {
    if (personalInterstsOptions.length > 0) {
      setSelectedInterests(getInterestDescByCodes(selectedInterestsIndexes));
    }
  }, [personalInterstsOptions, selectedInterestsIndexes]);

  useEffect(() => {
    const fetchData = async () => {
      const interests = await makeAmoveUserServer.GetPersonalInterests();
      setPersonalInterstsOptions(interests);

      const selectedIndexes =
        await makeAmoveUserServer.GetPersonalInterestsByEmail(email);
      setSelectedInterestsIndexes(selectedIndexes);
      console.log(selectedIndexes);
      setSelectedInterests(getInterestDescByCodes(selectedIndexes));
      console.log(selectedInterests);
      getLikesByEmail(myDetails.email);
      getEmailsThatLikesMe();
    };

    fetchData();
  }, [email, myDetails.email]);

  const getInterestDescByCodes = (codesArr) => {
    const temp = [];
    codesArr.forEach((index) => {
      personalInterstsOptions.forEach((option) => {
        if (option.interestCode === index) {
          temp.push(option.interestDesc);
        }
      });
    });
    console.log(temp);
    return temp;
  };

  const calculateAge = () => {
    const today = new Date();
    const birthDate = new Date(myDetails.birthday);
    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    return age;
  };

  const ageCalc = calculateAge();

  const fetchCities = async () => {
    try {
      const response = await axios.get(
        "https://data.gov.il/api/3/action/datastore_search?resource_id=b282b438-0066-47c6-b11f-8277b3f5a0dc&limit=2000"
      );
      const citiesData = response.data.result.records;
      const cityMap = {};
      citiesData.forEach((city) => {
        cityMap[city["תיאור ישוב"]] = city["סמל ישוב"];
      });
      setCityMap(cityMap);
      setTempCity(
        Object.entries(cityMap).find(
          ([key, val]) => val === parseInt(myDetails.city)
        )?.[0]
      );
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const handleLike = () => {
    console.log(currentEmail + " Likes " + myDetails.email);
    setLikedUsersEmails([...likedUsersEmails, myDetails.email]);
    console.log([...likedUsersEmails, myDetails.email]);

    makeAmoveUserServer
      .handleNewLike(currentEmail, myDetails.email, myDetails.currentPlace)
      .then((res) => {
        console.log(res);
        if (res === 2) {
          handleMatch();
        } else if (res === 1) {
        }
      })
      .catch((res) => console.log(res));
  };
  const handleUnlike = () => {
    console.log(currentEmail + " Unlikes " + myDetails.email);
  };

  const handleMatch = () => {
    console.log("Match");
    setMatchDetails(myDetails);
  };

  const getLikesByEmail = () => {
    makeAmoveUserServer.getMyLikesByEmail(currentEmail).then((res) => {
      let emailList = [];
      emailList = res.map((u) => u.secondEmail);
      setLikedUsersEmails(emailList);
      console.log(res);
      console.log(likedUsersEmails);
    });
  };

  const getEmailsThatLikesMe = () => {
    makeAmoveUserServer.getEmailsThatLikesMe(currentEmail).then((res) => {
      let emailList = [];
      emailList = res.map((u) => u.firstEmail);
      console.log("Emails that likes me: " + emailList);
      setEmailsThatLikesMe(emailList);
      console.log(res);
      console.log(emailsThatLikesMe);
    });
  };

  const getMatches = () => {
    makeAmoveMatchServer
      .getMatchesByEmail(currentEmail)
      .then((res) => console.log(res));
  };

  const pressLike = keyframes`
  0% {
    animation-timing-function: ease-out;
    transform: scale(1);
    transform-origin: center center;
  }
  10% {
    animation-timing-function: ease-in;
    transform: scale(0.91);
  }
  17% {
    animation-timing-function: ease-out;
    transform: scale(0.98);
  }
  33% {
    animation-timing-function: ease-in;
    transform: scale(0.87);
  }
  45% {
    animation-timing-function: ease-out;
    transform: scale(1);
  }
`;

  return (
    <div className="overlay">
      <div className="profile-container">
        <div
          className="pBackground"
          style={{
            // backgroundImage: `url(.${background})`,
            backgroundImage: `url(${
              import.meta.env.VITE_SERVER_IMAGE_SRC_URL
            }${currentImage})`,
          }}
          onClick={nextImage}
        >
          <FCHamburger />
        </div>
        <div className="bottomP">
          {/* <FCBackArrow color="white" /> */}
          <FCMatchScore score={Math.round(percentage)} />
          <h2>
            {firstName} {lastName}
          </h2>
          <p>
            <b>גיל: </b>
            {ageCalc}
          </p>
          <p>
            <b>גובה: </b>
            {height}
          </p>
          <p>
            <b>מאיפה: </b>
            {tempCity}
          </p>
          <p>
            <b>תחומי עניין: </b>
            {selectedInterests.join(", ")}
            <br />
            <br />
          </p>
          <p>
            <b>על עצמי: </b>
            <br />
            {persoalText}
          </p>
          {likedUsersEmails.includes(myDetails.email) ? (
            <FavoriteIcon
              fontSize="large"
              sx={{
                color: "#ffffff",
                display: "inline-block",
                margin: 5,
                "&:active": {
                  animation: `${pressLike} 2s linear 0s 1 normal forwards`,
                },
              }}
              onClick={handleUnlike}
            />
          ) : (
            <FavoriteBorderIcon
              fontSize="large"
              sx={{
                color: "#ffffff",
                margin: 5,
              }}
              onClick={handleLike}
            />
          )}
          {/* when viewed user is liked -> change display to "inline-block" */}
        </div>
      </div>

      {/* Conditional rendering of MatchDialog */}
      {matchDetails && (
        <FCMatchModal
          open
          details={matchDetails}
          onClose={() => setMatchDetails(null)}
        />
      )}
      {/* <AlertDialog open /> */}
    </div>
  );
}
