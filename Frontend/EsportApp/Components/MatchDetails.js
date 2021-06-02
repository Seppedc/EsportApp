import React from 'react';
import { Text, ScrollView } from 'react-native';
import styled from 'styled-components/native';


const GameView = styled.ScrollView`
    padding: 10px;
`;
const Game = props => {
    console.log(props)
    return (
        <GameView>
            <Text>{props.game.title}</Text>
        </GameView>
    );
};
export default Game;