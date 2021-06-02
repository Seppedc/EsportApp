import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert,TouchableOpacity  } from 'react-native';
import { MaterialCommunityIcons, MaterialIcons,Entypo,FontAwesome    } from '@expo/vector-icons';
import moment from "moment";
const userId = "5e1a26d5-8677-4903-ea84-08d925b7d737"
function PressHandelerFollowMatch(id){
    fetch("https://localhost:5001/api/UserGames",{
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserId: userId,
            GameId: id
        })
    })
    .then(response => response.json())
    .catch(error => console.log(error));
}
function PressHandelerFollowGame(id){
    fetch("https://localhost:5001/api/UserGameTitles",{
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserId: userId,
            GameTitleId: id
        })
    })
    .then(response => response.json())
    .catch(error => console.log(error));
}
function PressHandelerFollowTeam(id){
    fetch("https://localhost:5001/api/UserTeams",{
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserId: userId,
            TeamId: id
        })
    })
    .then(response => response.json())
    .catch(error => console.log(error));
}
const _renderItemMatches = ({ item }) => {
    const { PressHandelerMatches} = item;
    console.log(item)
    return(
        <TouchableOpacity>
            <View style={styles.matchen} onPress={PressHandelerMatches}>
                <View>
                    <Text style={styles.matchenTextDatum}>{item.Datum}</Text>
                </View>
                <View style={styles.matchenTeam}>
                    <Text style={[styles.matchenText,styles.matchenTeam1]}>{item.Teams[0]}</Text>
                    <Text style={styles.matchenTextScore}>{item.Score} 0 - 0 </Text>
                    <Text style={[styles.matchenText,styles.matchenTeam2]}>{item.Teams[1]}</Text>
                </View>
                <View>
                    <Text style={styles.matchenInfo}>{item.Tornooi}</Text>
                    <Text style={styles.matchenInfo}>{item.Naam}</Text>
                </View>
                
                <Entypo name="heart" size={24} color="black" onPress={()=>{PressHandelerFollowMatch(item.Id)}}/>
            </View>
        </TouchableOpacity>
        
    )
};
const _renderItemGames = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.Games} >
                <View style={styles.GamesGegevens}>
                    <Text style={styles.Naam}>{item.Naam}</Text>
                    <Text style={styles.Uitgever}>{item.Uitgever}</Text>
                </View>
                <Entypo  style={styles.HeartGame} name="heart" size={24} color="black" onPress={()=>{PressHandelerFollowGame(item.Id)}}/>
            </View>
        </TouchableOpacity>
    )
};
const _renderItemTeams = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.team} >
                <Text style={styles.teamNaam}>{item.Naam}</Text>
                <Entypo style={styles.HeartTeam} name="heart" size={24} color="black" onPress={()=>{PressHandelerFollowTeam(item.Id)}}/>
            </View>
        </TouchableOpacity>
    )
};
const _renderItemTornooien = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.tornooi}>
                <View style={styles.tornooiGegevens}>
                    <Text style={styles.tornooiGegevensBoldNaam}>{item.Naam}</Text>
                    <Text style={styles.tornooiGegevensBold}>Organisator: </Text>
                    <Text style={styles.tornooiBeschrijving}>{item.Organisator}</Text>
                </View>
                <Text style={styles.tornooiBeschrijvingBold}>Beschrijving: </Text>
                <Text style={styles.tornooiBeschrijving}>{item.Beschrijving}</Text>
            </View>
        </TouchableOpacity>
    )
};
const AllButtons = ( props ) => {
    const { GetDataMatchen,GetDataTeams,GetDataGames,GetDataTornooien } = props;
    return(
        <View style={styles.buttons}>
            <Button style={styles.button}
                title="Matchen"
                onPress={GetDataMatchen}
                />
            <Button style={styles.button}
                title="Games"
                onPress={GetDataGames}
                />
            <Button style={styles.button}
                title="Teams"
                onPress={GetDataTeams}
                />
            <Button style={styles.button}
                title="Tornooien"
                onPress={GetDataTornooien}
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
            dataTornooien:[],
            currentSelected:"",
            followedGames:[],
            followedTeams:[],
            followedMatchen:[],
        };
    }
    GetFollowedDataGames=()=>{
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/UserGameTitles")
            .then(response => response.json())
            .then((responseJson) => {
                responseJson.UserGameTitles.forEach(element => {
                    if(element.UserId == userId){
                        let joined = this.state.followedGames.concat(element.GameTitleId);
                        this.setState({ followedGames: joined });
                    }
                });
            })
            .catch(error => console.log(error));
    }
    GetFollowedDataTeams=()=>{
        fetch("https://localhost:5001/api/UserTeams")
        .then(response => response.json())
        .then((responseJson) => {
            responseJson.UserTeams.forEach(element => {
                if(element.UserId == userId){
                    let joined = this.state.followedTeams.concat(element.TeamId);
                    this.setState({ followedTeams: joined })
                }
            });
    })
    .catch(error => console.log(error));}
    GetFollowedDataMatchen=()=>{
        fetch("https://localhost:5001/api/UserGames")
        .then(response => response.json())
        .then((responseJson) => {
            responseJson.UserGames.forEach(element => {
                element.Datum = moment(element.Datum).format('MMM Do YYYY h:mm:ss a');
                if(element.UserId == userId){
                    let joined = this.state.followedMatchen.concat(element.GameId);
                    this.setState({ followedMatchen: joined })
                }
            });
        })
        .catch(error => console.log(error));
        this.setState({loading:false});
    }
    GetFollowedData=()=>{
        this.GetFollowedDataMatchen();
        this.GetFollowedDataGames();
        this.GetFollowedDataTeams();
    }
    GetDataMatchen = () => {
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Games")
            .then(response => response.json())
            .then((responseJson) => {
                responseJson.Games.forEach(element=>{
                    element.Datum = moment(element.Datum).format('MMM Do YYYY h:mm:ss a');
                })
                this.setState({
                    loading: false,
                    dataMatchen: responseJson,
                    currentSelected:"Matchen"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataTeams = () => {
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Teams")
            .then(response => response.json())
            .then((responseJson) => {
                this.setState({
                    loading: false,
                    dataTeams: responseJson,
                    currentSelected:"Teams"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataGames = () => {
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/GameTitles")
            .then(response => response.json())
            .then((responseJson) => {
                this.setState({
                    loading: false,
                    dataGames: responseJson,
                    currentSelected:"Games"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataTornooien = () => {
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Tornooien")
            .then(response => response.json())
            .then((responseJson) => {
                this.setState({
                    loading: false,
                    dataTornooien: responseJson,
                    currentSelected:"Tornooien"

                })
            })
            .catch(error => console.log(error))
    }
    PressHandelerMatches=()=>{
        console.log('ins')
        this.props.navigation.navigate('MatchDetails');
    }
    componentDidMount(){
        this.GetDataMatchen();
        this.GetFollowedData();
    }
    render() {
        if((!this.state.dataGames)||(this.state.loading)){
            return(
                <Text>Loading...</Text>
            );
        }else{
            if(this.state.currentSelected=="Matchen"){
                return (
                    <SafeAreaView style={styles.container}>
                        <AllButtons GetDataMatchen={this.GetDataMatchen}
                                    GetDataTeams={this.GetDataTeams}
                                    GetDataGames={this.GetDataGames}
                                    GetDataTornooien={this.GetDataTornooien}
                        ></AllButtons>
                        <FlatList
                            data={this.state.dataMatchen.Games}
                            keyExtractor={item => item.Id}
                            renderItem={_renderItemMatches}
                            FollowedMatchen = {this.state.followedMatchen}

                        />
                    </SafeAreaView>
                );
            }else if(this.state.currentSelected=="Teams")
            {
                return (
                    <SafeAreaView style={styles.container}>
                        <AllButtons GetDataMatchen={this.GetDataMatchen}
                                    GetDataTeams={this.GetDataTeams}
                                    GetDataGames={this.GetDataGames}
                                    GetDataTornooien={this.GetDataTornooien}
                        ></AllButtons>
                        <FlatList
                            data={this.state.dataTeams.Teams}
                            keyExtractor={item => item.Id}
                            renderItem={_renderItemTeams}
                            PressHandelerMatches={this.PressHandelerMatches}
                            FollowedTeams = {this.state.followedTeams}
                        />
                    </SafeAreaView>
                );
            }else if(this.state.currentSelected=="Games"){
                return (
                    <SafeAreaView style={styles.container}>
                        <AllButtons GetDataMatchen={this.GetDataMatchen}
                                    GetDataTeams={this.GetDataTeams}
                                    GetDataGames={this.GetDataGames}
                                    GetDataTornooien={this.GetDataTornooien}
                        ></AllButtons>
                        <FlatList
                            data={this.state.dataGames.GameTitles}
                            keyExtractor={item => item.Id}
                            renderItem={_renderItemGames}
                            FollowedGames = {this.state.followedGames}

                        />
                    </SafeAreaView>
                );
            }else{
                return (
                    <SafeAreaView  style={styles.container}>
                        <AllButtons GetDataMatchen={this.GetDataMatchen}
                                    GetDataTeams={this.GetDataTeams}
                                    GetDataGames={this.GetDataGames}
                                    GetDataTornooien={this.GetDataTornooien}
                        ></AllButtons>
                        <FlatList
                            data={this.state.dataTornooien.Tornooien}
                            keyExtractor={item => item.Id}
                            renderItem={_renderItemTornooien}
                        />
                    </SafeAreaView>
                );
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
    buttons:{
        padding:0,
        margin:5,
        marginTop:10,
        flexDirection:'row',
        justifyContent: 'space-around',
        
       alignItems:'center',
    },
    button:{
        marginTop: 20,
        width: 150,
        height: 150,
        justifyContent: 'center',
        alignItems: 'center',
        padding: 10,
        borderRadius: 100,
        backgroundColor: '#ccc',
    },
    matchen:{
        backgroundColor: 'lightgrey',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
        alignItems:'center',
    },
    matchenText:{
        fontSize:18,
        width:110,
        textAlign:'center'
    },
    matchenTextDatum:{
        fontSize:18,
        fontWeight: "bold",
    },
    matchenTextScore:{
        fontSize:18,
        fontWeight: "bold",
    },
    matchenTeam:{
        flexDirection:'row',
        justifyContent:'center',
        marginTop:10,

    },
    matchenTeam1:{
        textAlign:'right',
    },
    matchenTeam2:{
        textAlign:'left',
    },
    matchenInfo:{
        marginTop:10,
        textAlign:'center'
    },


    Games:{
        fontSize:18,
        backgroundColor: 'lightgrey',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
        flexDirection:'row',

    },
    Naam:{
        fontWeight: "bold",
    },
    HeartGame:{
        alignSelf:"center"
    },
    GamesGegevens:{
        alignContent:'column',
        width:"90%",

    },


    team:{
        fontSize:18,
        backgroundColor: 'lightgrey',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
        flexDirection:'row',
        alignItems:"flex-end"
        
    },
    teamNaam:{
        fontWeight: "bold",
        alignSelf:"center",
        width:"90%"
    },
    HeartTeam:{
        alignSelf:"center",
    },


    tornooi:{
        fontSize:18,
        backgroundColor: 'lightgrey',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
    },
    tonooiBeschrijving:{
        fontWeight:"normal"
    },
    tornooiBeschrijvingBold:{
        fontWeight: "bold",
    },
    tornooiGegevens:{
        fontWeight:"normal"

    },
    tornooiGegevensBold:{
        alignContent:'column',
        width:"90%",
        fontWeight: "bold",
    },
    tornooiGegevensBoldNaam:{
        fontSize:22,
    },
});
  