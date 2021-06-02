import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert,TouchableOpacity  } from 'react-native';
import { MaterialCommunityIcons, MaterialIcons,Entypo,FontAwesome    } from '@expo/vector-icons';
import moment from "moment";

const userId = "84f9f434-16b3-452c-ce14-08d925ee7fb8"

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
    const { RefreshScreen} = item;
    if(item){
        console.log(item)
        return(
            <TouchableOpacity>
            <View style={styles.matchen} onPress={() => console.log('pres ')}>
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
                
                <Entypo name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowMatch(item.UserGameId);RefreshScreen}}/>
            </View>
        </TouchableOpacity>
        )
    }else{
        return(<Text>Nog geen gevolgde matchen</Text>)
    }
    
};
const _renderItemGames = ({ item }) => {
    const { RefreshScreen} = item;
    if(item){
        return(
            <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.Games} >
                <View style={styles.GamesGegevens}>
                    <Text style={styles.Naam}>{item.Naam}</Text>
                </View>
                <Entypo  style={styles.HeartGame} name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowGame(item.Id);RefreshScreen}}/>
            </View>
        </TouchableOpacity>
        )
    }else{
        return(<Text>Nog geen gevolgde games</Text>)
    }
    
};
const _renderItemTeams = ({ item }) => {
    const { RefreshScreen} = item;
    if(item){
        console.log(item)
        return(
            <TouchableOpacity onPress={() => console.log('pres ')}>
                <View style={styles.team} >
                    <Text style={styles.teamNaam}>{item.Naam}</Text>
                    <Entypo style={styles.HeartTeam} name="heart" size={24} color="black" onPress={()=>{PressHandelerUnFollowTeam(item.Id);RefreshScreen}}/>
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
            currentSelected:"",
            lastRefresh: Date(Date.now()).toString(),

        };
    }
    RefreshScreen() {
        this.setState({ lastRefresh: Date(Date.now()).toString() })
    }
    GetMatch(GameId,id){
        fetch("https://localhost:5001/api/Games/"+GameId)
        .then(response => response.json())
        .then((responseJson) => {
            responseJson.Datum = moment(responseJson.Datum).format('MMM Do YYYY h:mm:ss a');
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
                    element.Datum = moment(element.Datum).format('MMM Do YYYY h:mm:ss a');
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
                                RefreshScreen = {this.RefreshScreen}
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
                                RefreshScreen = {this.RefreshScreen}
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
                                RefreshScreen = {this.RefreshScreen}
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
});
  