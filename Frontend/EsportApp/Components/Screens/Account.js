import React from 'react';
import { Text, View } from 'react-native';
const Account = () => {
    return (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
            <Text>My Account</Text>
        </View>
    );
};
Account.navigationOptions = {
    title: 'My Account'
};
export default Account;