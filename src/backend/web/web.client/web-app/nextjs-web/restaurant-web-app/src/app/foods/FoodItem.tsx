import * as React from 'react';
import { styled } from '@mui/material/styles';
import Card from "@mui/material/Card";
import CardActionArea from "@mui/material/CardActionArea";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import IconButton from "@mui/material/IconButton";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import { IFoodDto } from '@/api/dtos/FoodDto';
import { AppRouterInstance } from 'next/dist/shared/lib/app-router-context.shared-runtime';
import { NextRouter } from 'next/router';

// Styled components
const StyledCard = styled(Card)({
  width: 200, 
  height: 270, 
  margin: 10, 
  display: "inline-block"
});

const StyledItem = styled('div')({
  marginLeft: 5,
  fontWeight: "bold",
  whiteSpace: "nowrap",
  overflow: "hidden",
  textOverflow: "ellipsis"
});

const StyledPrice = styled('div')({
  margin: 5
});

const StyledIsPopular = styled('div')({
  color: "#1a9349",
  fontWeight: "bold",
  margin: 5
});

const StyledCardActions = styled(CardActions)({
  display: "flex", 
  alignItems: "center", 
  height: 45 
});

// FoodItem component
interface Props {
  item: IFoodDto;
  router: NextRouter
}

const FoodItem: React.FC<Props> = ({ item, router }) => {
  return (
    <StyledCard>
      <CardActionArea onClick={() => router.push("/details/" + item.id)}>
        <CardMedia style={{ height: 140 }} image={item.pictures[0].filePath} />
        <CardContent style={{ height: 50 }}>
          <StyledItem>{item.name}</StyledItem>
          <StyledPrice>Price: {item.price} $</StyledPrice>
          <StyledIsPopular>{false && "Popular"}</StyledIsPopular>
        </CardContent>
      </CardActionArea>
      <StyledCardActions>
        <Button size="small" style={{ marginRight: 60 }} onClick={() => router.push("/details/" + item.id)}>Details</Button>
        <Tooltip title="Add to cart">
          <IconButton size="small" color="primary" aria-label="Add to shopping cart">
            <AddShoppingCartIcon />
          </IconButton>
        </Tooltip>
      </StyledCardActions>
    </StyledCard>
  );
};

export default FoodItem;
