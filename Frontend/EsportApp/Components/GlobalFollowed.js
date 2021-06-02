import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert,TouchableOpacity  } from 'react-native';
import { MaterialCommunityIcons, MaterialIcons,Entypo,FontAwesome    } from '@expo/vector-icons';

const userId = "5e1a26d5-8677-4903-ea84-08d925b7d737"

function PressHandelerUnFollowMatch(id){
    fetch("https://localhost:5001/api/UserGames/"+id,{
        method: 'DELETE',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
    })
    .catch(error => console.log(error));
}
function PressHandelerUnFollowGame(id){
    fetch("https://localhost:5001/api/UserGameTitles/"+id,{
        method: 'DELETE',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
    })
    .catch(error => console.log(error));
}
function PressHandelerUnFollowTeam(id){
    fetch("https://localhost:5001/api/UserTeams/"+id,{
        method: 'DELETE',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        }
    })
    .catch(error => console.log(error));
}
const _renderItemMatches = ({ item }) => {
    if(item){
        console.log(item)
        return(
            <TouchableOpacity>
                <View style={styles.item} onPress={() => console.log('pres ')}>
                <Text>{item.Datum}</Text>
                    <Text>{item.Teams[0]}</Text>
                    <Text>{item.Teams[1]}</Text>
                    <Text>{item.Tornooi}</Text>
                    <Text>{item.GameTitle}</Text>
                    <Text>{item.Score}0 : 0</Text>
                    <Entypo name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowMatch(item.UserGameId)}}/>
                </View>
            </TouchableOpacity>
        )
    }else{
        return(<Text>Nog geen gevolgde matchen</Text>)
    }
    
};
const _renderItemGames = ({ item }) => {
    if(item){
        return(
            <TouchableOpacity onPress={() => console.log('pres ')}>
                <View style={styles.item} >
                    <Text>{item.Naam}</Text>
                    <Text>{item.Uitgever}</Text>
                    <Entypo name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowGame(item.Id)}}/>
                </View>
            </TouchableOpacity>
        )
    }else{
        return(<Text>Nog geen gevolgde games</Text>)
    }
    
};
const _renderItemTeams = ({ item }) => {
    if(item){
        console.log(item)
        return(
            <TouchableOpacity onPress={() => console.log('pres ')}>
                <View style={styles.item} >
                    <Text>{item.Naam}</Text>
                    <Entypo name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowTeam(item.Id)}}/>
                </View>
            </TouchableOpacity>
        )
    }else{
        return(<Text>Nog geen gevolgde teams</Text>)
    }
    
};
const AllButtons = ( props ) => {
    const { GetDataMatchen,GetDataTeams,GetDataGames } = props;
    return(
        <View style={styles.item}>
            <Button
                title="Matchen"
                onPress={GetDataMatchen}
            />
            <Button
                title="Games"
                onPress={GetDataGames}
            />
            <Button
                title="Teams"
                onPress={GetDataTeams}
            />
        </View>
    )
};
class ApiContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            dataGames: [],
            dataTeams:[],
            dataMatchen:[],
            currentSelected:""

        };
    }
    GetMatch(GameId,id){
        fetch("https://localhost:5001/api/Games/"+GameId)
        .then(response => response.json())
        .then((responseJson) => {
            var test = responseJson;
            test["UserGameId"] = id;
            var joined = this.state.dataMatchen.concat(test);
            this.setState({ dataMatchen: joined })
        })
        .catch(error => console.log(error))
    };
    GetDataMatchen = () => {
        this.setState({
            loading: true,
            dataMatchen:[],
        })
        fetch("https://localhost:5001/api/UserGames/"+ userId +"/User")
            .then(response => response.json())
            .then((responseJson) => {
                responseJson.UserGames.forEach(element => {
                    this.GetMatch(element.GameId,element.Id);
                });
                this.setState({
                    loading: false,
                    currentSelected:"Matchen"
                })
            })
            .catch(error => console.log(error))
    }
    GetTeam(UserTeamId){
        fetch("https://localhost:5001/api/UserTeams/"+UserTeamId)
        .then(response => response.json())
        .then((responseJson) => {
            var joined = this.state.dataTeams.concat(responseJson);
            this.setState({ dataTeams: joined })
        })
        .catch(error => console.log(error))
    };
    GetDataTeams = () => {
        this.setState({
            loading: true,
            dataTeams:[],
        })
        fetch("https://localhost:5001/api/UserTeams")
            .then(response => response.json())
            .then((responseJson) => {
                responseJson.UserTeams.forEach(element => {
                    if(element.UserId == userId){
                        this.GetTeam(element.Id);
                    }
                });
                this.setState({
                    loading: false,
                    currentSelected:"Teams"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataGames = () => {
        this.setState({
            loading: true,
            dataGames:[],
        })
        fetch("https://localhost:5001/api/UserGameTitles")
            .then(response => response.json())
            .then((responseJson) => {
                responseJson.UserGameTitles.forEach(element => {
                    if(element.UserId == userId){
                        var joined = this.state.dataGames.concat(element);
                        this.setState({ dataGames: joined })
                    }
                });
                this.setState({
                    loading: false,
                    currentSelected:"Games"
                })
            })
            .catch(error => console.log(error))
    }
    componentDidMount(){
        this.GetDataMatchen();
    }
    render() {
        if((!this.state.dataGames)||(this.state.loading)){
            return(
                <Text>Loading...</Text>
            );
        }else{
            if(this.state.currentSelected=="Matchen"){
                if(this.state.dataMatchen.length==0){
                    console.log('no',this.state.dataMatchen)
                    return(
                        <View>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <Text>Nog geen gevolgde Matchen</Text> 
                        </View> 
                    )
                }else{
                    console.log('yes',this.state.dataMatchen)

                    return (
                        <SafeAreaView style={styles.container}>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <FlatList
                                data={this.state.dataMatchen}
                                keyExtractor={item => item.Id}
                                renderItem={_renderItemMatches}
                            />
                        </SafeAreaView>
                    );
                }
            }else if(this.state.currentSelected=="Teams"){
                console.log(this.state.dataTeams)
                if(this.state.dataTeams.length==0){
                    return(
                        <View>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <Text>Nog geen gevolgde Teams</Text> 
                        </View> 
                    )              
                }else{
                    return (
                        <SafeAreaView style={styles.container}>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <FlatList
                                data={this.state.dataTeams}
                                keyExtractor={item => item.Id}
                                renderItem={_renderItemTeams}
                                PressHandelerMatches={this.PressHandelerMatches}
    
                            />
                        </SafeAreaView>
                    );
                }
            }else{
                if(this.state.dataGames.length==0){
                    return(
                        <View>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <Text>Nog geen gevolgde Games</Text> 
                        </View> 
                    )
                }else{
                    return (
                        <SafeAreaView style={styles.container}>
                            <AllButtons GetDataMatchen={this.GetDataMatchen}
                                        GetDataTeams={this.GetDataTeams}
                                        GetDataGames={this.GetDataGames}
                            ></AllButtons>
                            <FlatList
                                data={this.state.dataGames}
                                keyExtractor={item => item.Id}
                                renderItem={_renderItemGames}
                            />
                        </SafeAreaView>
                    );
                }
            }
        }
    }
}

export default ApiContainer;
const styles = StyleSheet.create({
    container: {
      flex: 1,
      marginTop: StatusBar.currentHeight || 0,
    },
    item: {
      backgroundColor: 'lightgrey',
      padding: 20,
      marginVertical: 8,
      marginHorizontal: 16,
    },
    tornooi: {
      fontSize: 32,
    },
});
  