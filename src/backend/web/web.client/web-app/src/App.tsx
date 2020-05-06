import * as React from 'react';
import './App.css';

import { Switch, Route } from 'react-router';
import FoodsListContainer from './pages/food/FoodsListContainer';
// import { RouteComponentProps } from 'react-router';
// import { ReactChild } from 'react';



class App extends React.PureComponent {
  public render() {
    return (
      <>
        <Switch>
          <Route path="/foods/" component={FoodsListContainer} />
        </Switch>
      </>
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
