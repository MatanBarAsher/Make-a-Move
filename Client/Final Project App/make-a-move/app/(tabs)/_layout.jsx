import { View, Text, Image } from "react-native";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { Tabs, Redirect } from "expo-router";
import { bye } from "../../constants/icons";
import { Octicons } from "@expo/vector-icons";
// const TabIcon = (icon, color, name, focused) => {
//   <View>
//     <Image source={icon} />
//   </View>;
// };

const TabsLayout = () => {
  return (
    <>
      <Tabs>
        <Tabs.Screen
          name="home"
          options={{
            title: "Home",
            headerShown: "false",
            // tabBarIcon: ({ color, focused }) => (
            //   <TabIcon icon={bye} color={color} name="Home" focused={focused} />
            // ),
            tabBarIcon: ({ color }) => (
              <Octicons name="home" size={24} color={color} />
            ),
          }}
        />
      </Tabs>
    </>
  );
};

export default TabsLayout;
