import * as React from 'react';
import Card from "@material-ui/core/Card";
import { CardActionArea, CardMedia, CardContent, CardActions, Button, Tooltip, IconButton, createStyles, Theme, WithStyles, withStyles } from '@material-ui/core';

import AddShoppingCartIcon from "@material-ui/icons/AddShoppingCart";
import { IFoodDto } from 'src/api/dtos/FoodDto';
import { NavigateFunction } from 'react-router';

const styles = (_: Theme) => createStyles({
  item: {
    marginLeft: 5,
    fontWeight: "bold",
    whiteSpace: "nowrap",
    overflow: "hidden",
    textOverflow: "ellipsis"
  },
  price: {
    margin: 5
  },
  isPopular: {
    color: "#1a9349",
    fontWeight: "bold",
    margin: 5
  },
  cardStyle: {
    width: 200, 
    height: 270, 
    margin: 10, 
    display: "inline-block"
  },
  cardActions: { 
    display: "flex", 
    alignItems: "center", 
    height: 45 
  }
});

interface Props {
  item: IFoodDto;
  navigate: NavigateFunction;
}

const FoodItem: React.FC<Props & WithStyles<typeof styles>> = props => {
  return (
    <Card className={props.classes.cardStyle}>
      <CardActionArea
        onClick={() => {
          props.navigate("/details/" + props.item.id);
        }}
      >
        <CardMedia
          style={{ height: 140 }}
          image={props.item.pictures[0].filePath}
        />
        <CardContent style={{ height: 50 }}>
          <div className={props.classes.item}>
            {props.item.name}
          </div>
          <div className={props.classes.price}>Price: {props.item.price} $</div>
          <div className={props.classes.isPopular}> {false && "Popular"} </div>
        </CardContent>
      </CardActionArea>
      <CardActions className={props.classes.cardActions}>
        <Button
          size="small"
          style={{ marginRight: 60 }}
          onClick={() => {
            props.navigate("/details/" + props.item.id);
          }}>{" "}Details
        </Button>
        <Tooltip title="Add to cart">
          <IconButton
            size="small"
            // onClick={e => {
            //   e.stopPropagation();
            //   // this.props.dispatch(
            //   //   addItemInCart({ ...this.props.item, quantity: 1 })
            //   // );
            // }}
            color="primary"
            aria-label="Add to shopping cart"
          >
            <AddShoppingCartIcon color="primary" />
          </IconButton>
        </Tooltip>
      </CardActions>
    </Card>
  );
};

export default withStyles(styles)(FoodItem);