import React from 'react';
import Screens from './Screens';
import getEnvVars from '../config';
import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client';

const Main = () => {
  return (
    <Screens />
  );
};
export default Main;