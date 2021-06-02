import React from 'react';
import { Text, View } from 'react-native';
const Search = () => {
    return (
        <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
            <Text>Coming Soon...</Text>
        </View>
    );
};
//screen title
Search.navigationOptions = {
    title: 'Search'
};
export default Search;