import React, { useEffect, useState } from "react";
import FCCustomX from "../../../components/FCCustomX";
import { useNavigate } from "react-router";

import {
  makeAmoveMatchServer,
  makeAmoveUserServer,
  makeAmoveFeedbackServer,
} from "../../../services";
import FCMatchedUser from "../../../components/FCMatchedUser";
import { MatchDialogError2 } from "./Dialog/MatchDialogError2";
import { MatchDialogToContinue } from "./Dialog/MatchDialogToContinue";

export default function FCMatchList() {
  const navigate = useNavigate();
  const [matchedUsers, setMatchedUsers] = useState([]);
  const [matchedUsersDetails, setMatchedUsersDetails] = useState([]);
  const [showMatchModal2, setShowMatchModal2] = useState(false);
  const [showMatchModalContinue, setShowMatchModalContinue] = useState(false);
  const [feedbackMap, setFeedbackMap] = useState({});
  const [continueFeedbackMap, setContinueFeedbackMap] = useState({});

  const currentEmail = JSON.parse(localStorage.getItem("current-email"));

  useEffect(() => {
    getMatches();
    getFeedbacks();
    getContinueFeedbacks();
  }, []);

  useEffect(() => {
    if (matchedUsers.length > 0) {
      GetMatchedUsersDetails();
    }
  }, [matchedUsers]);

  useEffect(() => {
    checkFeedbackStatus();
  }, [matchedUsersDetails]);

  const getMatches = async () => {
    try {
      const res = await makeAmoveMatchServer.getMatchesByEmail(currentEmail);
      setMatchedUsers(res);
    } catch (error) {
      console.error("Error fetching matches:", error);
    }
  };

  const getFeedbacks = () => {
    makeAmoveFeedbackServer
      .getFeedbacksByEmail(currentEmail)
      .then((res) => setFeedbackMap(mapFeedbacks(res)));
  };

  const getContinueFeedbacks = () => {
    makeAmoveFeedbackServer
      .getContinueFeedbacksByEmail(currentEmail)
      .then((res) => setContinueFeedbackMap(mapFeedbacks(res)));
  };

  const GetMatchedUsersDetails = async () => {
    try {
      const userDetailPromises = matchedUsers.map(async (u) => {
        const email =
          u.secondemail === currentEmail ? u.firstemail : u.secondemail;
        const userDetails = await makeAmoveUserServer.GetUserNoPasswordByEmail(
          email
        );
        return { ...userDetails, matchNum: u.matchNum }; // Combine user details with matchNum
      });

      const usersDetails = await Promise.all(userDetailPromises);
      setMatchedUsersDetails(usersDetails);
    } catch (error) {
      console.error("Error fetching user details:", error);
    }
  };

  const mapFeedbacks = (feedbacks) => {
    return feedbacks.reduce((map, feedback) => {
      map[feedback.matchId] = true;
      return map;
    }, {});
  };

  const handleMatchClick = (clickedUser) => {
    localStorage.setItem("matched-user", JSON.stringify(clickedUser));

    if (
      feedbackMap[clickedUser.matchNum] &&
      continueFeedbackMap[clickedUser.matchNum]
    ) {
      setShowMatchModal2(true);
      console.log("1 + 2");
    } else if (feedbackMap[clickedUser.matchNum]) {
      setShowMatchModalContinue(true);
      // navigate("/feedbackContinue");
    } else {
      navigate("/feedback");
    }
  };

  const checkFeedbackStatus = () => {
    matchedUsersDetails.forEach((u) => {
      setFeedbackMap((prev) => ({
        ...prev,
        [u.matchNum]: feedbackMap[u.matchNum] || false,
      }));

      setContinueFeedbackMap((prev) => ({
        ...prev,
        [u.matchNum]: continueFeedbackMap[u.matchNum] || false,
      }));
    });
  };

  return (
    <>
      <span onClick={() => navigate("/sideMenu")}>
        <FCCustomX color="white" />
      </span>
      <MatchDialogError2
        open={showMatchModal2}
        setClose={() => {
          setShowMatchModal2(false);
        }}
      />
      <MatchDialogToContinue
        open={showMatchModalContinue}
        setClose={() => {
          setShowMatchModalContinue(false);
          navigate("/feedbackContinue");
        }}
        setCloseCancel={() => {
          setShowMatchModalContinue(false);
        }}
      />
      <div className="matches-container">
        <h1 style={{ flex: "100%" }}>התאמות</h1>
        {matchedUsersDetails.map((u, index) => (
          <FCMatchedUser
            key={index}
            user={u}
            func={handleMatchClick}
            image={u.image[0]}
            currentEmail={currentEmail}
            isFeedbacked={!!feedbackMap[u.matchNum]}
            isContinueFeedbacked={!!continueFeedbackMap[u.matchNum]}
          />
        ))}
      </div>
    </>
  );
}
