import React, { useState } from "react";
import FCProfileView from "./FCProfileView";
import FCBackArrow from "../components/FCBackArrow";
import FCNextArrow from "../components/FCNextArrow";

export default function FCCarousel(props) {
  const [index, setIndex] = useState(0);
  const [prevIndex, setPrevIndex] = useState(null);
  const [direction, setDirection] = useState(null);
  const [key, setKey] = useState(0); // Added to force re-render
  const usersList = props.users;

  const nextProfile = () => {
    setDirection("next");
    setPrevIndex(index);
    setKey((prevKey) => prevKey + 1); // Update key to force re-render
    if (index < usersList.length - 1) {
      setIndex(index + 1);
    } else {
      setIndex(0);
    }
  };

  const prevProfile = () => {
    setDirection("prev");
    setPrevIndex(index);
    setKey((prevKey) => prevKey + 1); // Update key to force re-render
    if (index === 0) {
      setIndex(usersList.length - 1);
    } else {
      setIndex(index - 1);
    }
  };

  const enterAnimationStyle = {
    animation:
      direction === "next"
        ? "swipeLeftEnter 0.5s ease-in-out"
        : "swipeRightEnter 0.5s ease-in-out",
    zIndex: 2,
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "100%",
  };

  const exitAnimationStyle = {
    animation:
      direction === "next"
        ? "swipeLeftExit 0.5s ease-in-out"
        : "swipeRightExit 0.5s ease-in-out",
    zIndex: 1,
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "100%",
  };

  return (
    <div
      style={{
        // border: "1px solid white",
        height: "100%",
        position: "relative",
        overflow: "hidden",
      }}
    >
      <div
        style={{
          width: "50px",
          height: "30px",
          position: "fixed",
          top: 120,
          right: 0,
          backgroundColor: "#efe1d1",
          color: "white",
          opacity: 0.8,
          borderRadius: 20,
          boxShadow: "3px 3px 4px rgba(0, 0, 0, 0.7)",
          zIndex: 3,
        }}
        onClick={nextProfile}
      >
        <FCNextArrow color="white" />
      </div>
      <div
        style={{
          width: "50px",
          height: "30px",
          position: "fixed",
          top: 120,
          left: 0,
          backgroundColor: "#efe1d1",
          color: "white",
          opacity: 0.8,
          borderRadius: 20,
          boxShadow: "3px 3px 4px rgba(0, 0, 0, 0.7)",
          zIndex: 3,
        }}
        onClick={prevProfile}
      >
        <FCBackArrow color="white" style={{ opacity: 1, color: "white" }} />
      </div>
      {prevIndex !== null && (
        <div key={`prev-${key}`} style={exitAnimationStyle}>
          <FCProfileView userToShow={usersList[prevIndex]} />
        </div>
      )}
      <div key={`current-${key}`} style={enterAnimationStyle}>
        <FCProfileView userToShow={usersList[index]} />
      </div>
    </div>
  );
}
