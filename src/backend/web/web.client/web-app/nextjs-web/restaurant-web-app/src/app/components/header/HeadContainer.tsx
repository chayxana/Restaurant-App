import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import HeaderComponent from './Header';
import { RootState } from '@/store/reducers/redusers';
import { toggleMenu } from '@/store/actions/utils';
import { showCartDialog } from '@/store/actions/cart';
import { logOut } from '@/store/actions/user';
import { useRouter } from 'next/router';

const HeaderContainer = () => {
    const dispatch = useDispatch();
    const router = useRouter(); 

    const cartItemsCount = useSelector((state: RootState) => state.cart.cartItems.length);
    const loggedIn = useSelector((state: RootState) => state.auth.loggedIn);

    const handleToggleMenu = () => dispatch(toggleMenu());
    const handleShowCartDialog = (show: boolean) => dispatch(showCartDialog(show));
    const handleLogOut = () => dispatch(logOut());

    return (
        <HeaderComponent 
            cartItemsCount={cartItemsCount}
            loggedIn={loggedIn}
            router={router}
            toggleMenu={handleToggleMenu}
            showCartDialog={handleShowCartDialog}
            logOut={handleLogOut}
        />
    );
};

export default HeaderContainer;
