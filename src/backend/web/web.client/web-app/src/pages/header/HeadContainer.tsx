import * as React from 'react';
import { RouteComponentProps } from "react-router";
import { RootState } from 'src/store/reducers/redusers';
import { toggleMenu } from 'src/store/actions/utils';
import { connect } from 'react-redux';
import { showCartDialog } from 'src/store/actions/cart';
import { logOut } from 'src/store/actions/user';
import Header from './Header';


type OwnProps = RouteComponentProps<{}>;

interface StateProps {
    cartItemsCount: number;
    loggedIn: boolean;
}

interface DispatchProps {
    toggleMenu(): void;
    showCartDialog(show: boolean): void;
    logOut(): void;
}

class HeaderContainer extends React.PureComponent<StateProps & DispatchProps & OwnProps> {
    public render() {
        return (
            <Header {... this.props}/>
        );
    }
}

const mapStateToProps = (state: RootState): StateProps => ({
    cartItemsCount: state.cart.cartItems.length,
    loggedIn: state.auth.loggedIn
});

const mapDispatchProps = {
    toggleMenu: toggleMenu,
    showCartDialog: showCartDialog,
    logOut: logOut
};

export default connect<StateProps, DispatchProps, OwnProps>(
	mapStateToProps,
	mapDispatchProps
)(HeaderContainer);