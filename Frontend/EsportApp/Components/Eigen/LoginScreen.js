import React from 'react';
import { StyleSheet, Text, View,Button } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
function LoginScreen({navigation }) {
    return (
      <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
        <Text>Login Screen</Text>
        <Button
            title="Create Account"
            onPress={() => navigation.navigate('CreateAccount')}
            />
      </View>
    );
  }
  export default LoginScreen;
