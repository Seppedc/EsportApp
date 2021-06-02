import React, { Component } from 'react';
import { SafeAreaView, View, FlatList, StyleSheet, Text, StatusBar,ActivityIndicator,Button,Alert  } from 'react-native';


const _renderItem = ({ item }) => (
    
    <View style={styles.item}>
      <Text>{item.Datum}</Text>
      <Text>{item.Teams[0]}</Text>
      <Text>{item.Teams[1]}</Text>
      <Text>{item.Tornooi}</Text>
      <Text>{item.GameTitle}</Text>
      <Text>{item.Score}0 : 0</Text>
      
    </View>
);

const AllButtons = ({ item }) => (
    <View style={styles.item}>
      <Button
          title="Matchen"
          onPress={() => Alert.alert('Games button pressed')}
        />
        <Button
          title="Games"
          onPress={() => Alert.alert('Games button pressed')}
        />
      <Button
          title="Teams"
          onPress={() => Alert.alert('Teams button pressed')}
        />
      <Button
          title="Tornooien"
          onPress={() => Alert.alert('Tornooien button pressed')}
        />
      
    </View>
);
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
        };
    }
    GetData = () => {
        this.setState({
            loading: true,
        })
        fetch("https://localhost:5001/api/Games")
            .then(response => response.json())
            .then((responseJson) => {
                console.log('getting data from fetch', responseJson)
                this.setState({
                    loading: false,
                    dataGames: responseJson
                })
            })
            .catch(error => console.log(error))
    }
    componentDidMount(){
        this.GetData();
    }
    render() {
        if((!this.state.dataGames)||(this.state.loading)){
            return(
                <Text>Loading...</Text>
            );
        }else{
            return (
                <SafeAreaView style={styles.container}>
                    <AllButtons></AllButtons>
                    <FlatList
                        data={this.state.dataGames.Games}
                        keyExtractor={item => item.Id}
                        renderItem={_renderItem}
                    />
                </SafeAreaView>
            );
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
  