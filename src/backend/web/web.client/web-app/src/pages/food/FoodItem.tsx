import * as React from 'react';
import Card from "@material-ui/core/Card";
import { CardActionArea, CardMedia, CardContent, CardActions, Button, Tooltip, IconButton } from '@material-ui/core';
import { RouteComponentProps } from 'react-router';
import AddShoppingCartIcon from "@material-ui/icons/AddShoppingCart";
import { FoodDto } from 'src/api/dtos/FoodDto';

interface Props extends RouteComponentProps<any> {
  item: FoodDto
}

const FoodItem: React.FC<Props> = props => {
  return (
    <Card
      style={{ width: 200, height: 270, margin: 10, display: "inline-block" }}
    >
      <CardActionArea
        onClick={() => {
          props.history.push("/details/" + props.item.id);
        }}
      >
        <CardMedia
          style={{ height: 140 }}
          image={props.item.pictures[0].filePath}
        />
        <CardContent style={{ height: 50 }}>
          <div
            style={{
              marginLeft: 5,
              fontWeight: "bold",
              whiteSpace: "nowrap",
              overflow: "hidden",
              textOverflow: "ellipsis"
            }}
          >
            {props.item.name}
          </div>
          <div style={{ margin: 5 }}>Price: {props.item.price} $</div>
          <div style={{ color: "#1a9349", fontWeight: "bold", margin: 5 }}>
            {false && "Popular"}
          </div>
        </CardContent>
      </CardActionArea>
      <CardActions
        style={{ display: "flex", alignItems: "center", height: 45 }}
      >
        <Button
          size="small"
          style={{ marginRight: 60 }}
          onClick={() => {
            props.history.push("/details/" + props.item.id);
          }}
        >
          {" "}
                Details
              </Button>
        <Tooltip title="Add to cart">
          <IconButton
            size="small"
            onClick={e => {
              e.stopPropagation();
              // this.props.dispatch(
              //   addItemInCart({ ...this.props.item, quantity: 1 })
              // );
            }}
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

export default FoodItem