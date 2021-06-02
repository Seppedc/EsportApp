import React from 'react';
import { Text, View } from 'react-native';
import Game from '../Game';

let uid = '932f27c0-f58b-47f7-df6c-08d9253c2b1e';
const getGameFromApi = async () => {
    try {
      let response = await fetch(
        'https://localhost:5001/api/Games/'+uid
      );
      let json = await response.json();
      console.log(json);
      return json.Games;
    } catch (error) {
       console.error(error);
    }
};

const GameScreen = props => {
    console.log(props);
    const id = props.navigation.getParam('id');
    return (
        <View style={{ padding: 10 }}>
            <Text>This is a Game Screen... and game id is {id}.</Text>
        </View>
    );
};
export default GameScreen;