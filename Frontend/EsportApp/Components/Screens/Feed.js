import React from 'react';
import { Text, View } from 'react-native';
import { useQuery, gql } from '@apollo/client';
import GameFeed from '../GameFeed';
import GlobalFeed from '../GlobalFeed';

const getGameFromApi = async () => {
    try {
      let response = await fetch(
        'https://localhost:5001/api/Games'
        );
      let json = await response.json();
      console.log(json);
      data= json.Games;
      return[]
    } catch (error) {
       console.error(error);
    }
};
const Feed = props => {
    return (
        <GlobalFeed navigation={props.navigation} />
    );
};
Feed.navigationOptions = {
    title: 'Feed'
};
export default Feed;