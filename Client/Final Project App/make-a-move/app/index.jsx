import React from "react";
import { ScrollView, Text, View, StyleSheet } from "react-native";
import { Image } from "react-native";
import { LinearGradient } from "expo-linear-gradient";
import { SafeAreaView } from "react-native-safe-area-context";
import { StatusBar } from "expo-status-bar";
import { Redirect, router } from "expo-router";
import FCCustomBtn from "../components/FCCustomBtn";

export default function App() {
  return (
    <LinearGradient
      colors={["#5D3587", "#3F2E3E", "#331D2C"]}
      style={styles.gradient}
    >
      <SafeAreaView style={styles.container}>
        <ScrollView contentContainerStyle={styles.scrollViewContent}>
          <View style={styles.content}>
            <Image
              source={require("make-a-move/assets/images/Logo.png")}
              style={styles.logo}
              resizeMode="contain"
            />
            <Text style={styles.heading}>Location based dating app</Text>
            <FCCustomBtn
              title="התחברות"
              // handlePress={() => router.push("/sign-in")}
            />
            <FCCustomBtn title="הרשמה" handlePress={() => {}} />
          </View>
        </ScrollView>
        <StatusBar style="light" />
      </SafeAreaView>
    </LinearGradient>
  );
}

const styles = StyleSheet.create({
  gradient: {
    flex: 1,
  },
  container: {
    flex: 1,
  },
  scrollViewContent: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  content: {
    width: "100%",
    alignItems: "center",
  },
  logo: {
    width: 130,
    height: 84,
    marginBottom: 20,
  },
  heading: {
    fontSize: 20,
    color: "#fff",
    fontFamily: "Heebo",
    marginBottom: 20,
  },
});
