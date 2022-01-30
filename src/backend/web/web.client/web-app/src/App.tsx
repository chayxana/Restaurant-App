import * as React from 'react';
import './App.css';

import { Routes, Route } from 'react-router';
import FoodsListContainer from './pages/food/FoodsListContainer';
import Header from './pages/header/HeadContainer';
// import { RouteComponentProps } from 'react-router';
// import { ReactChild } from 'react';



class App extends React.PureComponent {
  public render() {
    return (
      <div className="app">
        <Header />
        <div className="app-body">
          <div className="content">
            <Routes>
              <Route path="/foods/" element={FoodsListContainer} />
            </Routes>
          </div>
        </div>
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
