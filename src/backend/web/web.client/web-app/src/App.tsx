import * as React from 'react';
import './App.css';

import logo from './logo.svg';
// import { RouteComponentProps } from 'react-router';
// import { ReactChild } from 'react';

interface Props {
}

class App extends React.PureComponent<Props> {
  public render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to React</h1>
        </header>
        <p className="App-intro">
          To get started, edit <code>src/App.tsx</code> and save to reload.
        </p>
      </div>
    );
  }

  // private requireLoggedInUser(contentProvider: (props: RouteComponentProps<any>) => ReactChild) {
  //   return (props: RouteComponentProps<any>) => this.props.loggedIn ? contentProvider(props) : (
  //     <>
  //       <div>Login reuquired</div>
  //     </>)
  // }
}

export default App;
