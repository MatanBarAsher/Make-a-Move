import React, { useEffect, useState } from "react";
import { makeAmoveUserServer } from "../services";

export default function FCMatchedUser({
  user,
  func,
  image,
  currentEmail,
  isFeedbacked,
  isContinueFeedbacked,
  id,
}) {
  const [score, setScore] = useState(0);

  useEffect(() => {
    console.log(currentEmail);
    console.log(user.email);
    if (currentEmail !== user.email) {
      {
        makeAmoveUserServer
          .getMatchScoreByEmails(currentEmail, user.email)
          .then((res) => setScore(Math.round(res)));
      }
    }
  }, []);

  return (
    <div onClick={() => func(user)} className="match" id={id}>
      <div style={{ position: "relative", width: 80 }}>
        <div
          className="profile-image"
          style={{
            backgroundImage: `url(${
              import.meta.env.VITE_SERVER_IMAGE_SRC_URL
            }${image})`,
            height: 60,
            width: 60,
            border: "4px solid white",
            borderRadius: "50%",
          }}
        >
          <div className="match-score-matchList">{score}%</div>
        </div>
      </div>
      <p>{user.firstName}</p>
      {!id ? (
        <div className="feedbacks-index">
          <span>משובים:</span>
          <span>1 {isFeedbacked ? " ✔️" : " ➖"}</span>
          <span>2{isContinueFeedbacked ? " ✔️" : " ➖"}</span>
        </div>
      ) : (
        <></>
      )}
    </div>
  );
}
