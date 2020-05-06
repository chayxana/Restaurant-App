import * as React from 'react';
import * as ReactDOM from 'react-dom';
import App from './App';
import './index.css';
import registerServiceWorker from './registerServiceWorker';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from './store/store';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core';

const appTheme = createMuiTheme({});

ReactDOM.render(
  <Provider store={store}>
    <BrowserRouter>
    <MuiThemeProvider theme={appTheme}>
      <App />
    </MuiThemeProvider>
    </BrowserRouter>
  </Provider>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
