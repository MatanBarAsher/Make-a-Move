import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { FCMultiSelect } from "../../../components";
import FCCustomBtn from "../../../components/FCCustomBtn";
import FCCustomTxtInp from "../../../components/FCCustomTxtInp";
import { FCLoad } from "../../../loading/FCLoad";
import { FeedbackSuccessDialog } from "./Dialog/FeedbackSuccessDialog";
import { FeedbackErrorDialog } from "./Dialog/FeedbackErrorDialog";
import {
  makeAmoveFeedbackServer,
  makeAmoveUserServer,
} from "../../../services";
import { Feedback, Margin } from "@mui/icons-material";
import AutoCompleteWithAddOption from "../../../components/AutoCompleteWithAddOption";
import FCMatchedUser from "../../../components/FCMatchedUser";
import FCCustomX from "../../../components/FCCustomX";

export const FCFeedback = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [friends, setFriends] = useState([]);
  const navigate = useNavigate();

  console.log(JSON.parse(localStorage.getItem("matched-user")));
  const matchedUser = JSON.parse(localStorage.getItem("matched-user"));
  const currentEmail = JSON.parse(localStorage.getItem("current-email"));

  useEffect(() => {
    getFriends();
  }, [isLoading]);

  useEffect(() => {
    console.log(friends);
  }, [friends]);

  const getFriends = async () => {
    try {
      const res = await makeAmoveUserServer
        .getUserFriendsByEmail(
          JSON.parse(localStorage.getItem("current-email"))
        )
        .then((res) => res)
        .catch((res) => console.log(res));
      console.log(res);
      let res1 = [];
      res.forEach((r) => res1.push({ value: r, label: r }));
      console.log(res1);
      setFriends(res1);
    } catch (error) {
      console.error("Error fetching matches:", error);
    }
  };

  var options1 = [
    { label: "1", id: 1 },
    { label: "2", id: 2 },
    { label: "3", id: 3 },
    { label: "4", id: 4 },
    { label: "5", id: 5 },
  ];

  const [option1, setOption1] = useState(null);
  const handleOption1Creation = (id) => {
    setOption1(id);
    // updateFeedbackData("Q1", id);
  };

  var options2 = [
    { label: "1", id: 1 },
    { label: "2", id: 2 },
    { label: "3", id: 3 },
    { label: "4", id: 4 },
    { label: "5", id: 5 },
  ];

  const [option2, setOption2] = useState(null);
  const handleOption2Creation = (id) => {
    setOption2(id);
    // updateFeedbackData("Q1", id);
  };

  var options3 = [
    { label: "1", id: 1 },
    { label: "2", id: 2 },
    { label: "3", id: 3 },
    { label: "4", id: 4 },
    { label: "5", id: 5 },
  ];

  const [option3, setOption3] = useState(null);
  const handleOption3Creation = (id) => {
    setOption3(id);
    // updateFeedbackData("Q1", id);
  };

  var options4 = [
    { label: "1", id: 1 },
    { label: "2", id: 2 },
    { label: "3", id: 3 },
    { label: "4", id: 4 },
    { label: "5", id: 5 },
  ];

  const [option4, setOption4] = useState(null);
  const handleOption4Creation = (id) => {
    setOption4(id);
    // updateFeedbackData("Q1", id);
  };

  const [friendName, setFriendName] = useState("");
  const handleFriendCreation = (name) => {
    setFriendName(name);
    // updateFeedbackData("Q1", id);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    console.log(option1);
    console.log(option2);
    console.log(option3);
    console.log(option4);
    console.log(friendName);
    const feebackData = {
      matchId: matchedUser.matchNum,
      q11: option1,
      q21: option2,
      q31: option3,
      q41: option4,
      name: friendName,
      email: JSON.parse(localStorage.getItem("current-email")),
    };

    console.log(feebackData);
    //go to server with precerencesData as prop
    try {
      const response = await makeAmoveFeedbackServer.createFeedback(
        feebackData
      );
      if (response) {
        setShowSuccessModal(true);
        console.log("success");
        console.log(response);
      } else {
        console.log("error");
        setShowErrorModal(true);
      }
    } catch (error) {
      console.error("Error create feedback:", error);
      setShowErrorModal(true);
    } finally {
      setIsLoading(false); // Set loading to false after the API call completes
    }
  };
  return (
    <span>
      {isLoading && <FCLoad />}

      {!isLoading && (
        <>
          <span onClick={() => navigate("/matches")}>
            <FCCustomX />
          </span>
          <FeedbackSuccessDialog
            open={showSuccessModal}
            setClose={() => {
              setShowSuccessModal(false);
              navigate("/matches");
            }}
          />
          <FeedbackErrorDialog
            open={showErrorModal}
            setClose={() => {
              setShowErrorModal(false);
            }}
          />
          <h1 className="pref-h1">משוב</h1>
          <FCMatchedUser
            id="match-in-feedback"
            user={matchedUser}
            image={matchedUser.image[0]}
            currentEmail={currentEmail}
          />
          <form>
            <h3>דרג/י את מידת ההסכמה שלך:</h3>
            <p className="feedback-p">
              <b>{matchedUser.firstName}</b> בעל מאפיינים דומים למה שאני מחפש/ת:
            </p>
            <div className="gender-inp">
              {options1.map((o1) => (
                <span key={o1.id}>
                  <input
                    checked={option1 === o1.id}
                    id={"option1_" + o1.id}
                    type="radio"
                    value={o1.id}
                    onClick={() => handleOption1Creation(o1.id)}
                  />
                  <label htmlFor={"option1_" + o1.id}>{o1.label}</label>
                </span>
              ))}
            </div>
            <p className="feedback-p">
              התמונות של <b>{matchedUser.firstName}</b> תואמות למציאות:
            </p>
            <div className="gender-inp">
              {options2.map((o2) => (
                <span key={o2.id}>
                  <input
                    checked={option2 === o2.id}
                    id={"option2_" + o2.id}
                    type="radio"
                    value={o2.id}
                    onClick={() => handleOption2Creation(o2.id)}
                  />
                  <label htmlFor={"option2_" + o2.id}>{o2.label}</label>
                </span>
              ))}
            </div>
            <p className="feedback-p">
              תחומי העניין ששיתפ/ת עזרו לי לפתח עם{" "}
              <b>{matchedUser.firstName}</b> שיחה:
            </p>
            <div className="gender-inp">
              {options3.map((o3) => (
                <span key={o3.id}>
                  <input
                    checked={option3 === o3.id}
                    id={"option3_" + o3.id}
                    type="radio"
                    value={o3.id}
                    onClick={() => handleOption3Creation(o3.id)}
                  />
                  <label htmlFor={"option3_" + o3.id}>{o3.label}</label>
                </span>
              ))}
            </div>

            <p className="feedback-p">
              הייתי רוצה להיפגש עם <b>{matchedUser.firstName}</b> שוב:
            </p>
            <div className="gender-inp">
              {options4.map((o4) => (
                <span key={o4.id}>
                  <input
                    checked={option4 === o4.id}
                    id={"option4_" + o4.id}
                    type="radio"
                    value={o4.id}
                    onClick={() => handleOption4Creation(o4.id)}
                  />
                  <label htmlFor={"option4_" + o4.id}>{o4.label}</label>
                </span>
              ))}
            </div>
            <p className="feedback-p">עם מי בילית היום?</p>

            <AutoCompleteWithAddOption
              friends={friends}
              onValueChange={handleFriendCreation}
            />
            <div
              style={{
                display: "flex",
                flexDirection: "row-reverse",
                justifyContent: "center",
                width: "25rem",
              }}
            >
              <FCCustomBtn
                style={{ width: "15rem", color: "black", margin: "30px 0" }}
                title={"סיום"}
                // type="submit"
                onClick={handleSubmit}
              />
            </div>
          </form>
        </>
      )}
    </span>
  );
};
