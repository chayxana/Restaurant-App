import * as React from "react";
import "./Header.css";
import { AppBar, Avatar, Badge, Button, IconButton, Menu, MenuItem, Select, TextField, Toolbar } from "@mui/material";
import { Person } from "@mui/icons-material";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import { NextRouter } from "next/router";

const categories = [
  {
    name: "All categories",
    icon: "list"
  },
  {
    name: "Foods",
    icon: "group"
  },
  {
    name: "Drinks",
    icon: "watch"
  },
  {
    name: "Deserts",
    icon: "menu_book"
  }
];

// Option items for product categories.
const categoryOptions = categories.map(x => {
  return (
    <MenuItem key={x.name} value={x.name}>
      {x.name}
    </MenuItem>
  );
});


interface Props {
  toggleMenu(): void;
  showCartDialog(show: boolean): void;
  logOut(): void;
  cartItemsCount: number;
  loggedIn: boolean;
	router: NextRouter;
}

interface State {
  searchTerm: string;
  anchorEl: any;
  categoryFilterValue: string;
}

class HeaderComponent extends React.Component<Props, State> {
  
  public readonly state: State = {
    searchTerm: "",
    anchorEl: null,
    categoryFilterValue: categories[0].name
  };

  public render() {
    const { anchorEl } = this.state;

    return (
      <AppBar
        position="static"
        style={{ backgroundColor: "#FAFAFB", padding: 10 }}
      >
        <Toolbar>
          <div className="left-part">
            <IconButton
              onClick={() => {
                this.props.toggleMenu();
              }}
            >
              {/* <MenuIcon fontSize="default" /> */}
            </IconButton>

            {/* <img
              src={cartImage}
              alt={"Logo"}
              style={{ marginLeft: 10 }}

            /> */}
            <TextField
              label="Search products"
              value={this.state.searchTerm}
              onChange={e => {
                this.setState({ searchTerm: e.target.value });
              }}
              style={{ marginLeft: 30, width: 250, marginBottom: 15 }}
            />

            <Select
              style={{ maxWidth: 200, marginLeft: 20 }}
              value={this.state.categoryFilterValue}
              MenuProps={{
                style: {
                  maxHeight: 500
                }
              }}
              onChange={e => {
                this.setState({ categoryFilterValue: e.target.value as string });
              }}
            >
              {categoryOptions}
            </Select>

            <Button
              style={{ marginLeft: 20 }}
              variant="outlined"
              color="primary"
              onClick={() => {
                this.props.router.push(
                  "/?category=" +
                  this.state.categoryFilterValue +
                  "&term=" +
                  this.state.searchTerm
                );
              }}
            >
              {" "}
              Search
            </Button>
          </div>
          <div className="right-part">
            {!this.props.loggedIn ? (
              <Button
                variant="outlined"
                style={{ marginRight: 20 }}
                color="primary"
                onClick={() => {
                  this.props.router.push("/login");
                }}
              >
                Log in
              </Button>
            ) : (
                <Avatar
                  onClick={event => {
                    this.setState({ anchorEl: event.currentTarget });
                  }}
                  style={{ backgroundColor: "#3f51b5", marginRight: 10 }}
                >
                  <Person />
                </Avatar>
              )}
            <IconButton
              aria-label="Cart"
              onClick={() => {
                this.props.showCartDialog(true);
              }}
            >
              <Badge badgeContent={this.props.cartItemsCount} color="primary">
                <AddShoppingCartIcon />
              </Badge>
            </IconButton>
            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={() => {
                this.setState({ anchorEl: null });
              }}
            >
              <MenuItem
                onClick={() => {
                  this.setState({ anchorEl: null });
                  this.props.router.push("/order");
                }}
              >
                Checkout page
              </MenuItem>
              <MenuItem
                onClick={() => {
                  this.props.logOut();
                  this.setState({ anchorEl: null });
                }}
              >
                Logout
              </MenuItem>
            </Menu>
          </div>
        </Toolbar>
      </AppBar>
    );
  }
}
export default HeaderComponent;
