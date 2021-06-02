import React from 'react';
import { Text, View } from 'react-native';
import { useQuery, gql } from '@apollo/client';
import GameFeed from '../GameFeed';
import GlobalFeed from '../GlobalFeed';

const Feed = props => {
    return (
        <GlobalFeed navigation={props.navigation} />
    );
};
Feed.navigationOptions = {
    title: 'Feed'
};
export default Feed;