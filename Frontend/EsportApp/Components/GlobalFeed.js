import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert,TouchableOpacity  } from 'react-native';


const _renderItemMatches = ({ item }) => {
    console.log(item)
    const { PressHandelerMatches } = item;
    return(
        <TouchableOpacity>
            <View style={styles.item} onPress={PressHandelerMatches}>
                <Text>{item.Datum}</Text>
                <Text>{item.Teams[0]}</Text>
                <Text>{item.Teams[1]}</Text>
                <Text>{item.Tornooi}</Text>
                <Text>{item.GameTitle}</Text>
                <Text>{item.Score}0 : 0</Text>
            </View>
        </TouchableOpacity>
        
    )
};
const _renderItemGames = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.item} >
                <Text>{item.Naam}</Text>
                <Text>{item.Uitgever}</Text>
            </View>
        </TouchableOpacity>
    )
};
const _renderItemTeams = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.item} >
                <Text>{item.Naam}</Text>
            </View>
        </TouchableOpacity>
    )
};
const _renderItemTornooien = ({ item }) => {
    
    return(
        <TouchableOpacity onPress={() => console.log('pres ')}>
            <View style={styles.item} >
                <Text>{item.Naam}</Text>
                <Text>{item.Organisator}</Text>
                <Text>{item.Beschrijving}</Text>
            </View>
        </TouchableOpacity>
    )
};
const AllButtons = ( props ) => {
    const { GetDataMatchen,GetDataTeams,GetDataGames,GetDataTornooien } = props;
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
            <Button
                title="Tornooien"
                onPress={GetDataTornooien}
                />
        </View>
    )
};
const Teams = ({ teams }) => (
    <View style={styles.item}>
      <Text style={styles.tornooi}>{Tornooi}</Text>
    </View>
);
class ApiContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            dataGames: [],
            dataTeams:[],
            dataMatchen:[],
            dataTornooien:[],
            currentSelected:""
        };
    }
    GetDataMatchen = () => {
        console.log('in')
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Games")
            .then(response => response.json())
            .then((responseJson) => {
                console.log('getting data from fetch', responseJson)
                this.setState({
                    loading: false,
                    dataMatchen: responseJson,
                    currentSelected:"Matchen"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataTeams = () => {
        console.log('in')
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Teams")
            .then(response => response.json())
            .then((responseJson) => {
                console.log('getting data from fetch', responseJson)
                this.setState({
                    loading: false,
                    dataTeams: responseJson,
                    currentSelected:"Teams"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataGames = () => {
        console.log('in')
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/GameTitles")
            .then(response => response.json())
            .then((responseJson) => {
                console.log('getting data from fetch', responseJson)
                this.setState({
                    loading: false,
                    dataGames: responseJson,
                    currentSelected:"Games"
                })
            })
            .catch(error => console.log(error))
    }
    GetDataTornooien = () => {
        console.log('in')
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Tornooien")
            .then(response => response.json())
            .then((responseJson) => {
                console.log('getting data from fetch', responseJson)
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
                        />
                    </SafeAreaView>
                );
            }else if(this.state.currentSelected=="Teams"){console.log(this.state.dataTeams);
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
                        />
                    </SafeAreaView>
                );
            }else{
                return (
                    <SafeAreaView style={styles.container}>
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
      backgroundColor: '#f9c2ff',
      padding: 20,
      marginVertical: 8,
      marginHorizontal: 16,
    },
    tornooi: {
      fontSize: 32,
    },
});
  