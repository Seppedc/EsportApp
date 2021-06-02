import React from 'react';
import { Text, View } from 'react-native';
import { useQuery, gql } from '@apollo/client';
import GlobalFeed from '../GlobalFeed';
import GlobalFollowed from '../GlobalFollowed';
const Followed = props => {
    return (
        <GlobalFollowed navigation={props.navigation} />
    );
};
Followed.navigationOptions = {
    title: 'Followed'
};
export default Followed;