import React from 'react';
import { createBottomTabNavigator } from 'react-navigation-tabs';
import Feed from './Feed';
import Followed from '../Screens/Followed';
import Account from '../Screens/Account';
import Search from '../Screens/Search';
import { MaterialCommunityIcons, MaterialIcons,Entypo,FontAwesome    } from '@expo/vector-icons';
import { createStackNavigator } from 'react-navigation-stack';
import GameScreen from '../Remove/Screens/game';
import { createAppContainer, createSwitchNavigator } from 'react-navigation';
import AuthLoading from '../Remove/Screens/authloading';
import SignIn from '../Remove/Screens/signin';
import Settings from '../Remove/Screens/settings';
import MatchDetails from '../MatchDetails'
//stacked navivator
const FeedStack = createStackNavigator({
    Feed: Feed,
    Game: GameScreen
});
const FollowedStack = createStackNavigator({
    Followed: Followed,
    Game: GameScreen
});
const SearchStack = createStackNavigator({
    Search: Search,
    Game: GameScreen
});
const AccountStack = createStackNavigator({
    Account: Account,
    Game: GameScreen
});
const AuthStack = createStackNavigator({
    SignIn: SignIn
});
const SettingsStack = createStackNavigator({
    Settings: Settings
});

const TabNavigator = createBottomTabNavigator({
    FeedScreen: {
        screen: FeedStack,
        navigationOptions: {
            tabBarLabel: 'Feed',
            tabBarIcon: () => (
            <Entypo name="calendar" size={24} color="black" />            )
        }
    },
    SearchScreen: {
        screen: SearchStack,
        navigationOptions: {
            tabBarLabel: 'Search',
            tabBarIcon: () => (
                <FontAwesome name="search" size={24} color="black" />
            )
        }
    },
    FollowedScreen: {
        screen: FollowedStack,
        navigationOptions: {
            tabBarLabel: 'Followed',
            tabBarIcon: () => (
                <Entypo name="heart" size={24} color="black" />
            )
        }
    },
    AccountScreen: {
        screen: AccountStack,
        navigationOptions: {
            tabBarLabel: 'My Account',
            tabBarIcon: () => (
                <MaterialIcons name="account-circle" size={24} color="black" />
            )
        }
    }
});
const SwitchNavigator = createSwitchNavigator(
    {
        //AuthLoading: AuthLoading,
        //Auth: AuthStack,
        App: TabNavigator,
        MatchDetails:MatchDetails,
    },
    {
        //initialRouteName: 'AuthLoading'
    }
);
//export default createAppContainer(TabNavigator);
export default createAppContainer(SwitchNavigator);