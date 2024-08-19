import { TouchableOpacity, View, Text, StyleSheet } from "react-native";
import React from "react";

const FCCustomBtn = ({ title, handlePress }) => {
  return (
    <TouchableOpacity
      style={styles.button}
      onPress={handlePress}
      activeOpacity={0.7}
    >
      <Text style={styles.text}>{title}</Text>
    </TouchableOpacity>
  );
};
const styles = StyleSheet.create({
  button: {
    backgroundColor: "#fff",
    padding: 10,
    marginTop: 21,
    borderRadius: 16,
    width: 280,
    minHeight: 55,
    alignItems: "center",
    justifyContent: "center",
  },
  text: {
    color: "#000",
    fontFamily: "Inter",
    fontSize: 16,
    textAlign: "center",
  },
});

export default FCCustomBtn;
