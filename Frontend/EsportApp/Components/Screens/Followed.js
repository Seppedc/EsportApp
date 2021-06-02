import React from 'react';
import { Text, View,Button } from 'react-native';
const Followed = (props ) => {
    return (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
            <Text>My Followed </Text>
            <Button
                title="Bekijk Wedstrijd"
                onPress={() => props.navigation.navigate('Match')}
            />
        </View>
    );
};
Followed.navigationOptions = {
    title: 'Followed'
};
export default Followed;