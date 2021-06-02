import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert,TouchableOpacity  } from 'react-native';


const _renderItemMatches = ({ item }) => {
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
    GetDataMatchen = () => {
        console.log('in')
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/UserGames")
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
                            data={this.state.dataMatchen.UserGames}
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
            }else{
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
  